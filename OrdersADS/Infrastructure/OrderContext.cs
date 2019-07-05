using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrdersADS.Models;
using System.Data.Entity;

namespace OrdersADS.Infrastructure
{
    public class OrderContext : DbContext
    {
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Smeta> Smetas { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}