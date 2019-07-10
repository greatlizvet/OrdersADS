using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    [Authorize(Roles = "ADS")]
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
            ActionResult result = Find(id, 0);

            return result;
        }

        public ActionResult Edit(int? id)
        {
            ActionResult result = Find(id, 1);
            GetLists();

            return result;
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

        private ActionResult Find(int? id, int action)
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
            return action == 0 ? View("Details", request) : View("Edit", request);
        }

        public ActionResult End(int? id)
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

            request.StatusId = 7;
            db.Entry(request).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}