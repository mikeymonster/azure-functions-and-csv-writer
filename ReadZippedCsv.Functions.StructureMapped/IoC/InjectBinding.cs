using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace ReadZippedCsv.Functions.StructureMapped.IoC
{
    public class InjectBinding : IBinding
    {
        private readonly ParameterInfo _parameterInfo;
        private readonly IObjectResolver _objectResolver;

        public InjectBinding(ParameterInfo parameterInfo, IObjectResolver objectResolver)
        {
            _parameterInfo = parameterInfo;
            _objectResolver = objectResolver;
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            //Task.FromResult((IValueProvider)new InjectValueProvider(value));

            return Task.FromResult<IValueProvider>(new InjectValueProvider(_parameterInfo, _objectResolver, value));
        }

        public async Task<IValueProvider> BindAsync(BindingContext context)
        {
            await Task.Yield();
            //var value = _serviceProvider.GetRequiredService(_type);
            var value = _objectResolver.Resolve(_parameterInfo.ParameterType);
            return await BindAsync(value, context.ValueContext);
            
            //TODO: Remove value above and just call with null, or do it like below
            //In das-data this just does this, probably because the value provider will do the object resolving:
            //return Task.FromResult<IValueProvider>(new InjectValueProvider(_parameterInfo, _objectResolver));
        }

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor
        {
            Name = _parameterInfo.Name,
            DisplayHints = new ParameterDisplayHints
            {
                Description = "Inject services",
                DefaultValue = "Inject services",
                Prompt = "Inject services"
            }
        };
    }
}
