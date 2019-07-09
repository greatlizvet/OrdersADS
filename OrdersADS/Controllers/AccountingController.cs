using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    public class AccountingController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Accounting
        public ActionResult Index()
        {
            return View(db.Requests.ToList());
        }

        public ActionResult Send(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Request request = db.Requests.Find(id);
            if(request == null)
            {
                return HttpNotFound();
            }
            request.StatusId = 3;
            db.Entry(request).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Pay(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            request.StatusId = 4;
            
            db.Entry(request).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            foreach (var r in db.Orders.Where(re => re.RequestId == request.Id))
            {
                r.StatusOrderId = 2;
                db.Entry(r).State = System.Data.Entity.EntityState.Modified;
            }

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