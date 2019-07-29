using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    [Authorize(Roles = "Administrator, Smeta")]
    public class ZakazchikController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Zakazchik
        public ActionResult Index()
        {
            return View(db.Zakazchiks.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Zakazchik zakazchik)
        {
            if(ModelState.IsValid)
            {
                db.Zakazchiks.Add(zakazchik);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            ActionResult result = Find(id, 0);

            return result;
        }

        public ActionResult Edit(int? id)
        {
            ActionResult result = Find(id, 1);

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Zakazchik zakazchik)
        {
            db.Entry(zakazchik).State = System.Data.Entity.EntityState.Modified;
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
            Zakazchik zakazchik = db.Zakazchiks.Find(id);
            if(zakazchik != null)
            {
                db.Zakazchiks.Remove(zakazchik);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private ActionResult Find(int? id, int action)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Zakazchik zakazchik = db.Zakazchiks.Find(id);
            if (zakazchik == null)
            {
                return HttpNotFound();
            }
            return action == 0 ? View("Details", zakazchik) : View("Edit", zakazchik);
        }
    }
}