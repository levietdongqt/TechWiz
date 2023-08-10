﻿using System.Runtime.CompilerServices;
using TechWizMain.Models;

namespace TechWizMain.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TechWizContext _context;
        private readonly GenericRepository<Category> _genericRepository;

        public CategoryRepository(TechWizContext context, GenericRepository<Category> genericRepository)
        {
            _context = context;
            this._genericRepository = genericRepository;
        }

        public bool Delete(Category entity)
        {
           return _genericRepository.Delete(entity);
        }

        public Task<bool> DeleteAll(List<Category> list)
        {
            return _genericRepository.DeleteAll(list);
        }

        public Task<IEnumerable<Category>?> GetAll()
        {
            return _genericRepository.GetAll();
        }

        public async Task<Category?> GetByID(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);             
                return category;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            
            
        }

        public bool Insert(Category entity)
        {
            return _genericRepository.Insert(entity);
        }

        public async Task<bool> InsertAll(List<Category> list)
        {
            return await (_genericRepository.InsertAll(list));
        }

        public bool Update(Category entity)
        {
            return _genericRepository.Update(entity);
        }

        public async Task<bool> UpdateAll(List<Category> list)
        {
            return await _genericRepository.UpdateAll(list);
        }
    }
}
