using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VT.Data.Vehicle;

namespace VT.Data.Repository
{
    public class VehicleRepository<T> : IReponsitory<T> where T : class
    {
        private VehicleContext VehicleContext { get; set; }

        public VehicleRepository(VehicleContext vehicleContext)
        {
            this.VehicleContext = vehicleContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.VehicleContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.VehicleContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.VehicleContext.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await this.VehicleContext.Set<T>().FindAsync(id);
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
