using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Models.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objList = _db.Categories.ToList();
            // we could set any query to AsNoTracking() and this would
            // positively impact performance, but with AsNoTracking() you
            // would not be tracking changes in the object, so the statues
            // of Added, Modified, Unchanged and Deleted (and Detached) would
            // not be tracked and so you wouldn't be able to persist it to the db
            // use AsNoTracking() when you want to work ReadOnly and positively Impact performance
            //List<Category> objList = _db.Categories.AsNoTracking().ToList();

            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Category obj = new Category();
            if (id == null)
            {
                // we create
                return View(obj);
            }
            // we edit
            // we use first because id is a primary key so there never will be more than
            // one object
            obj = _db.Categories.FirstOrDefault(x => x.Category_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
              if (obj.Category_Id == 0)
                {
                    // this is create
                    _db.Categories.Add(obj);
                }
                else
                {
                    // this is update
                    _db.Categories.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Categories.FirstOrDefault(x => x.Category_Id ==id);
            _db.Categories.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // there are two ways of creating multiple entities.
        // you can add a for loop and add the items one by one
        // or you can create a list and loop through adding items to a list and then calling the AddRange method on the model
        public IActionResult CreateMultiple2()
        {
            List<Category> catList = new List<Category>();
            for (int i = 1; i <= 2; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
                // _db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _db.Categories.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateMultiple5()
        {
            List<Category> catList = new List<Category>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
                // _db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _db.Categories.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2()
        {
            IEnumerable<Category> catList = _db.Categories.OrderByDescending(x => x.Category_Id).Take(2).ToList();
            _db.Categories.RemoveRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple5()
        {
            IEnumerable<Category> catList = _db.Categories.OrderByDescending(x => x.Category_Id).Take(5).ToList();
            _db.Categories.RemoveRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
