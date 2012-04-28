using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using PhoneApp1.Data;
using PhoneApp1.HoSTC_Service;
using System.Linq;

namespace PhoneApp1
{
    public class DataService
    {
        private static readonly DataService instance = new DataService();
        public static DataService Instance
        {
            get { return instance; }
        }

        private DataService()
        {

            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            using (var isoFileStream = new IsolatedStorageFileStream("hotlist.txt", FileMode.OpenOrCreate, myStore))
            {
                //Write the data
                using (var isoFileReader = new StreamReader(isoFileStream))
                {
                    _hotList = isoFileReader.ReadToEnd().Split(new[] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                }
            }

            this._client.GetList30StockCompleted += new EventHandler<GetList30StockCompletedEventArgs>(_client_GetList30StockCompleted);
            this._client.GetLiveTotalMKTCompleted += new EventHandler<GetLiveTotalMKTCompletedEventArgs>(client_GetLiveTotalMKTCompleted);
            this._client.GetLiveSecurityCompleted += new EventHandler<GetLiveSecurityCompletedEventArgs>(client_GetLiveSecurityCompleted);
            this._client.GetList30StockAsync();
        }

        void _client_GetList30StockCompleted(object sender, GetList30StockCompletedEventArgs e)
        {
            this._VN30List.Clear();
            var temp = e.Result.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < temp.Length - 1; i++)
            {
                this._VN30List.Add(int.Parse(temp[i]));
            }
        }

        void client_GetLiveSecurityCompleted(object sender, GetLiveSecurityCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.OnGetDataComplete(false);
                return;
            }
            this._liveSecurity = e.Result;
            this.RowsData = RowData.GetRowData(this._liveSecurity);
            this.StatisticData.CountTran = this.RowsData.Count(p => p.DGiaKhop == p.DTran);
            this.StatisticData.CountSan = this.RowsData.Count(p => p.DGiaKhop == p.DSan);
            this.StatisticData.CountThamChieu = this.RowsData.Count(p => p.DGiaKhop == p.DThamChieu);
            this.OnGetDataComplete(true);
        }

        void client_GetLiveTotalMKTCompleted(object sender, GetLiveTotalMKTCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.OnGetDataComplete(false);
                return;
            }
            this._totalMkt = e.Result;
            this.StatisticData = new StatisticData(this._totalMkt);
            this._client.GetLiveSecurityAsync(true);
        }

        private readonly HoSTC_ServiceSoapClient _client = new HoSTC_ServiceSoapClient();

        private string _totalMkt;
        private string _liveSecurity;
        private List<RowData> _rowsData;
        private StatisticData _statisticData;
        private List<int> _VN30List = new List<int>();
        private string[] _hotList;

        public List<int> VN30List { get { return _VN30List; } }

        public StatisticData StatisticData
        {
            get { return _statisticData; }
            set { _statisticData = value; }
        }

        public List<RowData> RowsData
        {
            get { return _rowsData; }
            set { _rowsData = value; }
        }

        public string[] HotList
        {
            get { return _hotList; }
            set { _hotList = value; }
        }

        private Action<GetDataCompletedEventArgs> _getDataCompleteCallback;

        protected void OnGetDataComplete(bool isSuccess)
        {
            if (this._getDataCompleteCallback != null)
            {
                this._getDataCompleteCallback(new GetDataCompletedEventArgs()
                                                 {
                                                     LiveSecurity = _liveSecurity,
                                                     TotalMKT = _totalMkt,
                                                     IsSuccess = isSuccess,
                                                     RowsData = RowsData,
                                                     StatisticData = StatisticData
                                                 });
            }
        }

        public void RefreshData(Action<GetDataCompletedEventArgs> callback)
        {
            this._getDataCompleteCallback = callback;
            this._totalMkt = string.Empty;
            this._liveSecurity = string.Empty;

            this._client.GetLiveTotalMKTAsync();
        }

        public class GetDataCompletedEventArgs : EventArgs
        {
            public string LiveSecurity { get; set; }
            public string TotalMKT { get; set; }
            public bool IsSuccess { get; set; }
            public List<RowData> RowsData { get; set; }
            public StatisticData StatisticData { get; set; }
        }
    }
}
