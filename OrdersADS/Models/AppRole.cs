using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersADS.Models
{
    public class AppRole : IdentityRole
    {
        //конструктор по умоллчанию
        public AppRole() : base() { }

        //параметрический, в который передается название роли
        public AppRole(string name) 
            : base (name)
        { }
    }
}