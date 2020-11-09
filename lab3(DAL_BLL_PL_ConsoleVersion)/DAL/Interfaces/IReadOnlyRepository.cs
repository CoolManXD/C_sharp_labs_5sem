using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IReadOnlyRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
