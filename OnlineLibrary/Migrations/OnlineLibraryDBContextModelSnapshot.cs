﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OnlineLibrary.Data;
using System;

namespace OnlineLibrary.Migrations
{
    [DbContext(typeof(OnlineLibraryDBContext))]
    partial class OnlineLibraryDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineLibrary.Data.Models.Book", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("OnlineLibrary.Data.Models.Genre", b =>
                {
                    b.Property<int>("GenreId");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("OnlineLibrary.Data.Models.Writer", b =>
                {
                    b.Property<int>("WriterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .HasMaxLength(200);

                    b.Property<string>("UserName");

                    b.HasKey("WriterId");

                    b.ToTable("Writers");
                });

            modelBuilder.Entity("OnlineLibrary.Data.Models.Book", b =>
                {
                    b.HasOne("OnlineLibrary.Data.Models.Writer", "Writer")
                        .WithMany("Books")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineLibrary.Data.Models.Genre", b =>
                {
                    b.HasOne("OnlineLibrary.Data.Models.Book", "Book")
                        .WithOne("Genre")
                        .HasForeignKey("OnlineLibrary.Data.Models.Genre", "GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
