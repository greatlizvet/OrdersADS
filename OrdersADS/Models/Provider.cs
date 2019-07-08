using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Display(Name = "Наименование организации")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Юридический адрес")]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Provider()
        {
            Orders = new List<Order>();
        }
    }
}