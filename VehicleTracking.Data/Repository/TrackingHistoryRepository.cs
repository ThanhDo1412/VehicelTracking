using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleTracking.Data.TrackingHistory;

namespace VehicleTracking.Data.Repository
{
    public class TrackingHistoryRepository<T> : IReponsitory<T> where T : class
    {
        private TrackingHistoryContext TrackingHistoryContext { get; set; }

        public TrackingHistoryRepository(TrackingHistoryContext trackingHistoryContext)
        {
            this.TrackingHistoryContext = trackingHistoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.TrackingHistoryContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.TrackingHistoryContext.Set<T>().Where(expression);
        }

        public T FindOneByCondition(Expression<Func<T, bool>> expression)
        {
            return this.TrackingHistoryContext.Set<T>().Where(expression).FirstOrDefault();
        }

        public T FindById(int id)
        {
            return this.TrackingHistoryContext.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            this.TrackingHistoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.TrackingHistoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.TrackingHistoryContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await this.TrackingHistoryContext.SaveChangesAsync();
        }
    }
}
