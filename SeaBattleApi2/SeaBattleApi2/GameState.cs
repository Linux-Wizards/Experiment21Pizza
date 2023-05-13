using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeaBattleApi2.GameMap;

namespace SeaBattleApi2
{
    public class GameState
    {
        public enum GameTile
        {
            Empty = '*',
            Undiscovered = ' ',
            UndiscoveredMarked = '+',
            Ship = '#',
            HitShip = 'X',
            HitEmpty = 'O',
            AroundSunkShip = '~'
        }

        [Key]
        public int Id { get; set; }
        public bool Public { get; set; }
        public string? GameCode { get; set; }
        public string Player1Name { get; set; }
        public string Player1Secret { get; set; } = "Secret"; // TODO: should be a real secret
        public bool Player1Ready { get; set; } = false;

        public string Player2Name { get; set; }
        public string Player2Secret { get; set; }
        public bool Player2Ready { get; set; } = false;
        public bool InProgress { get; set; } = false;
        // E.g. why the game finished
        public string? comment { get; set; }

        public string Player1Ships { get; set; } = new string((char)GameTile.Empty, Config.Height * Config.Width);
        public string Player1EnemyView { get; set; } = new string((char)GameTile.Empty, Config.Height * Config.Width);

        public string Player2Ships { get; set; } = new string((char)GameTile.Empty, Config.Height * Config.Width);
        public string Player2EnemyView { get; set; } = new string((char)GameTile.Empty, Config.Height * Config.Width);

        public GameTile At(int y, int x, string map)
        {
            if (y < 0 || x < 0 || y >= Config.Height || x >= Config.Width) return GameTile.Empty;
            return (GameTile)map[y * Config.Width + x];
        }
    }
}
