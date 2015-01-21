using System;
using System.Collections.Generic;

namespace PhoneApp1.Data
{
    public interface IDataService
    {
        List<RowData> RowsData { get; }
        StatisticData StatisticData { get; }
        Dictionary<string, string> ThongTinCty { get; }
        string[] HotList { get; set; }

        void RefreshData(Action callback);
        bool IsInHotList(string item);
        bool IsInTopList(int item);
    }
}
