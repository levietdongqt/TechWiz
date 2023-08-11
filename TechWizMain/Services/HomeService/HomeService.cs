﻿using TechWizMain.Models;
using TechWizMain.Repository.CategoryRepository;

namespace TechWizMain.Services.HomeService
{
    public class HomeService : IHomeService
    {
        private readonly ICategoryRepository _categoryRepository;
        public HomeService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> GetAllCate()
        {
            return await _categoryRepository.GetAll();
        }
    }
}
