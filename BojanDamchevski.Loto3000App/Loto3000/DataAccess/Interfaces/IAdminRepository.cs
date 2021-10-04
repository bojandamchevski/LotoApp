using Domain.Models;

namespace DataAccess.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GetAdminByUsername(string username);
        Admin LoginAdmin(string username, string password);
    }
}
