using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using PhoneApp1.Data;
using System.Windows.Threading;
using PhoneApp1.Utils;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        enum DataMode
        {
            All,
            Hot,
            Vn30
        }

        enum PriceMode
        {
            All,
            Tang,
            Giam,
            Tran,
            San,
            TC
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                if (_isFirstLoad == true)
                {
                    _isFirstLoad = false;
                    this.RefreshData();
                }
                else
                {
                    NavigationService.RemoveBackEntry();
                    NavigationService.RemoveBackEntry();
                    this.UpdateUI();
                }
            }
            catch
            {
            }

            base.OnNavigatedTo(e);
        }

        #region property

        private static DataMode _dataMode = DataMode.Hot;
        private static PriceMode _priceMode = PriceMode.All;

        private static bool _isFirstLoad = true;

        private readonly DispatcherTimer _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        private readonly DispatcherTimer _currentTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        private readonly PumpingCollection<RowData> _renderCollection = new PumpingCollection<RowData>();

        private volatile bool _isBusy = false;
        private string _searchText;

        #endregion

        #region Constructor
        public MainPage()
        {
            this.InitializeComponent();

            this.BackKeyPress += MainPage_BackKeyPress;

            this.txtSearch.TextChanged += txtSearch_TextChanged;
            this.txtSearch.GotFocus += txtSearch_GotFocus;
            this._renderCollection.PumpingFinished += _renderCollection_PumpingFinished;

            this.securityTable.listBox.SelectionChanged += listBox_SelectionChanged;
            this._timer.Tick += timer_Tick;
            this._currentTimer.Tick += currentTimer_Tick;
            this._currentTimer.Start();

            this.securityTable.DataContext = this._renderCollection.RenderCollection;

            Global.ExcelFileUrl = IsolatedStorageHelper.ReadFile(Constant.ExcelFileUrl);
        }

        void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit ?", "", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        #endregion

        void _renderCollection_PumpingFinished(object sender, EventArgs e)
        {
            Busy(false);
        }

        void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox.SelectedValue == null)
                return;

            string url = "http://www.cophieu68.vn/wap/historyprice.php?id=" + listBox.SelectedValue;
            url = HttpUtility.UrlEncode(url);
            NavigationService.Navigate(new Uri("/PageBrowser.xaml?url=" + url, UriKind.Relative));
        }

        #region timer

        void currentTimer_Tick(object sender, EventArgs e)
        {
            tbTime.Text = DateTime.Now.ToString("hh:mm");
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this._timer.Stop();
            this.UpdateUI();
        }

        #endregion

        #region application bar button

        void appBtnHotList_Click(object sender, EventArgs e)
        {
            MainPage._dataMode = DataMode.Hot;
            this.NavigationService.Navigate(new Uri("/PageHotList.xaml", UriKind.Relative));
        }

        void appBtnRefresh_Click(object sender, EventArgs e)
        {
            if (_isBusy == true)
            {
                return;
            }

            ((ApplicationBarIconButton)sender).IsEnabled = false;
            Busy(true);
            DataService.Instance.RefreshData(() =>
            {
                this.UpdateUI(true);
                ((ApplicationBarIconButton)sender).IsEnabled = true;
                Busy(false);
            });
        }

        void appBtnFile_Click(object sender, EventArgs e)
        {
            var url = Global.ExcelFileUrl;
            if (string.IsNullOrEmpty(url))
                return;

            var webBrowserTask = new WebBrowserTask
            {
                Uri = new Uri(url, UriKind.Absolute)
            };
            webBrowserTask.Show();
        }

        void appBtnACB_Click(object sender, EventArgs e)
        {
            //DataService.Mode = DataService.Mode == DataService.DataSource.HSX ? DataService.DataSource.HNX : DataService.DataSource.HSX;
            //tbSan.Text = DataService.Mode.ToString();
            this.RefreshData();
        }

        #endregion

        #region menu

        void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtSearch.Select(0, this.txtSearch.Text.Length);
        }

        void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._searchText = txtSearch.Text.ToUpper();
            this._timer.Start();
        }

        private void DataModeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string action = (sender as MenuItem).Header.ToString();
            btnDataMode.Content = action;

            switch (action)
            {
                case "All":
                    _dataMode = DataMode.All;
                    break;
                case "Hot":
                    _dataMode = DataMode.Hot;
                    break;
                case "VN30":
                    _dataMode = DataMode.Vn30;
                    break;
            }

            this.SwitchMode();
        }

        private void PriceModeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string action = (sender as MenuItem).Header.ToString();
            btnPriceMode.Content = action;

            switch (action)
            {
                case "All":
                    _priceMode = PriceMode.All;
                    break;
                case "Tang":
                    _priceMode = PriceMode.Tang;
                    break;
                case "Giam":
                    _priceMode = PriceMode.Giam;
                    break;
                case "Tran":
                    _priceMode = PriceMode.Tran;
                    break;
                case "San":
                    _priceMode = PriceMode.San;
                    break;
                case "TC":
                    _priceMode = PriceMode.TC;
                    break;
            }

            this.SwitchMode();
        }

        private void btnPriceMode_Click(object sender, RoutedEventArgs e)
        {
            this.PriceModeContextMenu.IsOpen = true;
        }

        private void btnDataMode_Click(object sender, RoutedEventArgs e)
        {
            this.DataModeContextMenu.IsOpen = true;
        }

        #endregion

        #region method

        void SwitchMode()
        {
            Busy(true);
            this.txtSearch.Text = string.Empty;
            this._searchText = string.Empty;
            this.UpdateUI();
        }

        void Busy(bool isBusy)
        {
            _isBusy = isBusy;
            if (isBusy == true)
            {
                tbTime.FontStyle = FontStyles.Italic;
                this.progressBar.IsIndeterminate = true;
                return;
            }

            tbTime.FontStyle = FontStyles.Normal;
            this.progressBar.IsIndeterminate = false;
        }

        void RefreshData()
        {
            if (_isBusy == true)
            {
                return;
            }

            Busy(true);
            DataService.Instance.RefreshData(() =>
            {
                this.UpdateUI();
            });
        }

        void UpdateUI(bool isRefresh = false)
        {
            this.statistic.DataContext = DataService.Instance.StatisticData;

            if (DataService.Instance.RowsData == null)
            {
                return;
            }

            IEnumerable<RowData> renderDatas = DataService.Instance.RowsData;

            Func<RowData, bool> func = FilterFunc(_priceMode, _dataMode);

            if (func != null)
            {
                renderDatas = renderDatas.Where(func);
            }
            if (string.IsNullOrEmpty(this._searchText) == false)
            {
                renderDatas = renderDatas.Where(p => p.MaCk == this._searchText);
            }

            var count = renderDatas.Count();
            for (var i = 0; i < count; i++)
                renderDatas.ElementAt(i).Num = i;

            this.tbCount.Text = count.ToString();

            if (isRefresh == false)
            {
                this._renderCollection.RenderDatas = renderDatas;
            }
            else
            {
                var dicData = renderDatas.ToDictionary(p => p.MaCk);

                foreach (var rowData in this._renderCollection.RenderCollection)
                {
                    var data = dicData[rowData.MaCk];
                    rowData.UpdateRowData(data);
                }
            }
        }

        #endregion

        private static Func<RowData, bool> DataModeFilterFunc(DataMode dataMode)
        {
            Func<RowData, bool> func = null;

            switch (dataMode)
            {
                case DataMode.All:
                    break;
                case DataMode.Hot:
                    func = (p => DataService.Instance.IsInHotList(p.MaCk));
                    break;
                case DataMode.Vn30:
                    func = (p => DataService.Instance.IsInTopList(p.Index));
                    break;
            }

            return func;
        }

        private static Func<RowData, bool> PriceModeFilterFunc(PriceMode priceMode)
        {
            Func<RowData, bool> func = (p => true);

            switch (priceMode)
            {
                case PriceMode.All:
                    break;
                case PriceMode.Tran:
                    func = (p => p.DGiaKhop == p.DTran);
                    break;
                case PriceMode.TC:
                    func = (p => p.DGiaKhop == p.DThamChieu);
                    break;
                case PriceMode.San:
                    func = (p => p.DGiaKhop == p.DSan);
                    break;
                case PriceMode.Tang:
                    func = (p => p.DGiaKhop > p.DThamChieu);
                    break;
                case PriceMode.Giam:
                    func = (p => p.DGiaKhop < p.DThamChieu);
                    break;
            }

            return func;
        }

        private static Func<RowData, bool> FilterFunc(PriceMode priceMode, DataMode dataMode)
        {
            var func1 = PriceModeFilterFunc(priceMode);
            var func2 = DataModeFilterFunc(dataMode);

            return AndPredict(func1, func2);
        }

        private static Func<T, bool> AndPredict<T>(Func<T, bool> func1, Func<T, bool> func2)
        {
            if (func1 == null && func2 == null)
            {
                return null;
            }

            if (func1 == null)
            {
                return func2;
            }

            if (func2 == null)
            {
                return func1;
            }

            return (p => func1(p) && func2(p));
        }
    }
}