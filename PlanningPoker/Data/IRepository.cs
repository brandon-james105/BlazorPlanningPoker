using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Data
{
    public interface IRepository<T> : IReadableRepository<T>, IWriteableRepository<T>
    {
    }

    public interface IReadableRepository<T>
    {
        T Find(string id);

        IEnumerable<T> List();
    }

    public interface IWriteableRepository<T>
    {
        T Create(T item);

        T Update(T item);

        bool Delete(string id);
    }
}
