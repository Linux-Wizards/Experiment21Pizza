namespace SeaBattleApi2
{
    public class GameState
    {
        public enum GameTile
        {
            Empty,
            Undiscovered,
            UndiscoveredMarked,
            Ship,
            HitShip,
            HitEmpty,
            AroundSunkShip
        }

        public int Id { get; set; }
        public bool Public { get; set; }
        public string? GameCode { get; set; }
        public string Player1Name { get; set; }
        public string Player1Secret { get; set; } = "Secret"; // TODO: should be a real secret
        public bool Player1Ready { get; set; } = false;
        GameTile[,]? Player1Ships;
        GameTile[,]? Player1EnemyView;
        public string? Player2Name { get; set; }
        public string? Player2Secret { get; set; }
        public bool Player2Ready { get; set; } = false;
        GameTile[,]? Player2Ships;
        GameTile[,]? Player2EnemyView;
        public bool InProgress = false;
    }
}
