using matchsite.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace matchsite.WebApi.Context
{
    public class ApiContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-0SC4E7S\\SQLEXPRESS;initial Catalog=MatchSiteDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<Standing> Standings { get; set; }
        public DbSet<MatchEventType> MatchEventTypes { get; set; }
        public DbSet<Player> Players { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Maç - Ev Sahibi Takım İlişkisi
            modelBuilder.Entity<Match>()
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeMatches)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Maç - Deplasman Takımı İlişkisi
            modelBuilder.Entity<Match>()
                .HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayMatches)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Takım - Puan Durumu (Bire Bir İlişki)
            modelBuilder.Entity<Team>()
                .HasOne(x => x.Standing)
                .WithOne(x => x.Team)
                .HasForeignKey<Standing>(x => x.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Oyuncu - Takım İlişkisi
            modelBuilder.Entity<Player>()
                .HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Olay - Maç İlişkisi
            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.Match)
                .WithMany(x => x.MatchEvents)
                .HasForeignKey(x => x.MatchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Olay - Ana Oyuncu İlişkisi
            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.Player)
                .WithMany(x => x.MatchEvents)
                .HasForeignKey(x => x.PlayerId)
                .IsRequired(false) // Nullable yapıldı
                .OnDelete(DeleteBehavior.Restrict);

            // Olay - İkinci Oyuncu (Asist/Değişiklik) İlişkisi
            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.RelatedPlayer)
                .WithMany() // Player içinde RelatedMatchEvents listesine genelde gerek olmaz
                .HasForeignKey(x => x.RelatedPlayerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // API Aramaları İçin Index Tanımlamaları (Performans Artırır)
            modelBuilder.Entity<Team>().HasIndex(t => t.ExternalId).IsUnique();
            modelBuilder.Entity<Match>().HasIndex(m => m.ExternalId).IsUnique();
            modelBuilder.Entity<Player>().HasIndex(p => p.ExternalId).IsUnique();
        }

    }
}
