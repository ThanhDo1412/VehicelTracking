using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VT.Data.TrackingHistory;

namespace VT.Data.Repository
{
    public class TrackingHistoryRepository<T> : IReponsitory<T> where T : class
    {
        private TrackingHistoryContext TrackingHistoryContext { get; set; }

        public TrackingHistoryRepository(TrackingHistoryContext trackingHistoryContext)
        {
            this.TrackingHistoryContext = trackingHistoryContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.TrackingHistoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.TrackingHistoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.TrackingHistoryContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await this.TrackingHistoryContext.Set<T>().FindAsync(id);
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
