using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            // List<Book> objList = _db.Books.ToList();
            // this is the third most efficient way to call - it's called eager loading
            // it makes fewest calls to the database (eager loading)
            List<Book> objList = _db.Books.Include(u => u.Publisher).ToList();
            //foreach (var obj in objList)
            //{
            //    // this is one way to load publishers - least efficient
            //    //obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_Id);
                
            //    // this will also load all the publishers but its more efficient
            //    // this is called Explicit Loading - more efficient
            //    _db.Entry(obj).Reference(u => u.Publisher).Load();

            //}


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
            //obj.Book = _db.Books.FirstOrDefault(x => x.Book_Id == id);
            //obj.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_Id == obj.Book.BookDetail_Id);
            // now with eager loading: do include before FirstOrDefault
            obj.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault( u => u.Book_Id == id);
            
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

        public IActionResult PlayGround()
        {
            //// execution (translation to sql query) will happen when
            //// you can a method such as Count() Single() FirstOrDefault()
            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //double totalPrice = 0;

            //// or when we iterate through a collection
            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //// or when we convert toList toArray or toDictionary
            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //// execution here too:
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();

            // difference between IEnumerable and IQueryable
            // IQueryable will filter in the SQL Query, IEnumerable will filter in memory after
            // fetching all the records. So IQueryable is preferable for performance issues
            // when filtering data.

            // now introducing IQueryable and IEnumerable
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 500).ToList();
            // the query returns all books, but filtering happens in memory afterwards

            IQueryable<Book> BookList2 = _db.Books;
            var FilteredBook2 = BookList1.Where(b => b.Price > 500).ToList();
            // the query returns just the books where price is greater than 500

            // When we want to filter our data it is best to use IQueryable

            ////// this is how we manually alter the state of an entity
            ////// this is not a common scenario
            ////var category = _db.Categories.FirstOrDefault();
            ////_db.Entry(category).State = EntityState.Modified;
            ////_db.SaveChanges();



            // Updating Related Data
            var bookTemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            bookTemp1.BookDetail.NumberOfChapters = 2222;
            _db.Books.Update(bookTemp1);
            _db.SaveChanges();
            // When you update it updates BookDetails, but it also updates Book

            var bookTemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            bookTemp2.BookDetail.Weight = 3333;
            _db.Books.Attach(bookTemp2);
            _db.SaveChanges();
            // When you attach it executes the update only on BookDetails, it won't update the rest
            // of the book entity

            // Attach puts all the entities in the graph in the unchanged state
            // Update puts all the entities in the graph in the modified state


            // You can also set manually the entity state in modified state 


            return RedirectToAction(nameof(Index));
        }
    }
}
