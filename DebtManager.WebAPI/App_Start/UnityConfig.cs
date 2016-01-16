using DebtManager.Application;
using DebtManager.Application.Infrastructure;
using DebtManager.Application.Payments;
using DebtManager.Application.Users;
using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Debts;
using DebtManager.Domain.Minimizers;
using DebtManager.Infrastructure.EFCodeFirst;
using DebtManager.WebAPI.Controllers;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace DebtManager.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IDbRepository, DebtManagerDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, DebtManagerDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDebtCalculatorForTwoPeople, DebtCalculatorForTwoPeople>(new HierarchicalLifetimeManager());
            container.RegisterType<IDebtCalculatorForMultiplePeople, DebtCalculatorForMultiplePeople>(new HierarchicalLifetimeManager());
            container.RegisterType<IDebtCalculatorForOnePerson, DebtCalculatorForOnePerson>(new HierarchicalLifetimeManager());
            container.RegisterType<IDebtsToDebtDtosConverter, DebtsToDebtDtosConverter>(new HierarchicalLifetimeManager());
            container.RegisterType<IPairMinimizer, PairMinimizer>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentCreator, PaymentCreator>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentsProvider, PaymentsProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsersProvider, UsersProvider>(new HierarchicalLifetimeManager());

            //var tmp = container.Resolve<UsersProvider>().Execute();

            //container.RegisterType<UsersProvider>(new HierarchicalLifetimeManager(), new InjectionConstructor(typeof(IDbRepository)));
            // e.g. container.RegisterType<ITestService, TestService>();



            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}