using SeaBattleApi.Models;

namespace SeaBattleApi.Services;

public static class LobbyService
{
    static List<Lobby> Lobbys { get; }
    static int nextId = 0;
    static LobbyService()
    {
        Lobbys = new List<Lobby>();
    }

    public static List<Lobby> GetAll() => Lobbys.FindAll(e=> ! e.IsPrivate);
    

    public static Lobby? Get(int id) => Lobbys.FirstOrDefault(p => p.Id == id);

    public static void Add(Lobby Lobby)
    {
        Lobby.Id = nextId++;
        Lobbys.Add(Lobby);
    }

    public static void Delete(int id)
    {
        var Lobby = Get(id);
        if(Lobby is null)
            return;

        Lobbys.Remove(Lobby);
    }

    public static void Update(Lobby Lobby)
    {
        var index = Lobbys.FindIndex(p => p.Id == Lobby.Id);
        if(index == -1)
            return;

        Lobbys[index] = Lobby;
    }
}