using DebtManager.Domain.Entities;
using System.Linq;
using Vadim.Common.Filters;

namespace DebtManager.Domain.Users.Filters
{
    public class UsersFilter : PagingFilter<User, int>
    {
        public UsersFilter(IQueryable<User> entities, PagingParams parameters)
            : base(entities, parameters) { }
    }
}
