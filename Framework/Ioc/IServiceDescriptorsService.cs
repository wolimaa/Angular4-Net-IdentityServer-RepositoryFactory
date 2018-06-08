using System.Collections.Generic;

namespace Framework.Ioc
{
    public interface IServiceDescriptorsService
    {
        IEnumerable<AppService> GetServices();
    }
}