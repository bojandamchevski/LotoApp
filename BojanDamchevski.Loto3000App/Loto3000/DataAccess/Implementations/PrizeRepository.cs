using DataAccess.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class PrizeRepository : IRepository<Prize>
    {
        private LotoAppDbContext _DbContext;
        public PrizeRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(Prize entity)
        {
            _DbContext.Prizes.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<Prize> GetAll()
        {
            return _DbContext.Prizes.ToList();
        }

        public Prize GetById(int id)
        {
            return _DbContext.Prizes.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Prize entity)
        {
            _DbContext.Prizes.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(Prize entity)
        {
            _DbContext.Prizes.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
