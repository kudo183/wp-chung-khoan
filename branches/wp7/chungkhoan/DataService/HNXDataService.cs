using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using PhoneApp1.Utils;

namespace PhoneApp1.Data
{
    public class HNXDataService : IDataService
    {
        private static readonly HNXDataService instance = new HNXDataService();
        public static IDataService Instance
        {
            get { return instance; }
        }

        #region property
        private volatile bool _isGettingData = false;
        private Action _getDataCompleteCallback;
       
        private readonly List<RowData> _rowsData = new List<RowData>();
        private StatisticData _statisticData;
        private readonly Dictionary<string, string> _thongTinCty = new Dictionary<string, string>();
        private string[] _hotList;
        private readonly List<int> _HNX30List = new List<int>();
        #endregion

        #region interface property
        public List<RowData> RowsData { get { return this._rowsData; } }
        public StatisticData StatisticData { get { return this._statisticData; } }
        public Dictionary<string, string> ThongTinCty { get { return this._thongTinCty; } }
        public string[] HotList
        {
            get { return _hotList; }
            set { _hotList = value; }
        }
        #endregion

        public HNXDataService()
        {
            _hotList = IsolatedStorageHelper.ReadFile(DataService.GetHotListFileName()).Split(new[] { "*" }, StringSplitOptions.RemoveEmptyEntries);
        }

        #region method
        protected void OnGetDataComplete(bool isSuccess)
        {
            this._isGettingData = false;
            if (this._getDataCompleteCallback != null)
            {
                this._getDataCompleteCallback();
            }
        }

        void ClearData()
        {
            this._rowsData.Clear();
            this._statisticData = null;
            this._thongTinCty.Clear();
        }

        void SiteLoaded(object sender, HtmlDocumentLoadCompleted args)
        {
            if (args.Error != null)
            {
                OnGetDataComplete(false);
                return;
            }

            var tbStock = args.Document.DocumentNode.SelectNodes("/html[1]/body[1]/table[2]/tr[1]/td[1]/table[1]/tbody[1]/tr");

            if (tbStock == null)
                return;

            var i = 0;
            foreach (var tr in tbStock)
            {
                this._rowsData.Add(new RowData(this.FormatRowData(tr)) { Num = i, Index = i });
                i++;
            }

            this._statisticData = new StatisticData(FormatStatisticData(args.Document.DocumentNode))
            {
                CountTran = this._rowsData.Count(p => p.DGiaKhop == p.DTran),
                CountSan = this._rowsData.Count(p => p.DGiaKhop == p.DSan),
                CountThamChieu = this._rowsData.Count(p => p.DGiaKhop == p.DThamChieu),
                CountTang = this._rowsData.Count(p => p.DGiaKhop > p.DThamChieu),
                CountGiam = this._rowsData.Count(p => p.DGiaKhop < p.DThamChieu)
            };

            OnGetDataComplete(true);
        }

        static string FormatStatisticData(HtmlNode node)
        {
            var index = node.SelectSingleNode("//*[@id=\"IDX\"]").InnerText;
            var change = node.SelectSingleNode("//*[@id=\"IDXC\"]").InnerText.Split(' ')[0];
            var prev = (double.Parse(index) - double.Parse(change)).ToString("N2");
            var khoiluong = node.SelectSingleNode("//*[@id=\"QTY\"]").InnerText;
            var giatri = double.Parse(node.SelectSingleNode("//*[@id=\"VAL\"]").InnerText) / 1000000;
            var date = node.SelectSingleNode("//*[@id=\"MKDTE\"]").InnerText.Substring(0, 5);
            var time = node.SelectSingleNode("//*[@id=\"MKTIM\"]").InnerText;
            var status = node.SelectSingleNode("//*[@id=\"MKSTA\"]").InnerText;

            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", index, change, prev, 0, khoiluong, giatri, date, time, status);
        }

        static string FormatEmptyString(string s)
        {
            return s == "&nbsp;" ? "0" : s;
        }

        string FormatRowData(HtmlNode tr)
        {
            var tenCty = tr.ChildNodes[0].ChildNodes[1].OuterHtml.Split(',')[9].Split('-')[1];
            this._thongTinCty.Add(tr.ChildNodes[0].ChildNodes[0].Id, System.Net.HttpUtility.HtmlDecode(tenCty));

            var sb = new StringBuilder();
            sb.Append(tr.ChildNodes[0].ChildNodes[0].Id);
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[1].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[2].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[3].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[5].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[4].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[7].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[6].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[9].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[8].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[14].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[13].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[16].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[15].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[18].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[17].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[10].InnerText));
            sb.Append("|");
            sb.Append(FormatEmptyString(tr.ChildNodes[24].InnerText));
            return sb.ToString();
        }
        #endregion

        #region interface method
        public void RefreshData(Action callback)
        {
            if (this._isGettingData == true)
                return;

            this._getDataCompleteCallback = callback;

            this.ClearData();

            this._isGettingData = true;
            HtmlWeb.LoadAsync("http://board.hnx.vn/", SiteLoaded);

        }

        public bool IsInHotList(string item)
        {
            return this._hotList.Contains(item);;
        }

        public bool IsInTopList(int item)
        {
            return this._HNX30List.Contains(item);
        }
        #endregion
    }
}
