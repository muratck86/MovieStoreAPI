using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieStore.API.DataAccess.EntityFramework.Repository.Abstracts;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.DataAccess.EntityFramework.Repository.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            if(filter is null)
                return _unitOfWork.Context.Set<T>().AsQueryable();
            return _unitOfWork.Context.Set<T>().Where(filter).AsQueryable();
        }
        public T Get(Expression<Func<T, bool>> filter)
        {
            return _unitOfWork.Context.Set<T>().SingleOrDefault(filter);
        }
        public void Add(T entity)
        {
            _unitOfWork.Context.Entry<T>(entity).State = EntityState.Added;
        }
        public void Update(T entity)
        {   
            _unitOfWork.Context.Entry<T>(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            _unitOfWork.Context.Entry<T>(entity).State = EntityState.Deleted;
        }
    }
}