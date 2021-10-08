using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(Guid id);
        IReadOnlyList<T> GetAll();
        T Add(T entity);
        void Delete(T entity);
    }
}
