using System;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;

namespace MovieStore.API.DataAccess.EntityFramework.Repository.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        public MovieStoreDbContext Context {get; }

        public UnitOfWork(MovieStoreDbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}