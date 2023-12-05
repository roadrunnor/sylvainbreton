using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Models;

namespace api_sylvainbreton.Data
{
    public class SylvainBretonDbContext : DbContext
    {
        public SylvainBretonDbContext(DbContextOptions<SylvainBretonDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("SylvainBretonConnection");
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventArtwork> EventArtworks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<DynamicContent> DynamicContents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>()
                .HasKey(a => a.ArtistID);

            // Clé composite pour EventArtworks
            modelBuilder.Entity<EventArtwork>()
                .HasKey(ea => new { ea.EventID, ea.ArtworkID });

            // Configurer les relations one-to-many et many-to-many
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Place)
                .WithMany(b => b.Performances)
                .HasForeignKey(p => p.PlaceID);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Place)
                .WithMany(p => p.Events)
                .HasForeignKey(e => e.PlaceID);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Artwork)
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.ArtworkID);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Performance)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PerformanceID);

            modelBuilder.Entity<Sentence>()
                .HasOne(s => s.Artwork)
                .WithMany(a => a.Sentences)
                .HasForeignKey(s => s.ArtworkID);

            // Configuration pour EventArtworks (relation many-to-many)
            modelBuilder.Entity<EventArtwork>()
                .HasOne(ea => ea.Event)
                .WithMany(e => e.EventArtworks)
                .HasForeignKey(ea => ea.EventID);

            modelBuilder.Entity<EventArtwork>()
                .HasOne(ea => ea.Artwork)
                .WithMany(a => a.EventArtworks)
                .HasForeignKey(ea => ea.ArtworkID);

            modelBuilder.Entity<DynamicContent>()
                .HasKey(dc => dc.ContentID);

            // Configuration spécifique pour la table Artwork
            modelBuilder.Entity<Artwork>()
                .ToTable("Artwork");

            // Ajout des configurations ToTable pour chaque entité
            modelBuilder.Entity<Place>().ToTable("Place");
            modelBuilder.Entity<Performance>().ToTable("Performance");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<EventArtwork>().ToTable("EventArtwork");
            modelBuilder.Entity<Image>().ToTable("Image");
            modelBuilder.Entity<Sentence>().ToTable("Sentence");
        }
    }
}
