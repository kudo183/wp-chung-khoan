namespace PhoneApp1.Data
{
    public class DataService
    {
        public enum DataSource
        {
            HSX,
            HNX
        }

        public static DataSource Mode = DataSource.HSX;

        public static string GetHotListFileName()
        {
            switch (Mode)
            {
                case DataSource.HSX:
                    return "HotList.txt";
                case DataSource.HNX:
                    return "HNXHotList.txt";
            }
            return string.Empty;
        }

        //singleton instance
        public static IDataService Instance
        {
            get
            {
                switch (Mode)
                {
                    case DataSource.HSX:
                        return HOSEDataService.Instance;
                    case DataSource.HNX:
                        return HNXDataService.Instance;
                }
                return HOSEDataService.Instance;
            }
        }
    }
}
