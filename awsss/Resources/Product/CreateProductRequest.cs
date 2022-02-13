using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Resources.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsFree { get; set; }
    }
}
