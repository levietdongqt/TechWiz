﻿using System.Collections;
using TechWizMain.Models;

namespace TechWizMain.Services.ProductsService
{
    public interface IProductService
    {
        bool AddProduct(Product product, IFormFile? formFile);
        IEnumerable getTypeProduct();
        Task<IEnumerable<Product>> GetProductListByStatus(bool status);
    }
}
