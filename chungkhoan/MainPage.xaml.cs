using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using PhoneApp1.Data;
using System.Windows.Threading;
using PhoneApp1.Utils;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        enum Mode
        {
            All,
            Tran,
            San,
            Hot,
            Vn30,
            ThamChieu
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
        private static Mode _mode = Mode.Hot;
        private static bool _isFirstLoad = true;

        private readonly DispatcherTimer _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        private readonly DispatcherTimer _currentTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        private readonly PumpingCollection<RowData> _renderCollection = new PumpingCollection<RowData>();

        private string _searchText;
        #endregion

        // Constructor
        public MainPage()
        {
            this.InitializeComponent();
            this.btnAll.Click += btnAll_Click;
            this.btnTran.Click += btnTran_Click;
            this.btnSan.Click += btnSan_Click;
            this.btnThamChieu.Click += btnThamChieu_Click;
            this.btnHot.Click += btnQuanTam_Click;
            this.btnVN30.Click += btnVN30_Click;
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

        void _renderCollection_PumpingFinished(object sender, EventArgs e)
        {
            Busy(false);
        }

        void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox.SelectedValue == null)
                return;

            string url = "http://data.cophieu68.com/wap/historyprice.php?id=" + listBox.SelectedValue;
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
            MainPage._mode = Mode.Hot;
            this.NavigationService.Navigate(new Uri("/PageHotList.xaml", UriKind.Relative));
        }

        void appBtnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshData();
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
            DataService.Mode = DataService.Mode == DataService.DataSource.HSX ? DataService.DataSource.HNX : DataService.DataSource.HSX;
            tbSan.Text = DataService.Mode.ToString();
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

        void btnThamChieu_Click(object sender, RoutedEventArgs e)
        {
            this.switchMode(Mode.ThamChieu);
        }

        void btnAll_Click(object sender, RoutedEventArgs e)
        {
            this.switchMode(Mode.All);
        }

        void btnVN30_Click(object sender, RoutedEventArgs e)
        {
            this.switchMode(Mode.Vn30);
        }

        void btnQuanTam_Click(object sender, RoutedEventArgs e)
        {
            this.switchMode(Mode.Hot);
        }

        void btnSan_Click(object sender, RoutedEventArgs e)
        {
            this.switchMode(Mode.San);
        }

        void btnTran_Click(object sender, RoutedEventArgs e)
        {
            this.switchMode(Mode.Tran);
        }
        #endregion

        #region method
        void switchMode(Mode m)
        {
            Busy(true);
            this.txtSearch.Text = string.Empty;
            this._searchText = string.Empty;
            MainPage._mode = m;
            this.UpdateUI();
        }

        void Busy(bool isBusy)
        {
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
            Busy(true);
            DataService.Instance.RefreshData(() =>
            {
                this.UpdateUI();
            });
        }

        void UpdateUI()
        {
            this.statistic.DataContext = DataService.Instance.StatisticData;

            this.btnAll.IsEnabled = true;
            this.btnTran.IsEnabled = true;
            this.btnSan.IsEnabled = true;
            this.btnHot.IsEnabled = true;
            this.btnThamChieu.IsEnabled = true;
            this.btnVN30.IsEnabled = true;

            IEnumerable<RowData> renderDatas = null;

            switch (_mode)
            {
                case Mode.All:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.MaCk == this._searchText);
                    }
                    else
                    {
                        renderDatas = DataService.Instance.RowsData;
                    }
                    this.btnAll.IsEnabled = false;
                    break;
                case Mode.Hot:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => DataService.Instance.IsInHotList(p.MaCk) && p.MaCk == this._searchText);
                    }
                    else
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => DataService.Instance.IsInHotList(p.MaCk));
                    }
                    this.btnHot.IsEnabled = false;
                    break;
                case Mode.Tran:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.DGiaKhop == p.DTran && p.MaCk == this._searchText);
                    }
                    else
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.DGiaKhop == p.DTran);
                    }
                    this.btnTran.IsEnabled = false;
                    break;
                case Mode.ThamChieu:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.DGiaKhop == p.DThamChieu && p.MaCk == this._searchText);
                    }
                    else
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.DGiaKhop == p.DThamChieu);
                    }
                    this.btnThamChieu.IsEnabled = false;
                    break;
                case Mode.San:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.DGiaKhop == p.DSan && p.MaCk == this._searchText);
                    }
                    else
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => p.DGiaKhop == p.DSan);
                    }
                    this.btnSan.IsEnabled = false;
                    break;
                case Mode.Vn30:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => DataService.Instance.IsInTopList(p.Index) && p.MaCk == this._searchText);
                    }
                    else
                    {
                        renderDatas = DataService.Instance.RowsData.Where(p => DataService.Instance.IsInTopList(p.Index));
                    }
                    this.btnVN30.IsEnabled = false;
                    break;
            }

            if (renderDatas == null)
                return;

            var count = renderDatas.Count();
            for (var i = 0; i < count; i++)
                renderDatas.ElementAt(i).Num = i;

            this.tbCount.Text = count.ToString();

            this._renderCollection.RenderDatas = renderDatas;
        }
        #endregion
    }
}