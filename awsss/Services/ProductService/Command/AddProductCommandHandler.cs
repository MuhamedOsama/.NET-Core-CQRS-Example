using AutoMapper;
using awsss.Domain.Models;
using awsss.Persistence;
using awsss.Resources.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace awsss.Services.ProductService.Command
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(product);

        }
    }
}
