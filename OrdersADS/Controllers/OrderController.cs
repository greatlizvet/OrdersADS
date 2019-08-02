using System;
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
    [Authorize(Roles = "Smeta, Administrator")]
    public class OrderController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Order
        public ActionResult Index()
        {
            return View(db.Orderes.ToList());
        }

        public ActionResult Create()
        {
            GetLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ordere order, int[] selDet)
        {
            if(ModelState.IsValid)
            {
                SetDetails(order, selDet);

                order.StatusOrderId = 1;

                db.Orderes.Add(order);
                await db.SaveChangesAsync();

                Request request = db.Requests.Find(order.RequestId);
                request.StatusId = 2;
                await db.SaveChangesAsync();
                await SendMessage(request);
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
        public ActionResult Edit(Ordere order, int[] selDet)
        {
            Ordere newOrder = db.Orderes.Find(order.Id);
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

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Ordere order = db.Orderes.Find(id);
            if(order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordere ordere = db.Orderes.Find(id);
            db.Orderes.Remove(ordere);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Buy(int? id)
        {
            Task<ActionResult> result = States(3, 5, id);

            return await result;
        }

        public async Task<ActionResult> OnWarehouse(int? id)
        {
            Task<ActionResult> result = States(4, 6, id);

            return await result;
        }

        private void GetLists()
        {
            var closeReq = db.Requests.Where(re => re.StatusId == 7);
            SelectList requests = new SelectList(db.Requests.Except(closeReq), "Id", "Name");
            SelectList providers = new SelectList(db.Providers, "Id", "Name");

            ViewBag.Requests = requests;
            ViewBag.Providers = providers;
            ViewBag.Details = db.Details.ToList();
        }

        private void SetDetails(Ordere order, int[] selDet)
        {
            if (selDet != null)
            {
                foreach (var d in db.Details.Where(de => selDet.Contains(de.Id)))
                {
                    order.Details.Add(d);
                }
            } 
        }

        private async Task<ActionResult> States(int statusOrder, int statusRequest, int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Ordere order = db.Orderes.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.StatusOrderId = statusOrder;
            order.Request.StatusId = statusRequest;
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            await SendMessage(order.Request);

            return RedirectToAction("Index");
        }

        private ActionResult Find(int? id, int action)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Ordere order = db.Orderes.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return action == 0 ? View("Details", order) : View("Edit", order);
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
    }
}