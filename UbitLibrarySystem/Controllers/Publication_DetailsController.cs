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
    public class Publication_DetailsController : Controller
    {
        private LMSContext db = new LMSContext();

        // GET: Publication_Details
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (searchBy == "Id")
            {
                return View(db.Publication_Details.Where(x => x.Publication_id.ToString() == search).ToList()
                    .ToPagedList(page ?? 1, 10));
            }

            {
                return View(db.Publication_Details.Where(x => x.Publisher_Name.StartsWith(search) || search == null)
                    .ToList().ToPagedList( page ?? 1, 10));

            }

        }





        // GET: Publication_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication_Details publication_Details = await db.Publication_Details.FindAsync(id);
            if (publication_Details == null)
            {
                return HttpNotFound();
            }
            return View(publication_Details);
        }

        // GET: Publication_Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publication_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Publication_id,Publisher_Name")] Publication_Details publication_Details)
        {
            if (ModelState.IsValid)
            {
                db.Publication_Details.Add(publication_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(publication_Details);
        }

        // GET: Publication_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication_Details publication_Details = await db.Publication_Details.FindAsync(id);
            if (publication_Details == null)
            {
                return HttpNotFound();
            }
            return View(publication_Details);
        }

        // POST: Publication_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Publication_id,Publisher_Name")] Publication_Details publication_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publication_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(publication_Details);
        }

        // GET: Publication_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication_Details publication_Details = await db.Publication_Details.FindAsync(id);
            if (publication_Details == null)
            {
                return HttpNotFound();
            }
            return View(publication_Details);
        }

        // POST: Publication_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Publication_Details publication_Details = await db.Publication_Details.FindAsync(id);
            db.Publication_Details.Remove(publication_Details);
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
