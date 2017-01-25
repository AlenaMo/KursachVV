using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentHall.Models;

namespace DocumentHall.Controllers
{
    public class OrderinfController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        //
        // GET: /Orderinf/

        public ActionResult Index()
        {
            return View(db.OrderInfo.ToList());
        }

        //
        // GET: /Orderinf/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Orderinf/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderInfo orderinfo)
        {
            if (ModelState.IsValid)
            {
                db.OrderInfo.Add(orderinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderinfo);
        }

        //
        // GET: /Orderinf/Edit/5

        public ActionResult Edit(string id = null)
        {
            OrderInfo orderinfo = db.OrderInfo.Find(id);
            if (orderinfo == null)
            {
                return HttpNotFound();
            }
            return View(orderinfo);
        }

        //
        // POST: /Orderinf/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderInfo orderinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderinfo);
        }

    
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}