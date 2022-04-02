using Microsoft.Extensions.DependencyInjection;
using SmallNetCore.Extensions.AutoMapper;

namespace SmallNetCore.Extensions.ServiceExtensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(CustomProfile));
            //AutoMapperConfig.RegisterMappings();
        }
    }
}
