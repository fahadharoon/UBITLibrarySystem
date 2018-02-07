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
using System.IO;
using System.Text;

namespace UbitLibrarySystem.Controllers
{
    public class Book_DetailsController : Controller
    {
        private LMSContext db = new LMSContext();

        // GET: Book_Detailsforadmin
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        { 
            //sorting
            ViewBag.SortTitleParameter = string.IsNullOrEmpty(sortBy) ? "Title desc" : "" ;
            ViewBag.SortAuthorParameter = string.IsNullOrEmpty(sortBy) ? "Author desc" : "" ;
            //ViewBag.SortAuthorParameter = sortBy== "Gender" ? "Gender desc " : "Gender";

            var bookDetails = db.Book_Details.AsQueryable();
            //searching
            if (searchBy == "Title")
            {
                var book_Details = db.Book_Details.Include(b => b.Category_Details).Include(b => b.Publication_Details);
                //return View(book_Details.Where(x => x.Book_Title.StartsWith(search) || search == null)
                //    .ToList().ToPagedList(page ?? 1, 10)

                //    );
                bookDetails = book_Details.Where(x => x.Book_Title.StartsWith(search) || search == null);

            }
            else if (searchBy == "Category")
            {
                var book_Details = db.Book_Details.Include(b => b.Category_Details).Include(b => b.Publication_Details);
                bookDetails = book_Details.Where(x => x.Category_Details.Category_Name.StartsWith(search) || search == null);

            }
            else
            {
                var book_Details = db.Book_Details.Include(b => b.Category_Details).Include(b => b.Publication_Details);
                //return View(book_Details.Where(x => x.Book_Author.StartsWith(search) || search == null)
                //    .ToList().ToPagedList(page ?? 1, 10));

               

                bookDetails = book_Details.Where(x => x.Book_Author.StartsWith(search) || search == null);

            }
            switch (sortBy)
            {
                case "Title desc" :
                    bookDetails = bookDetails.OrderByDescending(x => x.Book_Title);
                    break;

                case "Author desc" :
                    bookDetails = bookDetails.OrderByDescending(x => x.Book_Author);
                    break;
                default :
                    bookDetails = bookDetails.OrderBy(x => x.Book_Title);
                    break;
            }

            return View(bookDetails.ToPagedList(page ?? 1, 30));

        }
        // GET: Book_Details for student
        public ActionResult IndexforStudentBookDetails(string searchBy, string search, int? page,string sortBy)
        {

            //sorting
            ViewBag.SortTitleParameter = string.IsNullOrEmpty(sortBy) ? "Title desc" : "";
            ViewBag.SortAuthorParameter = string.IsNullOrEmpty(sortBy) ? "Author desc" : "";
            //ViewBag.SortAuthorParameter = sortBy== "Gender" ? "Gender desc " : "Gender";
            var bookDetails = db.Book_Details.AsQueryable();
            if (searchBy == "Title")
            {
                var book_Details = db.Book_Details.Include(b => b.Category_Details).Include(b => b.Publication_Details);
                bookDetails = book_Details.Where(x => x.Book_Title.StartsWith(search) || search == null);
                    

            }

            else if(searchBy == "Category")
            {
                var book_Details = db.Book_Details.Include(b => b.Category_Details).Include(b => b.Publication_Details);
                bookDetails = book_Details.Where(x => x.Category_Details.Category_Name.StartsWith(search) || search == null);

            }








            else
            {
                var book_Details = db.Book_Details.Include(b => b.Category_Details).Include(b => b.Publication_Details);
                bookDetails = book_Details.Where(x => x.Book_Author.StartsWith(search) || search == null);
                  
            }
            foreach (var item in bookDetails)
            {
                string partialName = "~" + item.ISBN_Code.ToString() + "~";
                var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/UploadTitleImage/"));
                System.IO.FileInfo[] fileNames = dir.GetFiles("*" + partialName + "*.*");

                if(fileNames != null && fileNames.Length > 0)
                {
                    string fileName = fileNames[0].ToString();
                    var filePath = Server.MapPath("~/App_Data/UploadTitleImage/" + fileName);
                    var base64Path = ConvertImageURLToBase64(filePath);
                    //item.Book_Author = base64Path;
                    item.ImagePath = base64Path;
                }
                
            }
            switch (sortBy)
            {
                //case "Title desc":
                //    bookDetails = bookDetails.OrderByDescending(x => x.Book_Title);
                //    break;

                //case "Author desc":
                //    bookDetails = bookDetails.OrderByDescending(x => x.Book_Author);
                //    break;
                default:
                    bookDetails = bookDetails.OrderBy(x => x.Book_Title);
                    break;
            }

            return View(bookDetails.ToPagedList(page ?? 1, 30));


        }

