using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    public class Publisher
    {
        [Key]
        public int Publisher_Id {get; set;}
        [Required]
        public string Name { get; set;}
        [Required]
        public string Location { get; set;}
    
        // having defined Publisher Publisher in the Book class and Publisher_Id Navigation
        // property in the Book class, and now List<Book> Books in the Publisher class
        // we have now established a one to many relationship between Book and Publisher classes
        // A book may have only one Publisher, but a Publisher may publish one or more books, so
        // the relationship is one to many
        // in the database itself things get saved in the same way as in one to one relationship
        public List<Book> Books { get; set; }
    }
}
