using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleTracking.Data.Vehicle;

namespace VehicleTracking.Data.Repository
{
    public class VehicleRepository<T> : IReponsitory<T> where T : class
    {
        private VehicleContext VehicleContext { get; set; }

        public VehicleRepository(VehicleContext vehicleContext)
        {
            this.VehicleContext = vehicleContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.VehicleContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.VehicleContext.Set<T>().Where(expression);
        }

        public T FindOneByCondition(Expression<Func<T, bool>> expression)
        {
            return this.VehicleContext.Set<T>().Where(expression).FirstOrDefault();
        }

        public T FindById(int id)
        {
            return this.VehicleContext.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            this.VehicleContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.VehicleContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.VehicleContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await this.VehicleContext.SaveChangesAsync();
        }
    }
}
