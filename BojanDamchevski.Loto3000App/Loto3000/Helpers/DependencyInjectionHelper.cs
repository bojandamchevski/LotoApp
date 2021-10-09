using DataAccess;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LotoAppDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRepository<Draw>, DrawRepository>();
            services.AddTransient<IRepository<Session>, SessionRepository>();
            services.AddTransient<IRepository<LotoNumber>, LotoNumberRepository>();
            services.AddTransient<IRepository<WinningNumber>, WinningNumberRepository>();
            services.AddTransient<IRepository<Prize>, PrizeRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
