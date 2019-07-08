using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Request
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Display(Name = "Заказчик")]
        public int ZakazchikId { get; set; }
        public virtual Zakazchik Zakazchik { get; set; }

        [Display(Name = "Детали")]
        public virtual ICollection<Detail> Details { get; set; }

        public Request()
        {
            Details = new List<Detail>();
        }
    }
}