﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_sylvainbreton.Data;

#nullable disable

namespace api_sylvainbreton.Migrations
{
    [DbContext(typeof(SylvainBretonDbContext))]
    partial class SylvainBretonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api_sylvainbreton.Models.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.HasKey("ArtistID");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Artwork", b =>
                {
                    b.Property<int>("ArtworkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ArtworkType")
                        .HasColumnType("longtext");

                    b.Property<string>("Conceptual")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Dimensions")
                        .HasColumnType("longtext");

                    b.Property<string>("Materials")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("ArtworkID");

                    b.ToTable("Artwork", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.DynamicContent", b =>
                {
                    b.Property<int>("ContentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<string>("Keyword")
                        .HasColumnType("longtext");

                    b.HasKey("ContentID");

                    b.ToTable("DynamicContents");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlaceID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("EventID");

                    b.HasIndex("PlaceID");

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.EventArtwork", b =>
                {
                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("ArtworkID")
                        .HasColumnType("int");

                    b.HasKey("EventID", "ArtworkID");

                    b.HasIndex("ArtworkID");

                    b.ToTable("EventArtwork", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ArtworkID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("FileRoute")
                        .HasColumnType("longtext");

                    b.Property<string>("MediaDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("MediaType")
                        .HasColumnType("longtext");

                    b.Property<int?>("PerformanceID")
                        .HasColumnType("int");

                    b.HasKey("ImageID");

                    b.HasIndex("ArtworkID");

                    b.HasIndex("PerformanceID");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Performance", b =>
                {
                    b.Property<int>("PerformanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Materials")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("PerformanceDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlaceID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("PerformanceID");

                    b.HasIndex("PlaceID");

                    b.ToTable("Performance", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Place", b =>
                {
                    b.Property<int>("PlaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("PlaceType")
                        .HasColumnType("longtext");

                    b.HasKey("PlaceID");

                    b.ToTable("Place", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Sentence", b =>
                {
                    b.Property<int>("SentenceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ArtworkID")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .HasColumnType("longtext");

                    b.Property<string>("BookTitle")
                        .HasColumnType("longtext");

                    b.Property<string>("CityOfPublication")
                        .HasColumnType("longtext");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<string>("CountryOfPublication")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("PublicationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Publisher")
                        .HasColumnType("longtext");

                    b.Property<int>("SentencePage")
                        .HasColumnType("int");

                    b.HasKey("SentenceID");

                    b.HasIndex("ArtworkID");

                    b.ToTable("Sentence", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Event", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Place", "Place")
                        .WithMany("Events")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.EventArtwork", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Artwork", "Artwork")
                        .WithMany("EventArtworks")
                        .HasForeignKey("ArtworkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_sylvainbreton.Models.Event", "Event")
                        .WithMany("EventArtworks")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Image", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Artwork", "Artwork")
                        .WithMany("Images")
                        .HasForeignKey("ArtworkID");

                    b.HasOne("api_sylvainbreton.Models.Performance", "Performance")
                        .WithMany("Images")
                        .HasForeignKey("PerformanceID");

                    b.Navigation("Artwork");

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Performance", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Place", "Place")
                        .WithMany("Performances")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Sentence", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Artwork", "Artwork")
                        .WithMany("Sentences")
                        .HasForeignKey("ArtworkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Artwork", b =>
                {
                    b.Navigation("EventArtworks");

                    b.Navigation("Images");

                    b.Navigation("Sentences");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Event", b =>
                {
                    b.Navigation("EventArtworks");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Performance", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Place", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Performances");
                });
#pragma warning restore 612, 618
        }
    }
}