        [HttpPost]
        public ActionResult GetBookDetails(int isbn)
        {
            Book_Details book = new Book_Details();
            book = db.Book_Details.Where(b=> b.ISBN_Code == isbn).FirstOrDefault();
            string partialName = "~" + book.ISBN_Code.ToString() + "~";
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/UploadTitleImage/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*" + partialName + "*.*");

            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0].ToString();
                var filePath = Server.MapPath("~/App_Data/UploadTitleImage/" + fileName);
                var base64Path = ConvertImageURLToBase64(filePath);
                //item.Book_Author = base64Path;
                book.ImagePath = base64Path;
            }
            return PartialView("GetBookDetails", book);
        }

        //convert image url to base64
        public static String ConvertImageURLToBase64(String url)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("data:image/png;base64,");
            Byte[] _byte = System.IO.File.ReadAllBytes(url);
            //Byte[] _byte = GetImage(url);
            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
            return _sb.ToString();
        }

        //get image from ImageUrl using webrequest
        private static byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }



        // GET: Book_Details/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Details book_Details = await db.Book_Details.FindAsync(id);
            if (book_Details == null)
            {
                return HttpNotFound();
            }
            return View(book_Details);
        }
        //User category
        public ActionResult Category(int id)
        {
            var category = db.Category_Details.Find(id).Book_Details.ToList();
            return View(category);
        }

        // GET: Book_Details/Details/5 for students
        public async Task<ActionResult> DetailsforStudentBookDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Details book_Details = await db.Book_Details.FindAsync(id);
            if (book_Details == null)
            {
                return HttpNotFound();
            }
            return View(book_Details);
        }




        // GET: Book_Details/Create
        public ActionResult Create()
        {
            ViewBag.Category_id = new SelectList(db.Category_Details, "Category_id", "Category_Name");
            ViewBag.Publication_id = new SelectList(db.Publication_Details, "Publication_id", "Publisher_Name");
            return View();
        }

        // POST: Book_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ISBN_Code,Book_Title,Book_Author,Publication_id,Category_id,No_of_Copies_Actual,No_of_Copies_Available,Edition")] Book_Details book_Details)
        {
            if (ModelState.IsValid)
            {
                db.Book_Details.Add(book_Details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Category_id = new SelectList(db.Category_Details, "Category_id", "Category_Name", book_Details.Category_id);
            ViewBag.Publication_id = new SelectList(db.Publication_Details, "Publication_id", "Publisher_Name", book_Details.Publication_id);
            return View(book_Details);
        }

        // GET: Book_Details/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Details book_Details = await db.Book_Details.FindAsync(id);
            if (book_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_id = new SelectList(db.Category_Details, "Category_id", "Category_Name", book_Details.Category_id);
            ViewBag.Publication_id = new SelectList(db.Publication_Details, "Publication_id", "Publisher_Name", book_Details.Publication_id);
            return View(book_Details);
        }

        // POST: Book_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ISBN_Code,Book_Title,Book_Author,Publication_id,Category_id,No_of_Copies_Actual,No_of_Copies_Available,Edition")] Book_Details book_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_Details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Category_id = new SelectList(db.Category_Details, "Category_id", "Category_Name", book_Details.Category_id);
            ViewBag.Publication_id = new SelectList(db.Publication_Details, "Publication_id", "Publisher_Name", book_Details.Publication_id);
            return View(book_Details);
        }

        // GET: Book_Details/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Details book_Details = await db.Book_Details.FindAsync(id);
            if (book_Details == null)
            {
                return HttpNotFound();
            }
            return View(book_Details);
        }

        // POST: Book_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book_Details book_Details = await db.Book_Details.FindAsync(id);
            db.Book_Details.Remove(book_Details);
            await db.SaveChangesAsync();

            string partialName = "~" + id.ToString() + "~";
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/UploadedBooks/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*" + partialName + "*.*");

            if (fileNames != null && fileNames.Count() > 0)
            {
                string fileName = fileNames[0].ToString();
                var filePath = "~/App_Data/UploadedBooks/" + fileName;
                var fullPath = Request.MapPath(filePath);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

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

        public ActionResult Upload(int id)
        {
            ViewData["ISBN_Code"] = id;

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int ISBN_Code)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "~" + ISBN_Code.ToString() + "~" + Path.GetExtension(file.FileName);
                //fileName = fileName + "~" + ISBN_Code.ToString() + "~";
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/UploadedBooks"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("BookUploaded");
        }




        // private bool isValidContentType(string contentType)
        //{
        //    return contentType.Equals("image/png") ||
        //        contentType.Equals("image/jpeg") ||
        //        contentType.Equals("image/gif") ||
        //        contentType.Equals("image.jpg");
        //}

        //public ActionResult UploadTitleImage(HttpPostedFileBase PostedFile)
        //    {
        //    if(!isValidContentType(PostedFile.ContentType))
        //    {
        //        ViewBag.Error = "only JPG,JPEG,PNG & GIF files are allowed.";
        //        return View("Index");
        //    }
        //    return View("ImageUploaded");
        //    }
        //upload title image
        [HttpPost]
        public ActionResult UploadTitleImage(HttpPostedFileBase file, int ISBN_Code)
        {

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "~" + ISBN_Code.ToString() + "~" + Path.GetExtension(file.FileName);
                //fileName = fileName + "~" + ISBN_Code.ToString() + "~";
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/UploadTitleImage/"), fileName);
                //var filePath = "~/App_Data/UploadTitleImage/" + fileName;

                file.SaveAs(path);
                ViewBag.fileName = file.FileName;


            }

            // redirect back to the index action to show the form once again
            return RedirectToAction("ImageUploaded");
        }



        public ActionResult UploadTitleImage(int id)
        {
            ViewData["ISBN_Code"] = id;

            return View();
        }


        //[HttpPost]
        public ActionResult Download(int id)





        {
            string partialName = "~" + id.ToString() + "~";
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/UploadedBooks/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*" + partialName + "*.*");
            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0].ToString();
                var filePath = "~/App_Data/UploadedBooks/" + fileName;
                return File(filePath, "application/force-download", Path.GetFileName(filePath));
            }
            else
            {
                TempData["Action"] = "Index";

                return RedirectToAction("NoBookUploaded");
            }
        }

        //[HttpPost]
        public ActionResult DownloadforStudent(int id)



        {
            string partialName = "~" + id.ToString() + "~";
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/UploadedBooks/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*" + partialName + "*.*");

            if (fileNames != null && fileNames.Length > 0)
            {
                string fileName = fileNames[0].ToString();
                var filePath = "~/App_Data/UploadedBooks/" + fileName;
                return File(filePath, "application/force-download", Path.GetFileName(filePath));
            }
            else
            {
               TempData["Action"] = "IndexforStudentBookDetails";
                    return RedirectToAction("NoBookUploaded");
            }
        }





        public ActionResult BookUploaded()
        {
            return View();
        }


        public ActionResult ImageUploaded()
        {
            return View();
        }



        public ActionResult NoBookUploaded()
        {
            return View();
        }
    }



}




 