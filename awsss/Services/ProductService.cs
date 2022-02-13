using AutoMapper;
using awsss.Domain.Models;
using awsss.Domain.Services;
using awsss.Persistence;
using awsss.Resources.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductResponse>> GetProducts()
        {
            return _mapper.Map<List<ProductResponse>>(await _context.Products.ToListAsync());
        }
        public async Task<ProductResponse> AddProduct(CreateProductRequest model)
        {
            var product = _mapper.Map<Product>(model);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
