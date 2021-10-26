using System.Linq;
using FlightPlanner.Core2.Models;
using FlightPlanner.Core2.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public IQueryable<T> Query()
        {
            return Query<T>();
        }

        public T GetById(int id)
        {
            return GetById<T>(id);
        }

        public void Create(T entity)
        {
            Create<T>(entity);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }
    }
}
