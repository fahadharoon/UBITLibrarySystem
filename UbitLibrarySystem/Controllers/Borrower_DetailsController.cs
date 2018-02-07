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
    public class Borrower_DetailsController : Controller
    {
        private LMSContext db = new LMSContext();

        // GET: Borrower_Details
        public ActionResult Index(string searchBy, string search, int? page,string sortBy)
        { //sorting
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
       
            //ViewBag.SortAuthorParameter = sortBy== "Gender" ? "Gender desc " : "Gender";

            var borrowerDetails = db.Borrower_Details.AsQueryable();
            if (searchBy == "Id")
            {
                var borrower_Details = db.Borrower_Details.Include(b => b.Book_Details);
                //return View(await borrower_Details.ToListAsync());
                borrowerDetails = db.Borrower_Details.Where(x => x.Borrower_id.ToString() == search);
                   
            }
            else
            {
                var borrower_Details = db.Borrower_Details.Include(b => b.Book_Details);

                borrowerDetails=db.Borrower_Details.Where(x => x.Borrower_Name.StartsWith(search) || search == null);
                   

            }



            switch (sortBy)
            {
                case "Name desc":
                    borrowerDetails = borrowerDetails.OrderByDescending(x => x.Borrower_Name);
                    break;

                
                default:
                    borrowerDetails = borrowerDetails.OrderBy(x => x.Borrower_Name);
                    break;
            }
            return View(borrowerDetails.ToPagedList(page ?? 1, 10));

        }


        // GET: Student Borrower_Details
        public ActionResult IndexForBorrowerDetailsStudent(string searchBy, string search, int? page)
        {
            if (searchBy == "Id")
            {
                var borrower_Details = db.Borrower_Details.Include(b => b.Book_Details);
                return View(db.Borrower_Details.Where(x => x.Borrower_id.ToString() == search)
                                  .ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                var borrower_Details = db.Borrower_Details.Include(b => b.Book_Details);

                return View(db.Borrower_Details.Where(x => x.Borrower_Name.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1, 10));
            }
        }


        // GET: Borrower_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower_Details borrower_Details = await db.Borrower_Details.FindAsync(id);
            if (borrower_Details == null)
            {
                return HttpNotFound();
            }
            return View(borrower_Details);
        }

        // GET: Student Borrower_Details/Details/5
        public async Task<ActionResult> DetailsforBorrowerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower_Details borrower_Details = await db.Borrower_Details.FindAsync(id);
            if (borrower_Details == null)
            {
                return HttpNotFound();
            }
            return View(borrower_Details);
        }

        // GET: Borrower_Details/Create
        public ActionResult Create()
        {
            ViewBag.Book_id = new SelectList(db.Book_Details, "ISBN_Code", "Book_Title");
            ViewBag.Issued_By = System.Web.HttpContext.Current.Session["sessionString"];
            return View();
        }

        // POST: Borrower_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Book_id,Issued_By,Borrower_id,PKBorrower_id,Borrower_Name,Borrowed_From_Date,Borrowed_To_Date")] Borrower_Details borrower_Details)
        {
            if (ModelState.IsValid)
            {
                Book_Details book = db.Book_Details.Where(b => b.ISBN_Code == borrower_Details.Book_id).FirstOrDefault();
                string issuedBy = System.Web.HttpContext.Current.Session["sessionString"].ToString();
                tblUser user = db.tblUsers.Where(u => u.email == issuedBy).FirstOrDefault();
                if (book != null && book.No_of_Copies_Available != 0 && user != null)
                {
                    book.No_of_Copies_Available = book.No_of_Copies_Available - 1;
                    borrower_Details.Issued_By = user.id;
                    db.Borrower_Details.Add(borrower_Details);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("NoBooksAvailable");
                }
            }

            ViewBag.Book_id = new SelectList(db.Book_Details, "ISBN_Code", "Book_Title", borrower_Details.Book_id);
            //ViewBag.Issued_By = new SelectList(db.Staff_Details, "Staff_id", "Staff_Name", borrower_Details.Issued_By);
            return View(borrower_Details);
        }

        // GET: Borrower_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower_Details borrower_Details = await db.Borrower_Details.FindAsync(id);
            if (borrower_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Book_id = new SelectList(db.Book_Details, "ISBN_Code", "Book_Title", borrower_Details.Book_id);
            //ViewBag.Issued_By = new SelectList(db.Staff_Details, "Staff_id", "Staff_Name", borrower_Details.Issued_By);
            return View(borrower_Details);
        }

        // POST: Borrower_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Book_id,Issued_By,Borrower_id,PKBorrower_id,Borrower_Name,Borrowed_From_Date,Borrowed_To_Date")] Borrower_Details borrower_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrower_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Book_id = new SelectList(db.Book_Details, "ISBN_Code", "Book_Title", borrower_Details.Book_id);
            //ViewBag.Issued_By = new SelectList(db.Staff_Details, "Staff_id", "Staff_Name", borrower_Details.Issued_By);
            return View(borrower_Details);
        }

        // GET: Borrower_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrower_Details borrower_Details = await db.Borrower_Details.FindAsync(id);
            if (borrower_Details == null)
            {
                return HttpNotFound();
            }
            return View(borrower_Details);
        }

        // POST: Borrower_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            Borrower_Details borrower_Details = await db.Borrower_Details.FindAsync(id);
            Book_Details book = db.Book_Details.Where(b => b.ISBN_Code == borrower_Details.Book_id).FirstOrDefault();
            book.No_of_Copies_Available = book.No_of_Copies_Available + 1;
            db.Borrower_Details.Remove(borrower_Details);
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


        public ActionResult NoBooksAvailable()
        {
            return View();


        }
    }
}
