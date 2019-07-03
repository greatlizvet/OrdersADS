using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrdersADS.Models;

namespace OrdersADS.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base ("IdentityDb") { }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
}