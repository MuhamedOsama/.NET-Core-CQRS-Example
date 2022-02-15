using awsss.Resources.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Services.ProductService.Query
{
    public record GetAllProductsQuery:IRequest<List<ProductResponse>>;
}
