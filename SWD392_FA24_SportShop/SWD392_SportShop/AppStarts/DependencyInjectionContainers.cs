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


            //AddService


            //AddRepository


        }
    }
}
