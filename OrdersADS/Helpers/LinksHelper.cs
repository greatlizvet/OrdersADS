using OrdersADS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using OrdersADS.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrdersADS
{
    public static class LinksHelper
    {
        public static MvcHtmlString CreateLinks(this HtmlHelper html, IPrincipal User, string ulClass)
        {
            if (html == null)
            {
                throw new ArgumentNullException(nameof(html));
            }

            AppIdentityDbContext db = new AppIdentityDbContext();

            AppUser user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            TagBuilder ul = new TagBuilder("ul");
            ICollection<IdentityUserRole> roles = new List<IdentityUserRole>();
            List<string> roleName = new List<string>();

            if(user != null)
            {
                roles = user.Roles;
            }

            foreach(var r in roles)
            {
                switch(r.ToString())
                {
                    case "Administrator":
                        TagBuilder li = new TagBuilder("li");
                        TagBuilder a = new TagBuilder("a");
                        a.MergeAttribute("href", "/admin");
                        a.SetInnerText("админка");
                        li.InnerHtml += a.ToString();
                        ul.InnerHtml += li.ToString();
                        break;
                }
            }

            ul.MergeAttribute("class", ulClass);

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}