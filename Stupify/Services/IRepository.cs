using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stupify.Services
{
    public interface IRepository<T>
    {
        List<T> GetList();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
