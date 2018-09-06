
using FileHelpers;

namespace ReadZippedCsv.Application.Entities
{
    [DelimitedRecord(",")]
    public class Aim
    {
        public long UkPrn { get; set; }

        public int NumberOfLearnersWithACT1 { get; set; }

        public int NumberOfLearnersWithACT2 { get; set; }
    }
}
