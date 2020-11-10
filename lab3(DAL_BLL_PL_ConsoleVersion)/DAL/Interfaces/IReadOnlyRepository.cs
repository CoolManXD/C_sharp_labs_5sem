using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IReadOnlyRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
