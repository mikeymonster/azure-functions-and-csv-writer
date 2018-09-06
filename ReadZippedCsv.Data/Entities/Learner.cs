
using FileHelpers;

namespace ReadZippedCsv.Data.Entities
{
    [DelimitedRecord(",")]
    public class Learner
    {
        public long UkPrn { get; set; }
        
        public int NumberOfLearners { get; set; }
    }
}
