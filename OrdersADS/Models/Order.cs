using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        //id детали, которую заказывают и нав поле
        [Display(Name = "Деталь")]
        public int DetailId { get; set; }
        public virtual Detail Detail { get; set; }

        [Display(Name = "Количество")]
        //кол-во деталей
        public int Count { get; set; }

        [Display(Name = "Статус заказа")]
        //статус заказа 
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}