﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_sylvainbreton.Data;

#nullable disable

namespace api_sylvainbreton.Migrations
{
    [DbContext(typeof(SylvainBretonDbContext))]
    [Migration("20240216194042_Bio")]
    partial class Bio
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("PostCategoryUserPost", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("UserPostsPostId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryId", "UserPostsPostId");

                    b.HasIndex("UserPostsPostId");

                    b.ToTable("PostCategoryUserPost");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ArtistID");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Artwork", b =>
                {
                    b.Property<int>("ArtworkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Conceptual")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Dimensions")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Materials")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("ArtworkID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Artwork", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.ArtworkImage", b =>
                {
                    b.Property<int>("ArtworkID")
                        .HasColumnType("int");

                    b.Property<int>("ImageID")
                        .HasColumnType("int");

                    b.HasKey("ArtworkID", "ImageID");

                    b.HasIndex("ImageID");

                    b.ToTable("ArtworkImage");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("CategoryID");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.DynamicContent", b =>
                {
                    b.Property<int>("ContentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ContentID");

                    b.ToTable("DynamicContent", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlaceID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

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

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("PerformanceID")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

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
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Materials")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("PerformanceDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlaceID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

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
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PlaceType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("PlaceID");

                    b.ToTable("Place", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.PostCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.PostTag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("UserPostPostId")
                        .HasColumnType("int");

                    b.HasKey("TagId");

                    b.HasIndex("UserPostPostId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Sentence", b =>
                {
                    b.Property<int>("SentenceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ArtworkID")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CityOfPublication")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CountryOfPublication")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SentencePage")
                        .HasColumnType("int");

                    b.HasKey("SentenceID");

                    b.HasIndex("ArtworkID");

                    b.ToTable("Sentence", (string)null);
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("UserComments");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserPost", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPosts");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserPostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("UserPostTags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_sylvainbreton.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostCategoryUserPost", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.PostCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_sylvainbreton.Models.UserPost", null)
                        .WithMany()
                        .HasForeignKey("UserPostsPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Artwork", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Category", "Category")
                        .WithMany("Artworks")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.ArtworkImage", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Artwork", "Artwork")
                        .WithMany("ArtworkImages")
                        .HasForeignKey("ArtworkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_sylvainbreton.Models.Image", "Image")
                        .WithMany("ArtworkImages")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artwork");

                    b.Navigation("Image");
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

            modelBuilder.Entity("api_sylvainbreton.Models.PostTag", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.UserPost", null)
                        .WithMany("Tags")
                        .HasForeignKey("UserPostPostId");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Sentence", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.Artwork", "Artwork")
                        .WithMany("Sentences")
                        .HasForeignKey("ArtworkID");

                    b.Navigation("Artwork");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserComment", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.UserPost", "UserPost")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_sylvainbreton.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserPost", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("UserPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserPostTag", b =>
                {
                    b.HasOne("api_sylvainbreton.Models.UserPost", "UserPost")
                        .WithMany("UserPostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_sylvainbreton.Models.PostTag", "PostTag")
                        .WithMany("UserPostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostTag");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.ApplicationUser", b =>
                {
                    b.Navigation("UserPosts");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Artwork", b =>
                {
                    b.Navigation("ArtworkImages");

                    b.Navigation("EventArtworks");

                    b.Navigation("Images");

                    b.Navigation("Sentences");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Category", b =>
                {
                    b.Navigation("Artworks");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Event", b =>
                {
                    b.Navigation("EventArtworks");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.Image", b =>
                {
                    b.Navigation("ArtworkImages");
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

            modelBuilder.Entity("api_sylvainbreton.Models.PostTag", b =>
                {
                    b.Navigation("UserPostTags");
                });

            modelBuilder.Entity("api_sylvainbreton.Models.UserPost", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Tags");

                    b.Navigation("UserPostTags");
                });
#pragma warning restore 612, 618
        }
    }
}
