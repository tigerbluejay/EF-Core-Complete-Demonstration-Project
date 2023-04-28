using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    public class FluentBook
    {
        public int Book_Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }

        public int FluentBookDetail_Id { get; set; }
        public FluentBookDetail FluentBookDetail { get; set; }

        public int FluentPublisher_Id { get; set; }
        public FluentPublisher FluentPublisher { get; set;}

        public ICollection<FluentBookAuthor> FluentBookAuthors { get; set; }


    }
}
