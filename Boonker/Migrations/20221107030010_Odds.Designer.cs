﻿// <auto-generated />
using System;
using Boonker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Boonker.Migrations
{
    [DbContext(typeof(BooksAddData))]
    [Migration("20221107030010_Odds")]
    partial class Odds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            modelBuilder.Entity("Boonker.Data.Models.Books", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("DSCR")
                        .HasColumnType("text");

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<bool>("IsFav")
                        .HasColumnType("boolean");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Views")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Boonker.Data.Models.Cats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("DSCR")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("Boonker.Data.Models.Books", b =>
                {
                    b.HasOne("Boonker.Data.Models.Cats", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Boonker.Data.Models.Cats", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
