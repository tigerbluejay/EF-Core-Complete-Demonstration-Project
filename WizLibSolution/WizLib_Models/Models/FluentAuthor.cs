using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    public class FluentAuthor
    {
        [Key] // Generates Identity Column automatically
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // this applies by default as per the annotation
        // above, but if you want to specify it it can be done like so
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // the property's value will be generated
        // by the database when it is first saved, and then regenerated each time the value is updated
        public int Author_Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Location { get; set; }
        // we don't want to add this property to the DB
        // this is just for computing the full name for the view
        [NotMapped]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }

        }
        // Implementing a many to many relationship such as the relationship between Book and Author
        // is a bit complicated in EFCore 5.
        // First We define ICollection<BookAuthor> BookAuthors in both Book and Author classes
        // We also add a DbSet<BookAuthor> BookAuthors in ApplicationDbContext, and in this class
        // using fluent API we set up a Composite Key, made up of Book_Id and Author_Id
        // In turn we create a model BookAuthor which has book these Ids as well as properties
        // Book Book and Author Author.
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<FluentBookAuthor> FluentBookAuthors { get; set; }

    }
}
