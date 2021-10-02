using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class DrawRepository : IRepository<Draw>
    {
        private LotoAppDbContext _DbContext;
        public DrawRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(Draw entity)
        {
            _DbContext.Draws.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<Draw> GetAll()
        {
            return _DbContext.Draws
                .Include(x=>x.Admin)
                .ToList();
        }

        public Draw GetById(int id)
        {
            return _DbContext.Draws
                .Include(x => x.Admin)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Draw entity)
        {
            _DbContext.Draws.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(Draw entity)
        {
            _DbContext.Draws.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
