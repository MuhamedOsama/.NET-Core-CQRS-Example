using AutoMapper;
using awsss.Persistence;
using awsss.Resources.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace awsss.Services.ProductService.Query
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ProductResponse>>(await _context.Products.ToListAsync(cancellationToken: cancellationToken));
        }
    }
}
