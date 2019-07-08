using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersADS.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public Status()
        {
            Requests = new List<Request>();
        }
    }
}