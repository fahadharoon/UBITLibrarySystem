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
    public class Student_DetailsController : Controller
    {
        private LMSContext db = new LMSContext();

        // GET: Student_Details for admin
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if(searchBy== "Id"){
                return View(db.Student_Details.Where(x => x.Student_id.ToString() == search).ToList()
                    .ToPagedList(page ?? 1, 10));
            }
            else if (searchBy == "Gender")
            {
                return View(db.Student_Details.Where(x => x.Gender.ToString() == search).ToList()
                    .ToPagedList(page ?? 1, 10));
            }
            else {
                return View(db.Student_Details.Where(x => x.Student_Name.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1, 10));
            }
        }      
        
        // GET: Registered Student_List for admin

        public ActionResult StudentList(string searchBy, string search, int? page )
        {
            //if (!string.IsNullOrEmpty(search))
            //{
            //    return View(db.tblUsers.Where(x => x.email.StartsWith(search) || search == null)
            //        .ToList().ToPagedList(page ?? 1, 10));
            //}

          

            if (searchBy == "Name")
            {
                return View(db.tblUsers.Where(x => x.UserName.StartsWith(search) || search == null)
                   .ToList().ToPagedList(page ?? 1, 15));
            }



            else if (searchBy == "Email")
            {
                return View(db.tblUsers.Where(x => x.email.StartsWith(search) || search == null)
                   .ToList().ToPagedList(page ?? 1, 15));
            }
            else
            {
                return View(db.tblUsers.Where(x => x.roleid==2).ToList().ToPagedList(page ?? 1, 15));
            }


           





        } 
        
        // GET: Registered Student_List for student

        public ActionResult StudentListforStudent(string searchBy,string search, int? page)
        {

            if (searchBy == "Name")
            {
                return View(db.tblUsers.Where(x => x.UserName.StartsWith(search) || search == null)
                   .ToList().ToPagedList(page ?? 1, 15));
            }



            else if (searchBy == "Email")
            {
                return View(db.tblUsers.Where(x => x.email.StartsWith(search) || search == null)
                   .ToList().ToPagedList(page ?? 1, 15));
            }
            else
            {
                return View(db.tblUsers.Where(x => x.roleid == 2).ToList().ToPagedList(page ?? 1, 15));
            }




        }



    




        // GET: Student_Details for student
        public ActionResult IndexforStudentsDetails(string searchBy, string search, int? page)
        {
            if (searchBy == "Id")
            {
                return View(db.Student_Details.Where(x => x.Student_id.ToString() == search).ToList()
                    .ToPagedList(page ?? 1, 10));
            }
                 else if (searchBy == "Gender")
            {
                return View(db.Student_Details.Where(x => x.Gender.ToString() == search).ToList()
                    .ToPagedList(page ?? 1, 10));
            }
            else
            {
                return View(db.Student_Details.Where(x => x.Student_Name.StartsWith(search) || search == null)
                    .ToList().ToPagedList(page ?? 1, 10));
            }
        }


        // GET: Student_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = await db.Student_Details.FindAsync(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Details);
        }

        // GET: Student_Details/Details/5
        public async Task<ActionResult> DetailsforStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = await db.Student_Details.FindAsync(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Details);
        }


        // GET: Student_Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Student_id,Student_Name,Gender,Date_Of_Birth,Program,Contact_No")] Student_Details student_Details)
        {
            if (ModelState.IsValid)
            {
                db.Student_Details.Add(student_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student_Details);
        }

        // GET: Student_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = await db.Student_Details.FindAsync(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Details);
        }

        // POST: Student_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Student_id,Student_Name,Gender,Date_Of_Birth,Program,Contact_No")] Student_Details student_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student_Details);
        }

        // GET: Student_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = await db.Student_Details.FindAsync(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Details);
        }
        
        // POST: Student_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student_Details student_Details = await db.Student_Details.FindAsync(id);
            db.Student_Details.Remove(student_Details);
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
