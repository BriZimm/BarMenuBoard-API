﻿// <auto-generated />
using BarMenuBoardAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BarMenuBoardAPI.Migrations
{
    [DbContext(typeof(BarMenuBoardContext))]
    [Migration("20200113041533_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BarMenuBoardAPI.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CurrentSpecial");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Garnish")
                        .IsRequired();

                    b.Property<int>("Glass");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("Ingredients")
                        .IsRequired();

                    b.Property<string>("Instructions")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Served");

                    b.Property<string>("SimilarTastes")
                        .IsRequired();

                    b.Property<int>("Strength");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("BarMenuBoardAPI.Models.TodaysSpecial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Garnish")
                        .IsRequired();

                    b.Property<int>("Glass");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Served");

                    b.Property<string>("SimilarTastes")
                        .IsRequired();

                    b.Property<int>("Strength");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("TodaysSpecials");
                });
#pragma warning restore 612, 618
        }
    }
}
