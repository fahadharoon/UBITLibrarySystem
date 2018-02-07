using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UbitLibrarySystem.Models;

namespace UbitLibrarySystem.Controllers
{
    public class HomeController : Controller
    {

        LMSContext db = new LMSContext();


        public ActionResult Index()
        {

            return View();
        }
        public JsonResult GetEvents()

        {

            {

                var events = db.Events.ToList();

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }

        }

        [HttpPost]

        public JsonResult SaveEvent(Event e)

        {

            var status = false;

            {

                if (e.EventID > 0)

                {

                    //Update the event

                    var v = db.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();

                    if (v != null)

                    {

                        v.Subject = e.Subject;

                        v.Start = e.Start;

                        v.End_Time = e.End_Time;

                        v.Description = e.Description;

                        v.IsFullDay = e.IsFullDay;

                        v.ThemeColor = e.ThemeColor;

                    }

                }

                else

                {

                    db.Events.Add(e);

                }

                db.SaveChanges();

                status = true;

            }


            return new JsonResult { Data = new { status = status } };

        }
             [HttpPost]

        public JsonResult DeleteEvent(int eventID)

        {

            var status = false;


            {

                var v = db.Events.Where(a => a.EventID == eventID).FirstOrDefault();

                if (v != null)

                {

                    db.Events.Remove(v);

                    db.SaveChanges();

                    status = true;

                }

            }

            return new JsonResult { Data = new { status = status } };

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Student Registration
        //public ActionResult StudentRegistration()

        //{
        //    int id = 1;

        //    ViewBag.tblrole = db.tblroles.Where(i => i.roleId==id).ToList();
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult StudentRegistration(tblUser user )
        //{
            
        //    if(ModelState.IsValid)
        //    {
        //        tblUser log = new tblUser();
        //        log.email = user.email;
        //        log.password = user.password;
        //        log.confirm_password = user.confirm_password;
        //        log.roleid = user.roleid;
        //        db.tblUsers.Add(log);
        //        db.SaveChanges();
        //        return RedirectToAction("StudentRegistration");
        //    }
        //    ViewBag.tblrole = db.tblroles.ToList();
        //    return View(user);
        //}


      


        //Student Login


        //public ActionResult StudentLogin()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult StudentLogin(tblUser user)
        //{


        //    var result = db.tblUsers.Where(i => i.email == user.email && i.password == user.password).ToList();
        //    if (result.Count() > 0)
        //    {
        //        Session["id"] = result[0].id;
        //        FormsAuthentication.SetAuthCookie(result[0].email, false);

                //if Admin
                //if (result[0].roleid == 1)
                //{
                //    return RedirectToAction("Index/Home");
                //}
                //if User
            //    if (result[0].roleid == 2)
            //    {
            //        return RedirectToAction("../User/Index");
            //    }
            //}
        //    else
        //    {
        //        ViewBag.Message = "Incorrect Username Or Password";
        //    }
        //    return View(user);
        //}


       
    }
}