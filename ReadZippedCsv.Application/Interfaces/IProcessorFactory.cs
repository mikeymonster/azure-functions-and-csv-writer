
namespace ReadZippedCsv.Application.Interfaces
{
    public interface IProcessorFactory
    {
        IProcessor GetProcessor(string hint);
    }
}
