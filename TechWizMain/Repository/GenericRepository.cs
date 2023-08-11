using Microsoft.EntityFrameworkCore;
using MimeKit;
using TechWizMain.Models;

namespace TechWizMain.Repository
{
    public class GenericRepository<T> where T : class
    {
        private readonly TechWizContext _context;
        private DbSet<T> _entities;
        public GenericRepository(TechWizContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            try
            {
                var list = await _entities.ToListAsync();
                return (IEnumerable<T>?)list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public bool Insert(T entity)
        {
            try
            {
                _entities.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public async Task<bool> InsertAll(List<T> list)
        {
            using (var trasaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _entities.AddRangeAsync(list);
                    _context.SaveChanges();
                    trasaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    trasaction.Rollback();
                    return false;
                }
            }
        }
        public bool Update(T entity)
        {
            try
            {
                _entities.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public async Task<bool> UpdateAll(List<T> list)
        {
            using (var trasaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in list)
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                    trasaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    trasaction.Rollback();
                    return false;
                }
            }
        }
        public bool Delete(T entity)
        {
            try
            {
                _entities.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                    return false;
            }
        }
        public async Task<bool> DeleteAll(List<T> list)
        {
            using (var trasaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in list)
                    {
                        _context.Entry(entity).State = EntityState.Deleted;
                    }
                    await _context.SaveChangesAsync();
                    trasaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    trasaction.Rollback();
                    return false;
                }
            }
        }

    }

}
