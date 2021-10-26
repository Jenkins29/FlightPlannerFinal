using System.Linq;
using FlightPlanner.Core2.Models;

namespace FlightPlanner.Core2.Services
{
    public interface IDbService
    {
        IQueryable<T> Query<T>() where T : Entity;

        T GetById<T>(int id) where T : Entity;

        void Create<T>(T entity) where T : Entity;

        void Update<T>(T entity) where T : Entity;

        void Delete<T>(T entity) where T : Entity;
    }
}
