using AverageLatencyApplication.Clients;
using AverageLatencyApplication.Interfaces;
using AverageLatencyApplication.Services;

namespace AverageLatencyApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILatenciesService, LatenciesService>();
            return services;
        }

        public static IServiceCollection ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddScoped<ILatenciesClient, LatenciesClient>();

            services.AddHttpClient("LatencyClient", (serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri("http://latencyapi-env.eba-kqb2ph3i.eu-west-1.elasticbeanstalk.com");
            });

            return services;
        }
    }
}
