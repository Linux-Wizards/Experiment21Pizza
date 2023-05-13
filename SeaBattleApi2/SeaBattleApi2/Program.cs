using Microsoft.EntityFrameworkCore;
using SeaBattleApi2;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameStateDb>(opt => opt.UseInMemoryDatabase("GameStateDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var gameApi = app.MapGroup("/gameApi");

gameApi.MapGet("/allGames", GetAllGames);
gameApi.MapPost("/newGame", CreateNewGame);
gameApi.MapPost("/joinGame", JoinGame);
gameApi.MapPost("/makeReady", MakeReady);
gameApi.MapPost("/makeUnready", MakeUnready);
gameApi.MapGet("/gameConfig", GetGameConfig);
gameApi.MapGet("/getStatus", GetStatus);

static async Task<IResult> GetStatus(GameStateDb db)
{

}
static async Task<IResult> GetGameConfig()
{
    return TypedResults.Ok(new { status = "ok", config = new GameConfig() });
}
static async Task<IResult> SwitchReady(GameIdAndPlayerSecret args, GameStateDb db, bool value)
{
    var game = await db.GameStates.FindAsync(args.Id);
    if (game is null) return TypedResults.Ok(new { status = "fail", reason = "Game does not exist" });

    if (game.Player1Secret == args.PlayerSecret) game.Player1Ready = value;
    else if (game.Player2Secret == args.PlayerSecret) game.Player2Ready = value;
    else return TypedResults.Ok(new { status = "fail", reason = "Wrong client secret" });

    if (game.Player1Ready && game.Player2Ready)
        game.InProgress = true;

    await db.SaveChangesAsync();

    return TypedResults.Ok(new { status = "ok", inProgress = game.InProgress});
}
static async Task<IResult> MakeReady(GameIdAndPlayerSecret args, GameStateDb db)
{
    return await SwitchReady(args, db, true);
}
static async Task<IResult> MakeUnready(GameIdAndPlayerSecret args, GameStateDb db)
{
    return await SwitchReady(args, db, false);
}
static async Task<IResult> GetAllGames(GameStateDb db)
{
    return TypedResults.Ok(new { status = "ok", games = await db.GameStates.Where(x => x.Public).Select(x => new PublicGameInfo(x)).ToListAsync() });
}
static async Task<IResult> CreateNewGame(CreateNewGameArgs args, GameStateDb db)
{
    var newGame = new GameState
    {
        Public = args.Public,
        GameCode = args.GameCode,
        Player1Name = args.Player1Name
    };

    db.GameStates.Add(newGame);
    await db.SaveChangesAsync();

    // Returns the ID of the new game
    return TypedResults.Ok(new { status = "ok", gameId = newGame.Id });
}

static async Task<IResult> JoinGame(JoinGameArgs args, GameStateDb db)
{
    var game = await db.GameStates.FindAsync(args.Id);
    if (game is null) return TypedResults.Ok(new { status="fail", reason="Game does not exist"});

    if ((!game.Public && game.GameCode != args.GameCode)
        || (game.Player1Name == args.Player2Name)
        || (game.Player2Name is not null))
        return TypedResults.Ok(new { status = "fail", reason = "Wrong code, trying to join your own game, or game is full" });

    game.Player2Name = args.Player2Name;
    await db.SaveChangesAsync();

    return TypedResults.Ok(new { status = "ok" });
}

// Launch the app
app.Run();

// Argument classes
public class CreateNewGameArgs
{
    public bool Public { get; set; }
    public string? GameCode { get; set; }
    public string Player1Name { get; set; }
}

public class JoinGameArgs
{
    public int Id { get; set; }
    public string? GameCode { get; set; }
    public string Player2Name { get; set; }
}

public class PublicGameInfo
{
    public int Id { get; set; }
    public bool Public { get; set; }
    public string Player1Name { get; set; }
    public string? Player2Name { get; set; }

    public PublicGameInfo (GameState gameInfo)
    {
        Id = gameInfo.Id;
        Public = gameInfo.Public;
        Player1Name = gameInfo.Player1Name;
        Player2Name = gameInfo.Player2Name;
    }
}

public class GameIdAndPlayerSecret
{
    public int Id { get; set; }
    public string PlayerSecret { get; set; }
}

public class GameConfig
{
    public int Width { get; set; } = 10;
    public int Height { get; set; } = 10;
    // First value - ship size
    // Second value - count
    public Dictionary<int, int> Ships { get; set; } = new Dictionary<int, int>()
    {
        { 2, 4 },
        { 3, 3 },
        { 4, 2 },
        { 6, 1 }
    };
}
