
using FileHelpers;

namespace ReadZippedCsv.Application.Entities
{
    [DelimitedRecord(",")]
    public class Learner
    {
        public long UkPrn { get; set; }
        
        public int NumberOfLearners { get; set; }
    }
}
