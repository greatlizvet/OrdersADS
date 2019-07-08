using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Models;
using OrdersADS.Infrastructure;

namespace OrdersADS.Controllers
{
    public class StatusOrderController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: StatusOrder
        public ActionResult Index()
        {
            return View(db.StatusOrders.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StatusOrder statusOrder)
        {
            if(ModelState.IsValid)
            {
                db.StatusOrders.Add(statusOrder);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            StatusOrder statusOrder = db.StatusOrders.Find(id);
            if(statusOrder == null)
            {
                return HttpNotFound();
            }

            return View(statusOrder);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            StatusOrder statusOrder = db.StatusOrders.Find(id);
            if (statusOrder == null)
            {
                return HttpNotFound();
            }

            return View(statusOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StatusOrder statusOrder)
        {
            db.Entry(statusOrder).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            StatusOrder statusOrder = db.StatusOrders.Find(id);
            if(statusOrder != null)
            {
                db.StatusOrders.Remove(statusOrder);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}