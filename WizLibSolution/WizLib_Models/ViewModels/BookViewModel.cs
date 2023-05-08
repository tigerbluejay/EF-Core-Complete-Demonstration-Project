using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizLib_Models.Models;

namespace WizLib_Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }


    }
}
