using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Models;
using OrdersADS.Infrastructure;

namespace OrdersADS.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StatusController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Status
        public ActionResult Index()
        {
            return View(db.Statuses.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Status status)
        {
            if(ModelState.IsValid)
            {
                db.Statuses.Add(status);
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
            Status status = db.Statuses.Find(id);
            if(status == null)
            {
                return HttpNotFound();
            }

            return View(status);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Status status = db.Statuses.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }

            return View(status);
        }

        [HttpPost]
        public ActionResult Edit(Status status)
        {
            db.Entry(status).State = System.Data.Entity.EntityState.Modified;
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
            Status status = db.Statuses.Find(id);
            if(status != null)
            {
                db.Statuses.Remove(status);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}