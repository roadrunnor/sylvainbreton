namespace api_sylvainbreton.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Text.Json;
    using api_sylvainbreton.Models;
    using System.IO;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, string seedDataFileKey)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SylvainBretonDbContext>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            var seedDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.GetValue<string>(seedDataFileKey));
            if (!File.Exists(seedDataFilePath))
            {
                throw new FileNotFoundException($"The seed data file was not found at the path: {seedDataFilePath}");
            }

            await SeedAsync(context, seedDataFilePath);
        }

        private static async Task SeedAsync(SylvainBretonDbContext context, string seedDataFilePath)
        {
            var jsonData = await File.ReadAllTextAsync(seedDataFilePath);
            var seedData = JsonSerializer.Deserialize<SeedDataRoot>(jsonData) ?? throw new InvalidOperationException("Seed data could not be loaded.");

            // Seed Artists
            if (seedData.Artists.Count != 0)
            {
                var artistIds = await context.Artists.Select(a => a.ArtistID).ToListAsync();
                var newArtists = seedData.Artists.Where(a => !artistIds.Contains(a.ArtistID));
                context.Artists.AddRange(newArtists);
            }

            // Seed Categories
            if (seedData.Categories.Count != 0)
            {
                var categoryIds = await context.Categories.Select(c => c.CategoryID).ToListAsync();
                var newCategories = seedData.Categories.Where(c => !categoryIds.Contains(c.CategoryID));
                context.Categories.AddRange(newCategories);
            }

            // Seed Places
            if (seedData.Places.Count != 0)
            {
                var placeIds = await context.Places.Select(p => p.PlaceID).ToListAsync();
                var newPlaces = seedData.Places.Where(p => !placeIds.Contains(p.PlaceID));
                context.Places.AddRange(newPlaces);
            }

            // Seed Performances
            if (seedData.Performances.Count != 0)
            {
                var performanceIds = await context.Performances.Select(p => p.PerformanceID).ToListAsync();
                var newPerformances = seedData.Performances.Where(p => !performanceIds.Contains(p.PerformanceID));
                context.Performances.AddRange(newPerformances);
            }

            // Seed Events
            if (seedData.Events.Count != 0)
            {
                var eventIds = await context.Events.Select(e => e.EventID).ToListAsync();
                var newEvents = seedData.Events.Where(e => !eventIds.Contains(e.EventID));
                context.Events.AddRange(newEvents);
            }

            // Seed Artworks
            if (seedData.Artworks.Count != 0)
            {
                var artworkIds = await context.Artworks.Select(a => a.ArtworkID).ToListAsync();
                var newArtworks = seedData.Artworks.Where(a => !artworkIds.Contains(a.ArtworkID));
                context.Artworks.AddRange(newArtworks);
            }

            // Seed Images
            if (seedData.Images.Count != 0)
            {
                var imageIds = await context.Images.Select(i => i.ImageID).ToListAsync();
                var newImages = seedData.Images.Where(i => !imageIds.Contains(i.ImageID));
                context.Images.AddRange(newImages);
            }

            // Seed EventArtworks
            if (seedData.EventArtworks.Count != 0)
            {
                var eventArtworkIds = await context.EventArtworks
                    .Select(ea => new { ea.EventID, ea.ArtworkID })
                    .ToListAsync();

                var newEventArtworks = seedData.EventArtworks
                    .Where(ea => !eventArtworkIds.Any(e => e.EventID == ea.EventID && e.ArtworkID == ea.ArtworkID));

                context.EventArtworks.AddRange(newEventArtworks);
            }

            // Seed Sentences
            if (seedData.Sentences.Count != 0)
            {
                var sentenceIds = await context.Sentences.Select(s => s.SentenceID).ToListAsync();
                var newSentences = seedData.Sentences.Where(s => !sentenceIds.Contains(s.SentenceID));
                context.Sentences.AddRange(newSentences);
            }

            // Seed DynamicContents
            if (seedData.DynamicContents.Count != 0)
            {
                var dynamicContentIds = await context.DynamicContents.Select(dc => dc.ContentID).ToListAsync();
                var newDynamicContents = seedData.DynamicContents.Where(dc => !dynamicContentIds.Contains(dc.ContentID));
                context.DynamicContents.AddRange(newDynamicContents);
            }

            // Perform a single save for all the changes
            await context.SaveChangesAsync();
        }

        private class SeedDataRoot
        {
            public List<Artist> Artists { get; set; }
            public List<Category> Categories { get; set; }
            public List<Place> Places { get; set; }
            public List<Performance> Performances { get; set; }
            public List<Event> Events { get; set; }
            public List<Image> Images { get; set; }
            public List<Sentence> Sentences { get; set; }
            public List<ArtworkImage> ArtworkImages { get; set; }
            public List<Artwork> Artworks { get; set; }
            public List<EventArtwork> EventArtworks { get; set; }
            public List<DynamicContent> DynamicContents { get; set; }
        }
    }
}