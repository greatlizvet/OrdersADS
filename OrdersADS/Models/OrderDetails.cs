using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Display(Name = "Заказ")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Display(Name = "Деталь")]
        public int DetailId { get; set; }
        public virtual Detail Detail { get; set; }

        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }
    }
}