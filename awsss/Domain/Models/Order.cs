using awsss.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Domain.Models
{
    public class Order: AuditableEntity
    {
        public int Id { get; set; }
        public virtual ICollection <OrderProduct> OrderProducts { get; set; }
        public double TotalAmount { get; set; }
    }
}
