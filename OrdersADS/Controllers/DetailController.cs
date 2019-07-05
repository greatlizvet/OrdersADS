using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Models;
using OrdersADS.Infrastructure;

namespace OrdersADS.Controllers
{
    public class DetailController : Controller
    {
        OrderContext db = new OrderContext();
        // GET: Detail
        public ActionResult Index()
        {
            return View(db.Details.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Detail detail)
        {
            if(ModelState.IsValid)
            {
                db.Details.Add(detail);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Detail detail = db.Details.Find(id);
            if(detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpPost]
        public ActionResult Edit(Detail detail)
        {
            db.Entry(detail).State = System.Data.Entity.EntityState.Modified;
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
            Detail detail = db.Details.Find(id);
            if(detail != null)
            {
                db.Details.Remove(detail);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}