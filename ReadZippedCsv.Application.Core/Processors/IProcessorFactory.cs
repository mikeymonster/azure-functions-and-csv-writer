
namespace ReadZippedCsv.Application.Processors
{
    public interface IProcessorFactory
    {
        IProcessor GetProcessor(string hint);
    }
}
