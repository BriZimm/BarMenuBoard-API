using Microsoft.EntityFrameworkCore;
using BarMenuBoardAPI.Models;

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
        public DbSet<BoardStyle> BoardStyles { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<LiquorCategory> LiquorCategory { get; set; }
        public DbSet<MixerCategory> MixerCategory { get; set; }
        public DbSet<GarnishCategory> GarnishCategory { get; set; }
        public DbSet<AmountValue> AmountValues { get; set; }
    }
}
