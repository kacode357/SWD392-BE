using BusinessLayer.Service;
using BusinessLayer.Service.Implement;
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
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IShirtService, ShirtService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITypeShirtService, TypeShirtService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IShirtSizeService, ShirtSizeService>();
            services.AddScoped<IVnPayService, VnPayService>();

            //AddRepository
            services.AddScoped<IUserRepositoty, UserRepository>();
            services.AddScoped<IClubRepository, ClubRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IShirtRepository, ShirtRepository>();
            services.AddScoped<ITypeShirtRepository, TypeShirtRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IShirtSizeRepository, ShirtSizeRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}
