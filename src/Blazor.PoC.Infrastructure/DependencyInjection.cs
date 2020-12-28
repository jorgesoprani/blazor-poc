using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Blazor.PoC.Infrastructure.Data;
using Blazor.PoC.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Blazor.PoC.Infrastructure.Common;
using Blazor.PoC.Common;

namespace Blazor.PoC.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlazorDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Blazor")));

            services.AddScoped<IBlazorDbContext>(provider => provider.GetService<BlazorDbContext>());
            services.AddTransient<IDateTime, MachineDateTime>();
        }
    }
}
