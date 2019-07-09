using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    public class ProvideController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Provide
        public ActionResult Index()
        {
            //выводит список заявок
            return View(db.Requests.ToList());
        }

        public ActionResult InAccounting(int? id)
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

            request.StatusId = 8;
            db.Entry(request).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}