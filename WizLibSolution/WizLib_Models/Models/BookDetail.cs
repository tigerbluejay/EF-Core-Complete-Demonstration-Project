using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    public class BookDetail
    {
        [Key]
        public int BookDetail_Id { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public double Weight { get; set; }
        // adding Book Book Property in BookDetail class, and adding BookDetail BookDetail
        // property in the Book class enforces a one to one relationship between Book and BookDetail
        public Book Book { get; set; }

    }
}
