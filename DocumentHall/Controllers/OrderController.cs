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
    public class OrderController : Controller
    {

        private Database1Entities1 db = new Database1Entities1();
        const string connectionString = @"data source=(LocalDB)\v11.0;attachdbfilename=|DataDirectory|\Database1.mdf;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework";
        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View(db.Order.ToList());
        }

        public ActionResult ApplyOrder(int id)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var qry = "UPDATE [Order] SET control = @control where Id=" + id;
                var cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@control", "check");
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CancelOrder(int id)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var qry = "DELETE [Order] where Id=" + id;
                var cmd = new SqlCommand(qry, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Oplata(int id)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var qry = "UPDATE [Order] SET control = @control where Id=" + id;
                var cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@control", "oplata");
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            return RedirectToAction("Index");
        }

        public void SetControl(int id)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var qry = "Update [Order] SET control = @control where Id=" + id;
                var cmd = new SqlCommand(qry, cn);
                cmd.Parameters.AddWithValue("@control", "uncheck");
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        [Authorize(Roles = "User,SuperUser")]
        public ActionResult Create()
        {
            var id1 = WebMatrix.WebData.WebSecurity.GetUserId(User.Identity.Name);
            int id = id1;

            Random rnd = new Random();

            Order order = new Order();
            order.Numer = rnd.Next(999999);
            order.Control = "uncheck";
            ViewBag.TypeOrder = new SelectList(db.OrderInfo, "Type", "Type");
            order.UserId_ = id;
            ViewBag.DocId = new SelectList(db.Document, "Id", "Name");
            order.Date = DateTime.Now.Date;
            return View("Create", order);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocId = new SelectList(db.Document, "Id", "Name", order.DocId);
            ViewBag.TypeOrder = new SelectList(db.OrderInfo, "Type", "Type", order.TypeOrder);
            return View(order);
        }
        //
        // GET: /Or/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        //
        // GET: /Or/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocId = new SelectList(db.Document, "Id", "Name", order.DocId);
            ViewBag.TypeOrder = new SelectList(db.OrderInfo, "Type", "Type", order.TypeOrder);
            return View(order);
        }

        //
        // POST: /Or/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocId = new SelectList(db.Document, "Id", "Name", order.DocId);
            ViewBag.TypeOrder = new SelectList(db.OrderInfo, "Type", "Type", order.TypeOrder);
            return View(order);
        }

        //
        // GET: /Or/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Or/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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