using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OrdersADS.Infrastructure;
using OrdersADS.Models;
using System.Net.Mail;

namespace OrdersADS.Controllers
{
    public class AccountingController : Controller
    {
        AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: Accounting
        public async Task<ActionResult> Index()
        {
            return View(await db.Requests.ToListAsync());
        }

        public async Task<ActionResult> Send(int? id)
        {
            Task<ActionResult> result = SendPay(id, 3, 0);

            return await result;
        }

        public async Task<ActionResult> Pay(int? id)
        {
            Task<ActionResult> result = SendPay(id, 4, 1);

            return await result;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private async Task<ActionResult> SendPay(int? id, int status, int action)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            request.StatusId = status;

            db.Entry(request).State = EntityState.Modified;
            await db.SaveChangesAsync();

            if(action == 1)
            {
                foreach (var r in db.Orders.Where(re => re.RequestId == request.Id))
                {
                    r.StatusOrderId = 2;
                    db.Entry(r).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}