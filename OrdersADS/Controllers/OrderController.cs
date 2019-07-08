using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    public class OrderController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Order
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult Create()
        {
            GetLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order, int[] selDet)
        {
            if(ModelState.IsValid)
            {
                SetDetails(order, selDet);

                order.StatusOrderId = 1;

                db.Orders.Add(order);
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
            Order order = db.Orders.Find(id);
            if(order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            GetLists();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order, int[] selDet)
        {
            Order newOrder = db.Orders.Find(order.Id);
            newOrder.Name = order.Name;
            newOrder.dateTime = order.dateTime;
            newOrder.ProviderId = order.ProviderId;
            newOrder.RequestId = order.RequestId;

            newOrder.Details.Clear();
            SetDetails(newOrder, selDet);

            db.Entry(newOrder).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Order order = db.Orders.Find(id);
            if(order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private void GetLists()
        {
            SelectList requests = new SelectList(db.Requests, "Id", "Name");
            SelectList providers = new SelectList(db.Providers, "Id", "Name");

            ViewBag.Requests = requests;
            ViewBag.Providers = providers;
            ViewBag.Details = db.Details.ToList();
        }

        private void SetDetails(Order order, int[] selDet)
        {
            if (selDet != null)
            {
                foreach (var d in db.Details.Where(de => selDet.Contains(de.Id)))
                {
                    order.Details.Add(d);
                }
            }
        }
    }
}