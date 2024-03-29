﻿using AutoMapper;
using awsss.Domain.Models;
using awsss.Resources.Product;
using awsss.Services.ProductService.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awsss.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<AddProductCommand, Product>();

        }
    }
}
