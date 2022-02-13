using awsss.Resources.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Domain.Services
{
    public interface IProductService
    {
        public Task<List<ProductResponse>> GetProducts();
        public Task<ProductResponse> AddProduct(CreateProductRequest model);

    }
}
