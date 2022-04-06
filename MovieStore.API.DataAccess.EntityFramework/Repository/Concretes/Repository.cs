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
                return _unitOfWork.Context.Set<T>().Where(e => e.IsDeleted == false).AsQueryable();
            return _unitOfWork.Context.Set<T>().Where(e => e.IsDeleted == false).Where(filter).AsQueryable();
        }
        public T Get(Expression<Func<T, bool>> filter)
        {
            return _unitOfWork.Context.Set<T>().Where(e => e.IsDeleted == false).SingleOrDefault(filter);
        }
        public void Add(T entity)
        {
            _unitOfWork.Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _unitOfWork.Context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            var exist = _unitOfWork.Context.Set<T>().SingleOrDefault(e => e.Id == entity.Id);
            if(exist is not null)
            {
                exist.IsDeleted = true;
                _unitOfWork.Context.Entry(exist).State = EntityState.Modified;
            }
        }
    }
}