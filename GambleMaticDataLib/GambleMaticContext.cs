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

            modelBuilder.Entity<GambleItemModel>()
                .HasOne<GameModel>(gim => gim.GameModel)
                .WithMany()
                .HasForeignKey(g => g.GameModelId);

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<GameModel>(
            //    entity =>
            //{
            //    entity.ToTable("Games");

            //    entity.Property(e => e.GameModelId).HasColumnName("GameModelId").ValueGeneratedOnAdd(); // <-- This fixed it for me
            //});

            //modelBuilder.Entity<PlayerModel>(
            //    entity =>
            //    {
            //        entity.ToTable("Players");

            //        entity.Property(e => e.PlayerModelId).HasColumnName("PlayerModelId").ValueGeneratedOnAdd(); // <-- This fixed it for me
            //    });




            //modelBuilder.Entity<PlayerModel>()
            //    .HasMany<GambleItemModel>(p => p.GambleItemModels).WithOne().HasForeignKey(g => g.Player);

            //modelBuilder.Entity<GambleItemModel>()
            //.Property<int>("PlayerForeignKey");

            //modelBuilder.Entity<GambleItemModel>()
            //    .HasOne(g => g.PlayerModel)
            //    .WithMany(p => p.GambleItemModels)
            //    .HasForeignKey("PlayerForeignKey");
        }

        public DbSet<GameModel> Games { get; set; }
        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<GambleItemModel> Gambles { get; set; }


    }
}
