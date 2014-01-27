using System;
using System.Collections.Generic;
using PhoneApp1.HoSTC_Service;
using System.Linq;
using PhoneApp1.Utils;
using System.Text;

namespace PhoneApp1.Data
{
    public class HOSEDataService : IDataService
    {
        const string String_DongCua = "ĐóngCửa";
        const string String_MoCua = "MởCửa";
        const string String_Dot1 = "Đợt1";
        const string String_Nghi = "Nghỉ";
        const string String_Dot2 = "Đợt2";
        const string String_KLDongCua = "KLĐóngCửa";
        const string String_SauGio = "SauGiờ";

        private static readonly HOSEDataService instance = new HOSEDataService();
        public static IDataService Instance
        {
            get { return instance; }
        }

        #region property
        private readonly HoSTC_ServiceSoapClient _client = new HoSTC_ServiceSoapClient();
        private volatile bool _isGettingData = false;

        private Action _getDataCompleteCallback;

        private readonly List<RowData> _rowsData = new List<RowData>();
        private StatisticData _statisticData;
        private Dictionary<string, string> _thongTinCty = new Dictionary<string, string>();
        private string[] _hotList;
        private readonly List<int> _VN30List = new List<int>();
        #endregion

        #region interface property
        public List<RowData> RowsData
        {
            get { return _rowsData; }
        }
        public StatisticData StatisticData
        {
            get { return _statisticData; }
        }
        public Dictionary<string, string> ThongTinCty
        {
            get { return _thongTinCty; }
        }
        public string[] HotList
        {
            get { return _hotList; }
            set { _hotList = value; }
        }
        #endregion

        private HOSEDataService()
        {
            _hotList = IsolatedStorageHelper.ReadFile(DataService.GetHotListFileName()).Split(new[] { "*" }, StringSplitOptions.RemoveEmptyEntries);

            this._client.GetListStockCompleted += _client_GetListStockCompleted;            
            this._client.GetLiveTotalMKTCompleted += client_GetLiveTotalMKTCompleted;
            this._client.GetLiveSecurityCompleted += client_GetLiveSecurityCompleted;
            this._client.fGetSTOCKROOMCompleted += _client_fGetSTOCKROOMCompleted;
        }

        #region download data complete
        void _client_fGetSTOCKROOMCompleted(object sender, fGetSTOCKROOMCompletedEventArgs e)
        {
            var companies = e.Result.Split(new[] { "**" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var company in companies)
            {
                var info = company.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                this._thongTinCty.Add(info[0], info[1]);
            }
            
            this._client.GetListStockAsync("VN30");
        }

        void _client_GetListStockCompleted(object sender, GetListStockCompletedEventArgs e)
        {
            this._VN30List.Clear();
            var temp = e.Result.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < temp.Length - 1; i++)
            {
                this._VN30List.Add(int.Parse(temp[i]));
            }
            this._client.GetLiveTotalMKTAsync();
        }

        void client_GetLiveTotalMKTCompleted(object sender, GetLiveTotalMKTCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.OnGetDataComplete(false);
                return;
            }

            this._statisticData = new StatisticData(FormatStatisticData(e.Result));
            this._client.GetLiveSecurityAsync(true);
        }

        void client_GetLiveSecurityCompleted(object sender, GetLiveSecurityCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.OnGetDataComplete(false);
                return;
            }

