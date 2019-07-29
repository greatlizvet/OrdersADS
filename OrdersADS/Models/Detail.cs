using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrdersADS.Models
{
    public class Detail
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Артикул")]
        public string Articul { get; set; }
        
        public virtual ICollection<Ordere> Orderes { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

        public Detail()
        {
            Orderes = new List<Ordere>();
            Requests = new List<Request>();
        }
    }
}