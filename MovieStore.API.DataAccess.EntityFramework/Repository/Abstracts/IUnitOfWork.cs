using System;

namespace MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        MovieStoreDbContext Context {get; }
        void Commit();
    }
}