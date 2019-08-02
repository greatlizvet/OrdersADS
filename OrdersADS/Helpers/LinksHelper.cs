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
            Tag tag = new Tag();
            ICollection<IdentityUserRole> roles = new List<IdentityUserRole>();

            if(user != null)
            {
                roles = user.Roles;
            }

            foreach(var r in roles)
            {
                string roleName = db.Roles.Find(r.RoleId).Name;
                switch(roleName)
                {
                    case "Administrator":
                        ul.InnerHtml += tag.CreateTags("/admin", "Панель администратора").ToString();
                        break;
                    case "ADS":
                        ul.InnerHtml += tag.CreateTags("/Request/Index", "Заявки: АДС").ToString();
                        break;
                    case "Accounting":
                        ul.InnerHtml += tag.CreateTags("/Accounting/Index", "Заявки: Бухгалтерия").ToString();
                        break;
                    case "Smeta":
                        ul.InnerHtml += tag.CreateTags("/Order/Index", "Заказы").ToString();
                        ul.InnerHtml += tag.CreateTags("/Provide/Index", "Заявки: На оплату");
                        ul.InnerHtml += tag.CreateTags("/Zakazchik/Index", "Заказчики");
                        break;

                }
            }

            ul.MergeAttribute("class", ulClass);

            return MvcHtmlString.Create(ul.ToString());
        }
    }

    public class Tag
    {
        public TagBuilder CreateTags(string link, string innerText)
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", link);
            a.SetInnerText(innerText);
            li.InnerHtml += a.ToString();

            return li;
        }
    }
}