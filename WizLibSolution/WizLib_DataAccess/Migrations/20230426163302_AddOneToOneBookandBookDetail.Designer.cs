﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WizLib_DataAccess.Data;

namespace WizLib_DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230426163302_AddOneToOneBookandBookDetail")]
    partial class AddOneToOneBookandBookDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WizLib_Models.Models.Author", b =>
                {
                    b.Property<int>("Author_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Author_Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("WizLib_Models.Models.Book", b =>
                {
                    b.Property<int>("Book_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookDetail_Id")
                        .HasColumnType("int");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Book_Id");

                    b.HasIndex("BookDetail_Id")
                        .IsUnique();

                    b.HasIndex("Category_Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WizLib_Models.Models.BookDetail", b =>
                {
                    b.Property<int>("BookDetail_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfChapters")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("BookDetail_Id");

                    b.ToTable("BookDetail");
                });

            modelBuilder.Entity("WizLib_Models.Models.Category", b =>
                {
                    b.Property<int>("Category_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Category_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WizLib_Models.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("GenreId");

                    b.ToTable("tb_Genre");
                });

            modelBuilder.Entity("WizLib_Models.Models.Publisher", b =>
                {
                    b.Property<int>("Publisher_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Publisher_Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("WizLib_Models.Models.Book", b =>
                {
                    b.HasOne("WizLib_Models.Models.BookDetail", "BookDetail")
                        .WithOne("Book")
                        .HasForeignKey("WizLib_Models.Models.Book", "BookDetail_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WizLib_Models.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookDetail");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WizLib_Models.Models.BookDetail", b =>
                {
                    b.Navigation("Book");
                });
#pragma warning restore 612, 618
        }
    }
}
