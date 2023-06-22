namespace SeaBattleWeb.Models;

public class Lobby
{
    public Lobby(string Player, bool IsPrivate)
    {
        Owner = Player;
        this.IsPrivate = IsPrivate;
    }

    public int Id { get; set; }

    public string Owner { get; set; }

    public string Opponent { get; set; }

    public bool InProggress { get; set; }

    public bool IsPrivate { get; set; }
}