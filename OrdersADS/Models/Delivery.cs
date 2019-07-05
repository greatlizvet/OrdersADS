using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        [Display(Name = "Заказ")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        [Display(Name = "Стоимость")]
        public int Cost { get; set; }
    }
}