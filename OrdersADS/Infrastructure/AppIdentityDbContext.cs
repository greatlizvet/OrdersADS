using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrdersADS.Models;
using System.Data.Entity;

namespace OrdersADS.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base ("OrderContext") { }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        public DbSet<Detail> Details { get; set; }
        public DbSet<Ordere> Orderes { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StatusOrder> StatusOrders { get; set; }
        public DbSet<Zakazchik> Zakazchiks { get; set; }
    }
}