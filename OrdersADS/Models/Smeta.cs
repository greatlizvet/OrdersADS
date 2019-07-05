using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Smeta
    {
        public int Id { get; set; }
        
        [Display(Name ="Заказ")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Display(Name = "Стоимость детали")]
        public double DetailsCost { get; set; }

        [Display(Name = "Стоимость работ")]
        public double WorksCost { get; set; }

        [Display(Name ="Стоимость доставки")]
        public double? AutoCost { get; set; }

        [Display(Name = "Стоимость материалов")]
        public double MaterailCost { get; set; }

        [Display(Name = "Итого")]
        public double Itogo { get; set; }
    }
}