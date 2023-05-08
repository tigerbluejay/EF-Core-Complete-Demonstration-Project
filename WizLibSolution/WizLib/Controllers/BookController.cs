using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Models.Models;
using WizLib_Models.ViewModels;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Book> objList = _db.Books.ToList();
            foreach (var obj in objList)
            {
                // this is one way to load publishers - least efficient
                //obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_Id);
                // this will also load all the publishers but its more efficient
                // this is called Explicit Loading - more efficient
                _db.Entry(obj).Reference(u => u.Publisher).Load();

            }
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            BookViewModel obj = new BookViewModel();
            // this next statement is a projection, we save into the Publisher
            // list a SelectListItem with values drawn from the Publisher class.
            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });

            if (id == null)
            {
                // we create
                return View(obj);
            }
            // we edit
            // we use first because id is a primary key so there never will be more than
            // one object
            obj.Book = _db.Books.FirstOrDefault(x => x.Book_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookViewModel obj)
        {
            if (obj.Book.Book_Id == 0)
            {
                // this is create
                _db.Books.Add(obj.Book);
            }
            else
            {
                // this is update
                _db.Books.Update(obj.Book);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            BookViewModel obj = new BookViewModel();

            if (id == null)
            {
                // we create
                return View(obj);
            }
            // we edit
            obj.Book = _db.Books.FirstOrDefault(x => x.Book_Id == id);
            obj.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_Id == obj.Book.BookDetail_Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookViewModel obj)
        {
            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                // this is create
                _db.BookDetails.Add(obj.Book.BookDetail);
                _db.SaveChanges();

                var BookFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id);
                BookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id;
                _db.SaveChanges();
            }
            else
            {
                // this is update
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Books.FirstOrDefault(x => x.Book_Id == id);
            _db.Books.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
