using System.Collections.Generic;

namespace CostApplication.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        T Get(int id);

        T Add(T cost);

        T Update(T cost);

        void Delete(int id);
    }
}
