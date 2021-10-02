using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class AdminRepository : IRepository<Admin>
    {
        private LotoAppDbContext _DbContext;
        public AdminRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(Admin entity)
        {
            _DbContext.Admin.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<Admin> GetAll()
        {
            return _DbContext.Admin
                .Include(x => x.Draws)
                .ThenInclude(x => x.Session)
                .Include(x => x.Users)
                .ToList();
        }

        public Admin GetById(int id)
        {
            return _DbContext.Admin
                .Include(x => x.Draws)
                .ThenInclude(x => x.Session)
                .Include(x => x.Users)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Admin entity)
        {
            _DbContext.Admin.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(Admin entity)
        {
            _DbContext.Admin.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
