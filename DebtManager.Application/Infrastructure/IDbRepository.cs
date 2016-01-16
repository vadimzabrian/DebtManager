using System.Collections.Generic;
using System.Linq;
using Vadim.Common;

namespace DebtManager.Application.Infrastructure
{
    public interface IDbRepository
    {
        IQueryable<T> GetAll<T, TKey>() where T : IEntity<TKey>;
        IQueryable<T> GetAll<T>(string[] includes = null) where T : class;

        T Add<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> array) where T : class;

        void Remove<T>(T entity) where T : class;

        void PersistChanges();
    }
}
