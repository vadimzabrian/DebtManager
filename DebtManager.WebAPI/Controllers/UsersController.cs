using DebtManager.Application.Users;
using DebtManager.Domain.Entities;
using DebtManager.WebAPI.App_Start;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DebtManager.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        // GET api/values
        public IEnumerable<User> Get()
        {
            return DependencyResolver.Resolve<IUsersProvider>().Execute();
            //return new UsersProvider().Execute().ToArray();
        }
    }
}
