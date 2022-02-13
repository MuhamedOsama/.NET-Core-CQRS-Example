using awsss.Resources.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Resources.Order
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public List<ProductResponse> OrderItems { get; set; }
    }
}

