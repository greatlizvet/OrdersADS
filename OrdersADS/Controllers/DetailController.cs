﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Models;
using OrdersADS.Infrastructure;
using System.Threading.Tasks;

namespace OrdersADS.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DetailController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Detail
        public ActionResult Index(string searchName)
        {
            var details = from d in db.Details
                          select d;

            if(!String.IsNullOrEmpty(searchName))
            {
                details = details.Where(s => s.Name.Contains(searchName));
            }

            return View(details);
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
            ActionResult result = Find(id, 0);

            return result;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ActionResult result = Find(id, 1);

            return result;
        }

        [HttpPost]
        public ActionResult Edit(Detail detail)
        {
            db.Entry(detail).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Detail detail = db.Details.Find(id);
            db.Details.Remove(detail);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Drop()
        {
            return RedirectToAction("Index");
        }

        private ActionResult Find(int? id, int action)
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
            return action == 0 ? View("Details", detail) : View("Edit", detail);
        }
    }
}