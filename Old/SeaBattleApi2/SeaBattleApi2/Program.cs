using Microsoft.EntityFrameworkCore;
using SeaBattleApi2;
using System.Net.NetworkInformation;
using static SeaBattleApi2.Game;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GameDb>(opt => opt.UseInMemoryDatabase("GameStateDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

var gameApi = app.MapGroup("/gameApi");

gameApi.MapGet("/allGames", GetAllGames);
gameApi.MapPost("/newGame", CreateNewGame);
gameApi.MapPost("/joinGame", JoinGame);
gameApi.MapPost("/makeReady", MakeReady);
gameApi.MapPost("/makeUnready", MakeUnready);
gameApi.MapGet("/gameConfig", GetGameConfig);
gameApi.MapPost("/getStatus", GetStatus);
gameApi.MapPost("/makeMove", MakeMove);

static async Task<IResult> GetStatus(GameIdAndPlayerSecret args, GameDb db)
{
    var game = await db.GameStates.FindAsync(args.Id);
    if (game is null) return TypedResults.Ok(new { status = "fail", reason = "Game does not exist" });

    // Ships of the player who calls the method
    string playerShips;

    if (game.Player1Secret == args.PlayerSecret) playerShips = game.Player1Ships;
    else if (game.Player2Secret == args.PlayerSecret) playerShips = game.Player2Ships;
    else return TypedResults.Ok(new { status = "fail", reason = "Wrong client secret" });

    return TypedResults.Ok(new { status = "ok", ownShips = playerShips, inProgress = game.InProgress });
}
static async Task<IResult> GetGameConfig()
{
    return TypedResults.Ok(new { status = "ok", height = Config.Height, width = Config.Width, ships = Config.Ships });
}
static async Task<IResult> SwitchReady(SwitchReadyArgs args, GameDb db, bool value)
{
    var game = await db.GameStates.FindAsync(args.Id);
    if (game is null) return TypedResults.Ok(new { status = "fail", reason = "Game does not exist" });

    if (game.InProgress)
    {
        return TypedResults.Ok(new { status = "fail", reason = "Game already in progress" });
    }

    if (game.Player1Secret == args.PlayerSecret)
    {
        game.Player1Ships = args.PlayerShips;

        if (!game.VerifySetup(game.Player1Ships))
        {
            game.Player1Ready = false;
            return TypedResults.Ok(new { status = "fail", reason = "Wrong map state" });
        }

        game.Player1Ready = true;
    }
    else if (game.Player2Secret == args.PlayerSecret)
    {
        game.Player2Ships = args.PlayerShips;

        if (!game.VerifySetup(game.Player2Ships))
        {
            game.Player2Ready = false;
            return TypedResults.Ok(new { status = "fail", reason = "Wrong map state" });
        }

        game.Player2Ready = true;
    }
    else return TypedResults.Ok(new { status = "fail", reason = "Wrong client secret" });

    if (game.Player1Ready && game.Player2Ready)
        game.InProgress = true;

    await db.SaveChangesAsync();

    return TypedResults.Ok(new { status = "ok", inProgress = game.InProgress});
}
static async Task<IResult> MakeReady(SwitchReadyArgs args, GameDb db)
{
    return await SwitchReady(args, db, true);
}
static async Task<IResult> MakeUnready(SwitchReadyArgs args, GameDb db)
{
    return await SwitchReady(args, db, false);
}
static async Task<IResult> MakeMove(MakeMoveArgs args, GameDb db)
{

}
static async Task<IResult> GetAllGames(GameDb db)
{
    return TypedResults.Ok(new { status = "ok", games = await db.GameStates.Where(x => x.Public).Select(x => new PublicGameInfo(x)).ToListAsync() });
}
static async Task<IResult> CreateNewGame(CreateNewGameArgs args, GameDb db)
{
    var newGame = new Game
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

static async Task<IResult> JoinGame(JoinGameArgs args, GameDb db)
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

    public PublicGameInfo (Game gameInfo)
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

public class SwitchReadyArgs : GameIdAndPlayerSecret
{
    public string PlayerShips { get; set; }
}

public class MakeMoveArgs : GameIdAndPlayerSecret
{
    public int AtX;
    public int AtY;
}