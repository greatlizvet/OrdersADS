using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrdersADS.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace OrdersADS.Infrastructure
{
    public class AppRoleManager : RoleManager<AppRole>, IDisposable
    {
        public AppRoleManager(RoleStore<AppRole> store) 
            : base(store)
        { }

        public static AppRoleManager Create(
            IdentityFactoryOptions<AppRoleManager> options, 
            IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<AppRole>(context.Get<AppIdentityDbContext>()));
        }
    }
}