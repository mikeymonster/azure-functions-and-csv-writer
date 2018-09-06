using System;
using FileHelpers;

namespace ReadZippedCsv.Application.Entities
{
    [DelimitedRecord(",")]
    public class DataLock
    {
        public string Collection { get; set; }

        public long UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public long ULN { get; set; }

        public long AimSeqNumber { get; set; }

        public string RuleId { get; set; }

        public string CollectionPeriodName { get; set; }

        public int CollectionPeriodMonth { get; set; }

        public int CollectionPeriodYear { get; set; }

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy HH:mm:ss")]
        public DateTime LastSubmission { get; set; }

        public long TNP { get; set; }
    }
}
