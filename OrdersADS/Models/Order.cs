﻿using System;
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

        [Display(Name = "Заказ")]
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }

        [Display(Name = "Дата оформления")]
        [DataType(DataType.DateTime)]
        public DateTime dateTime { get; set; }

        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        public ICollection<Detail> Details { get; set; }

        [Display(Name = "Статус")]
        public int StatusOrderId { get; set; }
        public virtual StatusOrder StatusOrder { get; set; }

        public Order()
        {
            Details = new List<Detail>();
        }
    }
}