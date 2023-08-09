namespace TechWizMain.Repository
{
    public interface IGenericRepository<T,U> where T : class
    {
        IEnumerable<T> GetAll();
        T GetT(U id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Remove(T entity);
        bool SaveChanges();
    }
}
