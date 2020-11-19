using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> ReadAll(bool isTracked = true);
        T Read(int id, bool isTracked = true);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, bool isTracked = true);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
