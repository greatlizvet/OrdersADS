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

        //id детали, которую заказывают и нав поле
        [Display(Name = "Деталь")]
        public int DetailId { get; set; }
        public Detail Detail { get; set; }

        [Display(Name = "Количество")]
        //кол-во деталей
        public int Count { get; set; }

        [Display(Name = "Статус заказа")]
        //статус заказа 
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}