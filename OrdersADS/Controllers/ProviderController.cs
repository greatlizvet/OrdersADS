﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Models;
using OrdersADS.Infrastructure;

namespace OrdersADS.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProviderController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Provider
        public ActionResult Index(string searchName)
        {
            var provider = from p in db.Providers
                           select p;

            if(!String.IsNullOrEmpty(searchName))
            {
                provider = provider.Where(p => p.Name.Contains(searchName));
            }

            return View(provider);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Provider provider)
        {
            if(ModelState.IsValid)
            {
                db.Providers.Add(provider);
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
        public ActionResult Edit(Provider provider)
        {
            db.Entry(provider).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            Provider provider = db.Providers.Find(id);

            if(provider == null)
            {
                return HttpNotFound();
            }

            return View(provider);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Provider provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
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
            Provider provider = db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return action == 0 ? View("Details", provider) : View("Edit", provider);
        }
    }
}