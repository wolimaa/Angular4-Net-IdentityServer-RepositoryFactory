using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Framework.Ioc
{

    public class ServiceDescriptorsService : IServiceDescriptorsService
    {
        private readonly IServiceCollection _serviceCollection;

        public ServiceDescriptorsService(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public IEnumerable<AppService> GetServices()
        {
            foreach (var srv in _serviceCollection)
            {
                yield return (
                    new AppService()
                    {
                        FullName = srv.ServiceType.FullName,
                        Lifetime = srv.Lifetime.ToString(),
                        ImplementationType = srv.ImplementationType?.FullName
                    });
            }
        }
    }
}