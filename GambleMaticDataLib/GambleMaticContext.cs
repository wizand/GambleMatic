using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace GambleMaticDataLib
{
    public class GambleMaticContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("GambleMaticDb");
            optionsBuilder
                .UseSqlServer(
                "Data Source=ANKKADESKTOP;Initial Catalog=GambleMatic;Integrated Security=True;TrustServerCertificate=True",
                    //@"Server=ANKKADESKTOP\wizand;Database=GameMatic",
                    providerOptions => { providerOptions.EnableRetryOnFailure(); }).LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        public GambleMaticContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<PlayerModel>().HasMany<GambleItemModel>(g => g.GambleItemModels)
                    .WithOne(e => e.PlayerModel);
            modelBuilder.Entity<GambleItemModel>().HasOne<PlayerModel>(gi => gi.PlayerModel)
                .WithMany(pm => pm.GambleItemModels)
                .HasForeignKey(gim => gim.PlayerModelId);

            modelBuilder.Entity<PlayerModel>().HasOne<ExtraGamblesModel>(pm => pm.ExtraGambles).WithOne(eg => eg.PlayerModel);
            modelBuilder.Entity<ExtraGamblesModel>().HasOne<PlayerModel>(egm => egm.PlayerModel).WithOne(pm => pm.ExtraGambles);

            modelBuilder.Entity<GambleItemModel>()
                .HasOne<GameModel>(gim => gim.GameModel)
                .WithMany()
                .HasForeignKey(g => g.GameModelId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GameModel> Games { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<GambleItemModel> Gambles { get; set; }
        public DbSet<ExtraGamblesModel> ExtraGambles { get; set; }

    }
}
