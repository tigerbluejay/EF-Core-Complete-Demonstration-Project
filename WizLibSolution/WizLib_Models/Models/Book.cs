using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }

        // this property we don't want to add in the db
        // it is only for our view. It's a Calculation based on Price
        [NotMapped]
        public string PriceRange { get; set; }

        // foreign key relationship
        // by adding this property here several things will happen in the migration
        // first a CategoryId Column is added to the Books Table
        // CategoryId Column is made into an Index Column
        // CategoryId is assigned a Foreign Key (is made into a foreign key)
        // which refrences the CategoryId column in the Categories table
        public Category Category { get; set; }
        // this is a navigation property it helps visualize things more clearly
        // in terms of relationships, it is not necessary to build the relationship in
        // the database but its a good idea to add it apart from the Category Category property
        // above.
        [ForeignKey("Category")]
        public int Category_Id { get; set; }

        public BookDetail BookDetail { get; set; }
        [ForeignKey("BookDetail")]
        public int BookDetail_Id { get; set; }

        public Publisher Publisher { get; set; }
        [ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }


    }
}
