﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SadhinBangla.Data;

#nullable disable

namespace SadhinBangla.Migrations
{
    [DbContext(typeof(SadhinBanglaDbContext))]
    partial class SadhinBanglaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.Property<Guid>("BloaPostsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BloaPostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BlogPostTag");
                });

            modelBuilder.Entity("SadhinBangla.Models.Domain.BlogPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeaturedImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDaate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("SadhinBangla.Models.Domain.BlogPostLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogPostLikes");
                });

            modelBuilder.Entity("SadhinBangla.Models.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.HasOne("SadhinBangla.Models.Domain.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BloaPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SadhinBangla.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SadhinBangla.Models.Domain.BlogPostLike", b =>
                {
                    b.HasOne("SadhinBangla.Models.Domain.BlogPost", null)
                        .WithMany("Likes")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SadhinBangla.Models.Domain.BlogPost", b =>
                {
                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
