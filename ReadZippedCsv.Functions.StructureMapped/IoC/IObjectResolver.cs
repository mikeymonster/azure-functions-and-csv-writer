using System;

namespace ReadZippedCsv.Functions.StructureMapped.IoC
{
    public interface IObjectResolver
    {
        object Resolve(Type type);
        T Resolve<T>();
    }
}
