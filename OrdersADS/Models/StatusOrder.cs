using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersADS.Models
{
    public class StatusOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ordere> Orderes { get; set; }

        public StatusOrder()
        {
            Orderes = new List<Ordere>();
        }
    }
}