using DebtManager.Application.Infrastructure;
using DebtManager.Domain.Entities;
using System.Linq;

namespace DebtManager.Application.Users
{
    public class UsersProvider : IUsersProvider
    {
        IDbRepository _dbRepository;

        public UsersProvider(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository != null ? dbRepository : null;
        }

        public IQueryable<User> Execute()
        {
            return _dbRepository.GetAll<User>();
        }
    }

    public interface IUsersProvider
    {
        IQueryable<User> Execute();
    }
}
