using Microsoft.EntityFrameworkCore;

namespace SeaBattleApi2
{
    class GameStateDb : DbContext
    {
        public GameStateDb(DbContextOptions<GameStateDb> options)
            : base(options) { }

        public DbSet<GameState> GameStates => Set<GameState>();
    }
}