            this.SetRowData(e.Result);
            this._statisticData.CountTran = this._rowsData.Count(p => p.DGiaKhop == p.DTran);
            this._statisticData.CountSan = this._rowsData.Count(p => p.DGiaKhop == p.DSan);
            this._statisticData.CountThamChieu = this._rowsData.Count(p => p.DGiaKhop == p.DThamChieu);
            this._statisticData.CountTang = this._rowsData.Count(p => p.DGiaKhop > p.DThamChieu);
            this._statisticData.CountGiam = this._rowsData.Count(p => p.DGiaKhop < p.DThamChieu);
            this.OnGetDataComplete(true);
        }
        #endregion

        #region method
        static string FormatStatisticData(string s)
        {
            var temp = s.Split(new string[] { "**" }, StringSplitOptions.RemoveEmptyEntries);

            var data = temp[temp.Length - 1].Split('|');

            var datetime = data[6].Split(' ');

            var date = datetime[0].Substring(0, 5);

            var t = ConvertToTime(datetime[1]);

            var time = string.Format("{0:D2}:{1:D2}", t[0], t[1]);

            var status = GetStatusFromTime(data[8], datetime[1]);

            var sb = new StringBuilder();

            sb.Append(data[0]);
            sb.Append("|");
            sb.Append(data[1]);
            sb.Append("|");
            sb.Append(data[2]);
            sb.Append("|");
            sb.Append(data[3]);
            sb.Append("|");
            sb.Append(data[4]);
            sb.Append("|");
            sb.Append(data[5]);
            sb.Append("|");
            sb.Append(date);
            sb.Append("|");
            sb.Append(time);
            sb.Append("|");
            sb.Append(status);

            return sb.ToString();
        }

        static int[] ConvertToTime(string time)
        {
            var result = new int[3];
            result[0] = int.Parse(time.Substring(0, time.Length == 5 ? 1 : 2));
            result[1] = int.Parse(time.Substring(time.Length - 4, 2));
            result[2] = int.Parse(time.Substring(time.Length - 2, 2));
            return result;
        }

        static string GetStatusFromTime(string phien, string time)
        {
            var result = string.Empty;
            var t = int.Parse(time);

            switch (phien)
            {
                case "1":
                    result = t < 90000 ? HOSEDataService.String_DongCua : HOSEDataService.String_MoCua;
                    break;
                case "2":
                    if (t <= 113000)
                        result = HOSEDataService.String_Dot1;
                    else if (t < 130000)
                        result = HOSEDataService.String_Nghi;
                    else
                        result = HOSEDataService.String_Dot2;
                    ;
                    break;
                case "3":
                    if (t <= 140000)
                    {
                        result = HOSEDataService.String_KLDongCua;
                    }
                    else
                    {
                        result = t <= 141500 ? HOSEDataService.String_SauGio : HOSEDataService.String_DongCua;
                    }
                    break;

            }
            return result;
        }

        static string FormatRowData(string row)
        {
            var sb = new StringBuilder();
            var temp = row.Split('|');
            sb.Append(temp[0]);
            sb.Append("|");
            sb.Append(temp[1]);
            sb.Append("|");
            sb.Append(temp[2]);
            sb.Append("|");
            sb.Append(temp[3]);
            sb.Append("|");
            sb.Append(temp[4]);
            sb.Append("|");
            sb.Append(temp[5]);
            sb.Append("|");
            sb.Append(temp[6]);
            sb.Append("|");
            sb.Append(temp[7]);
            sb.Append("|");
            sb.Append(temp[8]);
            sb.Append("|");
            sb.Append(temp[9]);
            sb.Append("|");
            sb.Append(temp[13]);
            sb.Append("|");
            sb.Append(temp[14]);
            sb.Append("|");
            sb.Append(temp[15]);
            sb.Append("|");
            sb.Append(temp[16]);
            sb.Append("|");
            sb.Append(temp[17]);
            sb.Append("|");
            sb.Append(temp[18]);
            sb.Append("|");
            sb.Append(temp[10]);
            sb.Append("|");
            sb.Append(temp[22]);
            sb.Append("|");
            sb.Append(temp[11]);
            return sb.ToString();
        }

        void ClearData()
        {
            this._rowsData.Clear();
            this._statisticData = null;
        }

        void SetRowData(string data)
        {
            var rows = data.Split(new[] { "**" }, StringSplitOptions.RemoveEmptyEntries);
            var i = 0;
            foreach (var row in rows)
            {
                this._rowsData.Add(new RowData(FormatRowData(row)) { Num = i, Index = i });
                i++;
            }
        }
        #endregion

        #region interface method
        public void RefreshData(Action callback)
        {
            if (this._isGettingData == true)
            {
                this._getDataCompleteCallback = callback;
                return;
            }

            this._getDataCompleteCallback = callback;
            this.ClearData();

            this._isGettingData = true;

            if (this._thongTinCty.Count == 0)
            {
                this._client.fGetSTOCKROOMAsync();
                return;
            }

            this._client.GetLiveTotalMKTAsync();
        }

        public bool IsInHotList(string item)
        {
            return this._hotList.Contains(item);
        }

        public bool IsInTopList(int item)
        {
            return this._VN30List.Contains(item);
        }
        #endregion

        protected void OnGetDataComplete(bool isSuccess)
        {
            this._isGettingData = false;
            if (this._getDataCompleteCallback != null)
            {
                this._getDataCompleteCallback();
            }
        }
    }
}
