using Microsoft.EntityFrameworkCore;

namespace BarMenuBoardAPI.Models
{
    public class BarMenuBoardContext : DbContext
    {
        public BarMenuBoardContext(DbContextOptions<BarMenuBoardContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<TodaysSpecial> TodaysSpecials { get; set; }
    }
}
