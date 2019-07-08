using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Zakazchik
    {
        public int Id { get; set; }

        [Display(Name = "Название организации")]
        public string Name { get; set; }

        [Display(Name = "Адрес электронной почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Юридический адрес")]
        public string Address { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public Zakazchik()
        {
            Requests = new List<Request>();
        }
    }
}