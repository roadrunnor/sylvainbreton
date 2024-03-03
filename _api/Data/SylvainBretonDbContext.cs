namespace api_sylvainbreton.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using api_sylvainbreton.Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Identity;

    public class SylvainBretonDbContext(DbContextOptions<SylvainBretonDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("SYLVAINBRETON_DB_CONNECTION");
                optionsBuilder
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            }
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventArtwork> EventArtworks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<DynamicContent> DynamicContents { get; set; }
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<UserPostTag> UserPostTags { get; set; }
        public DbSet<ArtworkImage> ArtworkImages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artist>()
                .HasKey(a => a.ArtistID);

            modelBuilder.Entity<Artist>()
                .HasIndex(a => new { a.FirstName, a.LastName })
                .IsUnique();

            modelBuilder.Entity<Artwork>()
                .HasOne(a => a.Category) 
                .WithMany(c => c.Artworks) 
                .HasForeignKey(a => a.CategoryID);

            modelBuilder.Entity<ArtworkImage>()
                .HasKey(ai => new { ai.ArtworkID, ai.ImageID });

            modelBuilder.Entity<ArtworkImage>()
                .HasIndex(ai => ai.ArtworkID);

            modelBuilder.Entity<ArtworkImage>()
                .HasIndex(ai => ai.ImageID);

            modelBuilder.Entity<ArtworkImage>()
                .HasOne(ai => ai.Artwork)
                .WithMany(a => a.ArtworkImages)
                .HasForeignKey(ai => ai.ArtworkID);

            modelBuilder.Entity<ArtworkImage>()
                .HasOne(ai => ai.Image)
                .WithMany(i => i.ArtworkImages)
                .HasForeignKey(ai => ai.ImageID);

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
                .HasForeignKey(i => i.ArtworkID)
                .IsRequired(false); 

            modelBuilder.Entity<Image>()
                .HasIndex(img => img.FileName)
                .IsUnique();

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Performance)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PerformanceID)
                .IsRequired(false);

            modelBuilder.Entity<Sentence>()
                .HasOne(s => s.Artwork)
                .WithMany(a => a.Sentences)
                .HasForeignKey(s => s.ArtworkID);

            modelBuilder.Entity<EventArtwork>()
                .HasKey(ea => new { ea.EventID, ea.ArtworkID });

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

            modelBuilder.Entity<UserPost>()
                .HasKey(up => up.PostId);

            modelBuilder.Entity<UserPost>()
                .HasMany(up => up.UserPostTags)
                .WithOne(upt => upt.UserPost)
                .HasForeignKey(upt => upt.PostId);

            modelBuilder.Entity<UserComment>()
                .HasKey(uc => uc.CommentId);

            modelBuilder.Entity<PostCategory>()
                .HasKey(pc => pc.CategoryId);

            modelBuilder.Entity<PostTag>()
                .HasMany(pt => pt.UserPostTags)
                .WithOne(up => up.PostTag)
                .HasForeignKey(up => up.TagId);

            modelBuilder.Entity<UserPostTag>()
                .HasKey(upt => new { upt.PostId, upt.TagId });

            modelBuilder.Entity<UserPostTag>()
                .HasOne(upt => upt.UserPost)
                .WithMany(up => up.UserPostTags) 
                .HasForeignKey(upt => upt.PostId);

            modelBuilder.Entity<UserPostTag>()
                .HasOne(upt => upt.PostTag)
                .WithMany(pt => pt.UserPostTags)
                .HasForeignKey(upt => upt.TagId);


            modelBuilder.Entity<Artist>().ToTable("Artist");
            modelBuilder.Entity<Artwork>().ToTable("Artwork");
            modelBuilder.Entity<ArtworkImage>().ToTable("ArtworkImage");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Place>().ToTable("Place");
            modelBuilder.Entity<Performance>().ToTable("Performance");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<EventArtwork>().ToTable("EventArtwork");
            modelBuilder.Entity<Image>().ToTable("Image");
            modelBuilder.Entity<Sentence>().ToTable("Sentence"); 
            modelBuilder.Entity<DynamicContent>().ToTable("DynamicContent");
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");

            // Identity configurations that require the generic type parameter
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}
