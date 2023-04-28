using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    // we create this class to enforce many to many relationship in EFCore 5 and previous
    public class BookAuthor
    {
        [ForeignKey("Book")]
        public int Book_Id { get; set; }
        [ForeignKey("Author")]
        public int Author_Id { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }

    }
}
