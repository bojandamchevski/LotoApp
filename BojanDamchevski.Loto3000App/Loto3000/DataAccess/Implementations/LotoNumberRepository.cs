using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class LotoNumberRepository : IRepository<LotoNumber>
    {
        private LotoAppDbContext _DbContext;
        public LotoNumberRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(LotoNumber entity)
        {
            _DbContext.LotoNumbers.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<LotoNumber> GetAll()
        {
            return _DbContext.LotoNumbers
                .Include(x => x.User)
                .ToList();
        }

        public LotoNumber GetById(int id)
        {
            return _DbContext.LotoNumbers
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(LotoNumber entity)
        {
            _DbContext.LotoNumbers.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(LotoNumber entity)
        {
            _DbContext.LotoNumbers.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
