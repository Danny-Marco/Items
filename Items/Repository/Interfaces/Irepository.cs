using System.Collections.Generic;

namespace Items.Repository
{
    public interface Irepository<T> where T : class
    {
        public IEnumerable<T> GetParents();

        public IEnumerable<T> GetChildrenByID(int id);

        public IEnumerable<T> GetAll();
        
        public T GetById(int id);

        public void Add(T item);
        
        void Update(T item);

        void Delete(T item);

        void Save();
    }
}