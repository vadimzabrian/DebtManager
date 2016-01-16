using System.Linq;

namespace Vadim.Common.Filters
{
    public class PagingFilter<T, TKey>
        where T : IEntity<TKey>
    {
        IQueryable<T> _entities;
        PagingParams _parameters;

        public PagingFilter(IQueryable<T> entities, PagingParams parameters)
        {
            _entities = entities;
            _parameters = parameters;
        }

        public virtual IQueryable<T> Execute()
        {
            if (_parameters != null)
            {
                if (_parameters.PageNumber.HasValue && _parameters.PageSize.HasValue)
                {
                    _entities = _entities.OrderBy(q => q.Id).Skip((_parameters.PageNumber.Value - 1) * _parameters.PageSize.Value).Take(_parameters.PageSize.Value);
                }
            }

            return _entities;
        }
    }
}
