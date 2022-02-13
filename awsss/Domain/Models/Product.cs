using awsss.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Domain.Models
{
    public class Product: AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsFree { get; set; }
        public virtual ICollection<OrderProduct> ProductOrders { get; set; }

    }
}
