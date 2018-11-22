using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace ReadZippedCsv.Functions.StructureMapped.IoC
{
    public class InjectValueProvider : IValueProvider
    {
        private readonly object _value;
        private readonly ParameterInfo _parameterInfo;
        private readonly IObjectResolver _objectResolver;

        public InjectValueProvider(ParameterInfo parameterInfo, IObjectResolver objectResolver, object value)
        {
            _value = value;
            _parameterInfo = parameterInfo;
            _objectResolver = objectResolver;
        }

        //public Type Type => _value.GetType();
        public Type Type => _parameterInfo.ParameterType;

        public Task<object> GetValueAsync() => 
            //Task.FromResult(_value);
            Task.FromResult(_objectResolver.Resolve(Type));

        public string ToInvokeString() => 
            //_value.ToString();
            Type.ToString();

    }
}
