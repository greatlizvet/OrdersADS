using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    public class RequestController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Request
        public ActionResult Index()
        {
            return View(db.Requests.ToList());
        }

        public ActionResult Create()
        {
            GetLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Request request, int[] selDet)
        {
            if(ModelState.IsValid)
            {
                //добавляем детали в заявку
                SetDetails(request, selDet);
                request.StatusId = 1;

                db.Requests.Add(request);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
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

            return View(request);
        }

        public ActionResult Edit(int? id)
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
            GetLists();

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Request request, int[] selDet)
        {
            Request newRequest = db.Requests.Find(request.Id);
            newRequest.Name = request.Name;
            newRequest.ZakazchikId = request.ZakazchikId;

            newRequest.Details.Clear();
            SetDetails(newRequest, selDet);

            db.Entry(newRequest).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Request request = db.Requests.Find(id);
            if(request != null)
            {
                db.Requests.Remove(request);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private void GetLists()
        {
            SelectList zakazchiks = new SelectList(db.Zakazchiks, "Id", "Name");
            ViewBag.Zakazchiks = zakazchiks;
            ViewBag.Details = db.Details.ToList();
        }

        private void SetDetails(Request request, int[] selDet)
        {
            if (selDet != null)
            {
                foreach (var d in db.Details.Where(de => selDet.Contains(de.Id)))
                {
                    request.Details.Add(d);
                }
            }
        }
    }
}