using BusinessLayer.Service;
using BusinessLayer.Service.Interface;
using DataLayer.DBContext;
using DataLayer.Repository;
using DataLayer.Repository.Implement;
using Microsoft.EntityFrameworkCore;

namespace SWD392_FA24_SportShop.AppStarts
{
    public static class DependencyInjectionContainers
    {
        public static void ServiceContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true; ;
                options.LowercaseQueryStrings = true;
            });
            //Add_DbContext
            services.AddDbContext<db_aad141_swd392Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("hosting"));
            });

            //AddService
            services.AddScoped<IUserService, UserService>();

            //AddRepository
            services.AddScoped<IUserRepositoty, UserRepository>();

        }
    }
}
