using ContactsApp.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.API
{
    public class ConfigureService
    {
        public static void Configure(IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IContactsService, ContactsService>();

        }
    }
}
