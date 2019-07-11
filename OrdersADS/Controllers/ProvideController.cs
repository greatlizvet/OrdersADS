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
    public class ProvideController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Provide
        public ActionResult Index()
        {
            //выводит список заявок
            return View(db.Requests.ToList());
        }

        public async Task<ActionResult> InAccounting(int? id)
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
            await db.SaveChangesAsync();

            await SendMessage(request);

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
    }
}