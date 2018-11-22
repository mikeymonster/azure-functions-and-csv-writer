using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace ReadZippedCsv.Functions.StructureMapped.IoC
{
    public class InjectBindingProvider : IBindingProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public InjectBindingProvider(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            //IBinding binding = new InjectBinding(_serviceProvider, context.Parameter.ParameterType);
            //return Task.FromResult(binding);

            var parameterInfo = context.Parameter;
            var injectAttribute = parameterInfo.GetCustomAttribute<InjectAttribute>();
            if (injectAttribute == null)
            {
                return Task.FromResult<IBinding>(null);
            }

            var objectResolver = new StructureMapObjectResolver();
            return Task.FromResult<IBinding>(new InjectBinding(parameterInfo, objectResolver));
        }
    }
}
