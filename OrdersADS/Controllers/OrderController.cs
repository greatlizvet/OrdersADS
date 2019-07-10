﻿using System;
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

                Request request = db.Requests.Find(order.Id);
                request.StatusId = 2;
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
            GetLists();
            ActionResult result = Find(id, 1);

            return result;
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
            newOrder.Request.StatusId = 2;

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

        public ActionResult Buy(int? id)
        {
            ActionResult result = States(3, 5, id);

            return result;
        }

        public ActionResult OnWarehouse(int? id)
        {
            ActionResult result = States(4, 6, id);

            return result;
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
                    db.OrderDetails.Add(new OrderDetails
                    {
                        OrderId = order.Id,
                        DetailId = d.Id,
                        Count = 0,
                        Price = 0
                    });
                }
            } 
        }

        private ActionResult States(int statusOrder, int statusRequest, int? id)
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

            order.StatusOrderId = statusOrder;
            order.Request.StatusId = statusRequest;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        private ActionResult Find(int? id, int action)
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

            return action == 0 ? View("Details", order) : View("Edit", order);
        }
    }
}