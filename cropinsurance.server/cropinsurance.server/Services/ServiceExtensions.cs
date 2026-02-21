using System.Runtime.Serialization;

namespace cropinsurance.server.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services) 
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
