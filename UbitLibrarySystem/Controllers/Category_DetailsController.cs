using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UbitLibrarySystem.Models;
using PagedList;
using PagedList.Mvc;

namespace UbitLibrarySystem.Controllers
{
    public class Category_DetailsController : Controller
    {
        private LMSContext db = new LMSContext();

        // GET: Category_Details
        public ActionResult Index(string searchBy, string search, int? page,string sortBy)
        {//sorting
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";

            //ViewBag.SortAuthorParameter = sortBy== "Gender" ? "Gender desc " : "Gender";

            var CategoryDetails = db.Category_Details.AsQueryable();

            if (searchBy == "Id")
            {
                CategoryDetails = db.Category_Details.Where(x => x.Category_id.ToString() == search);
                 
            }
            else
            {
                CategoryDetails= db.Category_Details.Where(x => x.Category_Name.StartsWith(search) || search == null);
                    

            }


            switch (sortBy)
            {
                case "Name desc":
                    CategoryDetails = CategoryDetails.OrderByDescending(x => x.Category_Name);
                    break;


                default:
                    CategoryDetails = CategoryDetails.OrderBy(x => x.Category_Name);
                    break;
            }
            return View(CategoryDetails.ToPagedList(page ?? 1, 10));
        }





        public ActionResult ProductCategory()
        {
            var model = db.Category_Details.ToList();
            return PartialView(model);
        }










        // GET: Category_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Details category_Details = await db.Category_Details.FindAsync(id);
            if (category_Details == null)
            {
                return HttpNotFound();
            }
            return View(category_Details);
        }






        // GET: Category_Details/Details/5
        public async Task<ActionResult> DetailsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Details category_Details = await db.Category_Details.FindAsync(id);
            if (category_Details == null)
            {
                return HttpNotFound();
            }
            return View(category_Details);
        }






        // GET: Category_Details/Create
        public ActionResult Create()
        {
            return View();
        }




        // GET: Category_Details/Create
        public ActionResult CreateCategory()
        {
            return View();
        }







        // POST: Category_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory([Bind(Include = "Category_id,Category_Name")] Category_Details category_Details)
        {
            if (ModelState.IsValid)
            {
                db.Category_Details.Add(category_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(category_Details);
        }








        // POST: Category_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Category_id,Category_Name")] Category_Details category_Details)
        {
            if (ModelState.IsValid)
            {
                db.Category_Details.Add(category_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(category_Details);
        }

        // GET: Category_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Details category_Details = await db.Category_Details.FindAsync(id);
            if (category_Details == null)
            {
                return HttpNotFound();
            }
            return View(category_Details);
        }



        // GET: Category_Details/Edit/5
        public async Task<ActionResult> EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Details category_Details = await db.Category_Details.FindAsync(id);
            if (category_Details == null)
            {
                return HttpNotFound();
            }
            return View(category_Details);
        }




        // POST: Category_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Category_id,Category_Name")] Category_Details category_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category_Details);
        }




        // POST: Category_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory([Bind(Include = "Category_id,Category_Name")] Category_Details category_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category_Details);
        }





        // GET: Category_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Details category_Details = await db.Category_Details.FindAsync(id);
            if (category_Details == null)
            {
                return HttpNotFound();
            }
            return View(category_Details);
        }





        // GET: Category_Details/Delete/5
        //public async Task<ActionResult> DeleteCategory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category_Details category_Details = await db.Category_Details.FindAsync(id);
        //    if (category_Details == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category_Details);
        //}

        

        // POST: Category_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category_Details category_Details = await db.Category_Details.FindAsync(id);
            db.Category_Details.Remove(category_Details);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
