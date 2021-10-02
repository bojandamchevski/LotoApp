using DataAccess.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class SessionRepository : IRepository<Session>
    {
        private LotoAppDbContext _DbContext;
        public SessionRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(Session entity)
        {
            _DbContext.Sessions.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<Session> GetAll()
        {
            return _DbContext.Sessions.ToList();
        }

        public Session GetById(int id)
        {
            return _DbContext.Sessions.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Session entity)
        {
            _DbContext.Sessions.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(Session entity)
        {
            _DbContext.Sessions.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
