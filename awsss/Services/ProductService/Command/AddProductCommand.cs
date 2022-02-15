using awsss.Resources.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Services.ProductService.Command
{
    public record AddProductCommand(string Name, double Price, bool IsFree ) : IRequest<ProductResponse>;
}
