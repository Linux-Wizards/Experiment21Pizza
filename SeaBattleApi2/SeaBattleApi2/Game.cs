using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeaBattleApi2
{
    public class Game
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

        public string? Player2Name { get; set; }
        public string Player2Secret { get; set; } = "Secret2"; // TODO: should be a real secret
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

        // Returns true if there is a ship tile adjacent (indicating an incorrect map)
        private bool checkAdjacentForHorizontal(int y, int x, string map)
        {
            // Check down
            if (y + 1 < Config.Height && At(y + 1, x, map) != GameTile.Empty) return true;
            // Check up
            if (y - 1 >= 0 && At(y - 1, x, map) != GameTile.Empty) return true;

            // Check down + left
            if (y + 1 < Config.Height && x - 1 >= 0 && At(y + 1, x - 1, map) != GameTile.Empty) return true;
            // Check down + right
            if (y + 1 >= 0 && x + 1 < Config.Width && At(y + 1, x + 1, map) != GameTile.Empty) return true;

            // Check up + left
            if (y - 1 < Config.Height && x - 1 >= 0 && At(y - 1, x - 1, map) != GameTile.Empty) return true;
            // Check up + right
            if (y - 1 >= 0 && x + 1 < Config.Width && At(y - 1, x + 1, map) != GameTile.Empty) return true;

            return false;
        }

        // Returns true if there is a ship tile adjacent (indicating an incorrect map)
        private bool checkAdjacentForVertical(int y, int x, string map)
        {
            // Check right
            if (x + 1 < Config.Height && At(y, x + 1, map) != GameTile.Empty) return true;
            // Check left
            if (x - 1 >= 0 && At(y, x - 1, map) != GameTile.Empty) return true;

            // Check left + left
            if (y + 1 < Config.Height && x - 1 >= 0 && At(y + 1, x - 1, map) != GameTile.Empty) return true;
            // Check left + right
            if (y + 1 >= 0 && x + 1 < Config.Width && At(y + 1, x + 1, map) != GameTile.Empty) return true;

            // Check right + left
            if (y - 1 < Config.Height && x - 1 >= 0 && At(y - 1, x - 1, map) != GameTile.Empty) return true;
            // Check right + right
            if (y - 1 >= 0 && x + 1 < Config.Width && At(y - 1, x + 1, map) != GameTile.Empty) return true;

            return false;
        }

        public bool VerifySetup(string ShipMap)
        {
            var ships = new Dictionary<int, int>()
    {
        { 2, 0 },
        { 3, 0 },
        { 4, 0 },
        { 6, 0 }
    };

            int tilesOccupied = 0;
            int shouldBeOccupied = ships.Sum(x => x.Value);

            // Horizontal pass
            for (int y = 0; y < Config.Height; y++)
            {
                int currentSize = 0;
                for (int x = 0; x < Config.Width; x++)
                {
                    if (At(y, x, ShipMap) == GameTile.Empty)
                    {
                        if (currentSize > 1)
                        {
                            if (currentSize > 6)
                            {
                                return false;
                            }

                            ships[currentSize] += 1;
                        }
                            
                            
                        currentSize = 0;
                    }
                    else if (At(y, x, ShipMap) == GameTile.Ship)
                    {
                        tilesOccupied++;
                        currentSize += 1;
                    }
                }
            }

            if (tilesOccupied != shouldBeOccupied)
            {
                return false;
            }

            return (ships.Count == Config.Ships.Count) && (!ships.Except(Config.Ships).Any());
        }
    }
}
