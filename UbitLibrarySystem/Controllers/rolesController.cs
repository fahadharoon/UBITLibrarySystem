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

namespace UbitLibrarySystem.Controllers
{
    public class rolesController : Controller
    {
        private LMSContext db = new LMSContext();

        // GET: roles
        public async Task<ActionResult> Index()
        {
            return View(await db.tblroles.ToListAsync());
        }

        // GET: roles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblrole tblrole = await db.tblroles.FindAsync(id);
            if (tblrole == null)
            {
                return HttpNotFound();
            }
            return View(tblrole);
        }

        // GET: roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "roleId,roleName")] tblrole tblrole)
        {
            if (ModelState.IsValid)
            {
                db.tblroles.Add(tblrole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblrole);
        }

        // GET: roles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblrole tblrole = await db.tblroles.FindAsync(id);
            if (tblrole == null)
            {
                return HttpNotFound();
            }
            return View(tblrole);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "roleId,roleName")] tblrole tblrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblrole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblrole);
        }

        // GET: roles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblrole tblrole = await db.tblroles.FindAsync(id);
            if (tblrole == null)
            {
                return HttpNotFound();
            }
            return View(tblrole);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            tblrole tblrole = await db.tblroles.FindAsync(id);
            db.tblroles.Remove(tblrole);
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
