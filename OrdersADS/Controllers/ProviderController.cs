﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Models;
using OrdersADS.Infrastructure;

namespace OrdersADS.Controllers
{
    public class ProviderController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Provider
        public ActionResult Index()
        {
            return View(db.Providers.ToList());
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

        [HttpGet]
        public ActionResult Edit(int? id)
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
            return View(provider);
        }

        [HttpPost]
        public ActionResult Edit(Provider provider)
        {
            db.Entry(provider).State = System.Data.Entity.EntityState.Modified;
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
            Provider provider = db.Providers.Find(id);
            if(provider != null)
            {
                db.Providers.Remove(provider);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}