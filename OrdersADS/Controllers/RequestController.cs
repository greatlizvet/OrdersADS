﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using OrdersADS.Infrastructure;
using OrdersADS.Models;

namespace OrdersADS.Controllers
{
    [Authorize(Roles = "ADS, Administrator")]
    public class RequestController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Request
        public ActionResult Index(string searchName, string zakaz, string status)
        {
            var zakazQry = from z in db.Requests
                           orderby z.Zakazchik.Name
                           select z.Zakazchik.Name;

            var statusQry = from s in db.Requests
                            orderby s.Status.StatusName
                            select s.Status.StatusName;

            var zakazList = new List<string>();
            zakazList.AddRange(zakazQry.Distinct());
            ViewData["zakaz"] = new SelectList(zakazList);

            var statList = new List<string>();
            statList.AddRange(statusQry.Distinct());
            ViewData["status"] = new SelectList(statList);

            var requests = from r in db.Requests
                           select r;

            if(!String.IsNullOrEmpty(searchName))
            {
                requests = requests.Where(r => r.Name.Contains(searchName));
            }

            if(!String.IsNullOrEmpty(zakaz))
            {
                requests = requests.Where(z => z.Zakazchik.Name == zakaz);
            }

            if(!String.IsNullOrEmpty(status))
            {
                requests = requests.Where(s => s.Status.StatusName == status);
            }

            return View(requests);
        }

        public ActionResult Create()
        {
            GetLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Request request, int[] selDet)
        {
            if(ModelState.IsValid)
            {
                //добавляем детали в заявку
                SetDetails(request, selDet);
                request.StatusId = 1;

                db.Requests.Add(request);
                db.SaveChanges();

                await Send(request.Id);
            }

            return RedirectToAction("Index");
        }

        private async Task Send(int id)
        {
            using (AppIdentityDbContext DB = new AppIdentityDbContext())
            {
                Request request = DB.Requests.Find(id);
                await SendMessage(request);
            }
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

        [HttpGet]
        public ActionResult Delete(int? id)
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

            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();

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

        public async Task<ActionResult> End(int? id)
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
            await db.SaveChangesAsync();

            await SendMessage(request);

            return RedirectToAction("Index");

        }

        public ActionResult Drop()
        {
            return RedirectToAction("Index");
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        private async Task SendMessage(Request request)
        {
            IEnumerable<AppUser> users = UserManager.Users;
            Mail statusMail = new Mail();
            foreach (var u in users)
            {
                await statusMail.Send(u.Email.ToString(), "Изменился статус",
                    "Статус заявки " + request.Name.ToString() + " был изменен на " + request.Status.StatusName);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}