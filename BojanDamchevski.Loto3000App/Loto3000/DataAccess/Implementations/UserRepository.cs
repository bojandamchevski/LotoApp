using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private LotoAppDbContext _DbContext;
        public UserRepository(LotoAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Delete(User entity)
        {
            _DbContext.Users.Remove(entity);
            _DbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _DbContext.Users
                .Include(x => x.LotoNumbers)
                .ToList();
        }

        public User GetById(int id)
        {
            return _DbContext.Users
                .Include(x => x.LotoNumbers)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Insert(User entity)
        {
            _DbContext.Users.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _DbContext.Users.Update(entity);
            _DbContext.SaveChanges();
        }
    }
}
