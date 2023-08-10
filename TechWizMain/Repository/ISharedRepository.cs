namespace TechWizMain.Repository
{
    public interface ISharedRepository<T, U> where T : class
    {
        public Task<T?> GetByID(U id);
        public  Task<IEnumerable<T>?> GetAll();
        public bool Insert(T entity);
        public bool Update(T entity);
        public bool Delete(T entity);

        public Task<bool> DeleteAll(List<T> list);

        public Task<bool> UpdateAll(List<T> list);

        public Task<bool> InsertAll(List<T> list);
    }
}
