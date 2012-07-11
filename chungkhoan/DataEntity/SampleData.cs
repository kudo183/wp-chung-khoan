using System.Collections.Generic;

namespace PhoneApp1.Data
{
    public class RowsData : List<RowData>
    {

    }

    public class SampleData
    {
        public RowsData SecurityTableData { get; set; }
        public StatisticData StatisticData { get; set; }
    }
}
