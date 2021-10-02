using DataAccess.Interfaces;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class WinningNumberRepository : IRepository<WinningNumber>
    {
        private LotoAppDbContext _DbContext;
        public WinningNumberRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(WinningNumber entity)
        {
            _DbContext.WinningNumbers.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<WinningNumber> GetAll()
        {
            return _DbContext.WinningNumbers.ToList();
        }

        public WinningNumber GetById(int id)
        {
            return _DbContext.WinningNumbers.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(WinningNumber entity)
        {
            _DbContext.WinningNumbers.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(WinningNumber entity)
        {
            _DbContext.WinningNumbers.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
