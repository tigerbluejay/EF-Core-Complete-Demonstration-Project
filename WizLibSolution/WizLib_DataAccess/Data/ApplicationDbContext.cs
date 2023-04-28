using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_DataAccess.Migrations;
using WizLib_Models.Models;

namespace WizLib_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        
        // to delete a table from db, remember to remove from here
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; } // this is only to enforce many to many relationship
        // it is a table that links books with authors, one book can have more than one author, one author can
        // author more than one book.
        public DbSet<BookDetail> BookDetails { get; set; } // we are adding this here, but 
        // EFCore is smart enough that even if we didn't have this here, because BookDetail
        // is a navigation property in Books, it knows to add the table to the DB anyway.
        // That said, we should be neat and spell it out and specify it here also for clarity.

        public DbSet<FluentBookDetail> FluentBookDetails { get; set; }
        public DbSet<FluentBook> FluentBooks { get; set; }
        public DbSet<FluentAuthor> FluentAuthors { get; set; }
        public DbSet<FluentPublisher> FluentPublishers { get; set; }

        // fluent api can do some things data annotations cannot
        // for example defining a composite class for BookAuthor
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // setting up a composite key:
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            // FLUENT API AS AN ALTERNATIVE TO DATA ANNOTATIONS
            modelBuilder.Entity<FluentBookDetail>().HasKey(x => x.BookDetail_Id);
            modelBuilder.Entity<FluentBookDetail>().Property(x => x.NumberOfChapters).IsRequired();

            modelBuilder.Entity<FluentBook>().HasKey(x => x.Book_Id);
            modelBuilder.Entity<FluentBook>().Property(x => x.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<FluentBook>().Property(x => x.Title).IsRequired();
            modelBuilder.Entity<FluentBook>().Property(x => x.Price).IsRequired();

            modelBuilder.Entity<FluentAuthor>().HasKey(x => x.Author_Id);
            modelBuilder.Entity<FluentAuthor>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<FluentAuthor>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<FluentAuthor>().Ignore(x => x.FullName);

            modelBuilder.Entity<FluentPublisher>().HasKey(x => x.Publisher_Id);
            modelBuilder.Entity<FluentPublisher>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<FluentPublisher>().Property(x => x.Location).IsRequired();

            modelBuilder.Entity<Category>().ToTable("tbl_category"); // this table name
            // will take precedence over the Category Class version as a specification of table name
            // because Fluent API takes precedence over Data Annotations
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("CategoryName");
            // and the same here, the column in the database will be named "CategoryName" and
            // not "Name" as specified in the Category class file.

            // FLUENT API RELATIONSHIPS
            // ONE TO ONE BETWEEN FLUENTBOOK AND FLUENTBOOKDETAIL
            // first we need to add the respective objects and navigation properties in the classes
            // we need to specify first the primary key with HasKey, which we've done above, then:
            modelBuilder.Entity<FluentBook>()
                .HasOne(x => x.FluentBookDetail)
                .WithOne(x => x.FluentBook).HasForeignKey<FluentBook>("FluentBookDetail_Id");

            // ONE TO MANY BETWEEN FLUENTBOOK AND FLUENTPUBLISHER
            modelBuilder.Entity<FluentBook>()
                .HasOne(x => x.FluentPublisher)
                .WithMany(x => x.FluentBook).HasForeignKey(x => x.FluentPublisher_Id);

            // MANY TO MANY BETWEEN FLUENTBOOK AND FLUENTAUTHOR
            // Remember we need an intermediate table, and we establish one to many relationships
            // with that table for both fluentbook and fluentauthor
            // first we nee a composite key
            modelBuilder.Entity<FluentBookAuthor>()
                .HasKey(ba => new { ba.FluentAuthor_Id, ba.FluentBook_Id });
            modelBuilder.Entity<FluentBookAuthor>()
                .HasOne(x => x.FluentBook)
                .WithMany(x => x.FluentBookAuthors).HasForeignKey(x => x.FluentBook_Id);
            modelBuilder.Entity<FluentBookAuthor>()
                .HasOne(x => x.FluentAuthor)
                .WithMany(x => x.FluentBookAuthors).HasForeignKey(x => x.FluentAuthor_Id);


        }
    }
}
