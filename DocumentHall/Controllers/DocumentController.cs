using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DocumentHall.Models;
using System.IO;
using System.Data.SqlClient;
using DocumentHall.Filters;
using System.Web.Security;
using System.Linq.Expressions;
using System.Data;


namespace DocumentHall.Controllers
{
    [InitializeSimpleMembership]
    public class DocumentController : Controller
    {
        
        private Database1Entities1 db = new Database1Entities1();
        //
        // GET: /Default1/

        [Authorize(Roles = "Administrator,SuperUser")]
        public ActionResult Index()
        {
            return View(db.Document.ToList());
        }

        public ActionResult About()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            Random rnd = new Random();
            Roles.AddUserToRole(User.Identity.Name, "SuperUser");
            Roles.RemoveUserFromRole(User.Identity.Name, "User");
            var id1 = WebMatrix.WebData.WebSecurity.GetUserId(User.Identity.Name);
            int id = id1;
            Document document = new Document();
            document.Name = null;
            document.Serial = rnd.Next(999999);
            document.Country = null;
            document.DateOfBirth = null;
            document.DateOfRegistartion = DateTime.Now.Date;
            return View("Create", document);
        }
        [Authorize(Roles = "SuperUser")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Document.Add(document);
                db.SaveChanges();
                return RedirectToAction("About");
            }

            return View(document);
        }

        //
        // GET: /Doc/Details/5

        public ActionResult Details(int id = 0)
        {
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }


        //
        // GET: /Doc/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Doc/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }
 //
        // GET: /Doc/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Document document = db.Document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        //
        // POST: /Doc/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Document.Find(id);
            db.Document.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}