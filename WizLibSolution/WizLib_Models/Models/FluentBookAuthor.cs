using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizLib_Models.Models
{
    // we create this class to enforce many to many relationship in EFCore 5 and previous
    public class FluentBookAuthor
    {
        public int FluentBook_Id { get; set; }
        public int FluentAuthor_Id { get; set; }

        public FluentBook FluentBook { get; set; }
        public FluentAuthor FluentAuthor { get; set; }


    }
}
