using DebtManager.Application.Infrastructure;
using DebtManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using Vadim.Common;

namespace DebtManager.Infrastructure.EFCodeFirst
{
    public class DebtManagerDbContext : DbContext, IDbRepository
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebtManagerDbContext"/> class.
        /// </summary>
        public DebtManagerDbContext()
            : base("DebtManagerDbContext")
        {
            System.Data.Entity.Database.SetInitializer<DebtManagerDbContext>(null);//new DropCreateDatabaseAlways<DebtManagerDbContext>();
        }

        //static DebtManagerDbContext()
        //{
        //    Database.SetInitializer<DebtManagerDbContext>(new CreateDatabaseIfNotExists<DebtManagerDbContext>());
        //}

        public static DebtManagerDbContext Create()
        {
            //var dbContext = context.Get<DebtManagerDbContext>();//IOwinContext context
            return new DebtManagerDbContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebtManagerDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        //public DebtManagerDbContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{
        //    System.Data.Entity.Database.SetInitializer<DebtManagerDbContext>(null); 
        //}

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            var typesToRegister =
                Assembly.GetExecutingAssembly().GetTypes().Where(
                    type =>
                    type.BaseType != null &&
                    type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }

            FixRelationships(modelBuilder);

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>()
            //    .ToTable("Users", "dbo")
            //    .HasKey<int>(l => l.Id);

            //modelBuilder.Entity<UserLogin>().ToTable("UserLogin", "dbo");//.HasKey<int>(l => l.UserId);
            //modelBuilder.Entity<Role>().ToTable("Roles", "dbo");//.HasKey<int>(r => r.Id);
            //modelBuilder.Entity<UserRole>().ToTable("UserRoles");//.HasKey(r => new { r.RoleId, r.UserId });
            //modelBuilder.Entity<UserClaim>().ToTable("UserClaim");

            //modelBuilder.Entity<Case>()
            //.HasMany(x => x.Participants)
            //.WithMany(x => x.CasesParticipants)
            //.Map(x =>
            //{
            //    x.ToTable("CaseParticipants");
            //    x.MapLeftKey("Case_Id");
            //    x.MapRightKey("User_Id");
            //});
        }

        /// <summary>
        /// This method is called to fix some issues with related entities.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void FixRelationships(System.Data.Entity.DbModelBuilder modelBuilder)
        {
        }

        //public T Insert<T>(T entity)
        //    where T : class
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException("entity");

        //    return this.Set<T>().Add(entity);
        //new DropCreateDatabaseAlways<DebtManagerDbContext>()}

        //    public IQueryable<T> GetAll<T>()
        //where T : class
        //    {
        //        return this.Set<T>();
        //    }

        /// <summary>
        /// Persists all.
        /// </summary>
        //public void PersistAll()
        //{
        //    try
        //    {
        //        this.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        // Convert DbEntityValidationException to ValidationException
        //        throw new ValidationException(ex.Message, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException is OptimisticConcurrencyException)
        //        {
        //            //throw new ValidationException(ex.Message, ex);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}



        public IQueryable<T> GetAll<T, TKey>()
            where T : IEntity<TKey>
        {
            //if (typeof(T) == typeof(Debt)) { return this.Debts.OfType<T>(); }
            if (typeof(T) == typeof(Payment)) { return this.Payments.OfType<T>(); }
            if (typeof(T) == typeof(User)) { return this.Users.OfType<T>(); }

            throw new Exception("GetAll operation not implemented for this entity.");
        }

        public IQueryable<T> GetAll<T>(string[] includes)
            where T : class
        {
            //if (typeof(T) == typeof(Debt)) { return GetIncludable(this.Debts, includes).OfType<T>(); }
            if (typeof(T) == typeof(Payment)) { return GetIncludable(this.Payments, includes).OfType<T>(); }
            if (typeof(T) == typeof(User)) { return GetIncludable(this.Users, includes).OfType<T>(); }

            throw new Exception("Remove operation not implemented for this entity.");
        }

        private IQueryable<T> GetIncludable<T>(DbSet<T> dbSet, string[] includes)
               where T : class
        {
            if (includes == null) return dbSet.OfType<T>();

            DbQuery dbQuery = dbSet.Include(includes[0]);

            foreach (var include in includes)
            {
                dbQuery = dbQuery.Include(include);
            }

            return dbQuery.OfType<T>();
        }

        public T Add<T>(T entity)
            where T : class
        {
            //if (typeof(T) == typeof(Debt)) { this.Debts.Add(entity as Debt); return entity; }
            if (typeof(T) == typeof(Payment)) { this.Payments.Add(entity as Payment); return entity; }
            if (typeof(T) == typeof(User)) { this.Users.Add(entity as User); return entity; }

            throw new Exception("Add operation not implemented for this entity.");
        }

        public void Remove<T>(T entity)
            where T : class
        {
            //if (typeof(T) == typeof(Debt)) { this.Debts.Remove(entity as Debt); return; }
            if (typeof(T) == typeof(Payment)) { this.Payments.Remove(entity as Payment); return; }
            if (typeof(T) == typeof(User)) { this.Users.Remove(entity as User); return; }

            throw new Exception("Remove operation not implemented for this entity.");
        }



        public void AddRange<T>(IEnumerable<T> array) where T : class
        {
            //if (typeof(T) == typeof(Debt)) { this.Debts.AddRange(array.Cast<Debt>()); return; }
            if (typeof(T) == typeof(Payment)) { this.Payments.AddRange(array.Cast<Payment>()); return; }
            if (typeof(T) == typeof(User)) { this.Users.AddRange(array.Cast<User>()); return; }


            throw new Exception("AddRange operation not implemented for this entity.");
        }

        public void PersistChanges()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw new ValidationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is OptimisticConcurrencyException)
                {
                    //throw new ValidationException(ex.Message, ex);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
