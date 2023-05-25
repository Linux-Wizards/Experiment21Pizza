using Microsoft.EntityFrameworkCore;

namespace SeaBattleApi2
{
    class GameDb : DbContext
    {
        public GameDb(DbContextOptions<GameDb> options)
            : base(options) { }

        public DbSet<Game> GameStates => Set<Game>();
    }
}
