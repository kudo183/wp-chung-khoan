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

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static List<RowData> _rowsData;
        private static StatisticData _statisticData;

        enum Mode
        {
            All,
            Tran,
            San,
            Hot,
            VN30,
            ThamChieu
        }

        private static Mode _mode = Mode.Hot;
        private string _searchText;
        private static bool isFirstLoad = true;
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        DispatcherTimer currentTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                if (isFirstLoad == true)
                {
                    isFirstLoad = false;
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
        // Constructor
        public MainPage()
        {
            this.InitializeComponent();
            this.btnAll.Click += new RoutedEventHandler(btnAll_Click);
            this.btnTran.Click += btnTran_Click;
            this.btnSan.Click += btnSan_Click;
            this.btnThamChieu.Click += new RoutedEventHandler(btnThamChieu_Click);
            this.btnHot.Click += btnQuanTam_Click;
            this.btnVN30.Click += btnVN30_Click;
            this.txtSearch.TextChanged += txtSearch_TextChanged;
            this.txtSearch.GotFocus += new RoutedEventHandler(txtSearch_GotFocus);

            securityTable.listBox.SelectionChanged += new SelectionChangedEventHandler(listBox_SelectionChanged);
            this.timer.Tick += new EventHandler(timer_Tick);
            this.currentTimer.Tick += new EventHandler(currentTimer_Tick);
            this.currentTimer.Start();

            tbTime.Text = currentTime();
        }

        string currentTime()
        {
            return DateTime.Now.ToString("hh:mm");
        }

        void currentTimer_Tick(object sender, EventArgs e)
        {
            tbTime.Text = currentTime();
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

        void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtSearch.Select(0, this.txtSearch.Text.Length);
        }

        void appBtnHotList_Click(object sender, EventArgs e)
        {
            MainPage._mode = Mode.Hot;
            this.NavigationService.Navigate(new Uri("/PageHotList.xaml", UriKind.Relative));
        }

        void appBtnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void appBtnFile_Click(object sender, EventArgs e)
        {
            var url = DataService.Instance.ExcelFileUrl;
            if (string.IsNullOrEmpty(url))
                return;

            //url = HttpUtility.UrlEncode(url);
            //NavigationService.Navigate(new Uri("/PageBrowser.xaml?url=" + url, UriKind.Relative));
            var webBrowserTask = new WebBrowserTask
            {
                Uri = new Uri(url, UriKind.Absolute)
            };
            webBrowserTask.Show();
        }

        private void appBtnACB_Click(object sender, EventArgs e)
        {
            string url = "https://www.acbonline.com.vn";
            url = HttpUtility.UrlEncode(url);
            NavigationService.Navigate(new Uri("/PageBrowser.xaml?cal=0&url=" + url, UriKind.Relative));
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.UpdateUI();
        }

        void RefreshData()
        {
            this.progressBar.Visibility = System.Windows.Visibility.Visible;
            this.progressBar.IsIndeterminate = true;
            DataService.Instance.RefreshData((c) =>
            {
                MainPage._rowsData = c.RowsData;
                MainPage._statisticData = c.StatisticData;

                this.progressBar.IsIndeterminate = false;
                this.progressBar.Visibility = System.Windows.Visibility.Collapsed;
                this.UpdateUI();
            });
        }

        void UpdateUI()
        {
            this.statistic.DataContext = MainPage._statisticData;
            
            this.btnAll.IsEnabled = true;
            this.btnTran.IsEnabled = true;
            this.btnSan.IsEnabled = true;
            this.btnHot.IsEnabled = true;
            this.btnThamChieu.IsEnabled = true;
            this.btnVN30.IsEnabled = true;

            IEnumerable<RowData> rowDatas = null;
            switch (_mode)
            {
                case Mode.All:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.MaCk == this._searchText);
                    }
                    else
                    {
                        rowDatas = MainPage._rowsData;
                    }
                    btnAll.IsEnabled = false;
                    break;
                case Mode.Hot:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        rowDatas = MainPage._rowsData.Where(p => DataService.Instance.HotList.Contains(p.MaCk) && p.MaCk == this._searchText);
                    }
                    else
                    {
                        rowDatas = MainPage._rowsData.Where(p => DataService.Instance.HotList.Contains(p.MaCk));
                    }
                    btnHot.IsEnabled = false;
                    break;
                case Mode.Tran:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.DGiaKhop == p.DTran && p.MaCk == this._searchText);
                    }
                    else
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.DGiaKhop == p.DTran);
                    }
                    btnTran.IsEnabled = false;
                    break;
                case Mode.ThamChieu:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.DGiaKhop == p.DThamChieu && p.MaCk == this._searchText);
                    }
                    else
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.DGiaKhop == p.DThamChieu);
                    }
                    btnThamChieu.IsEnabled = false;
                    break;
                case Mode.San:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.DGiaKhop == p.DSan && p.MaCk == this._searchText);
                    }
                    else
                    {
                        rowDatas = MainPage._rowsData.Where(p => p.DGiaKhop == p.DSan);
                    }
                    btnSan.IsEnabled = false;
                    break;
                case Mode.VN30:
                    if (string.IsNullOrEmpty(this._searchText) == false)
                    {
                        rowDatas = MainPage._rowsData.Where(p => DataService.Instance.VN30List.Contains(p.Index) && p.MaCk == this._searchText);
                    }
                    else
                    {
                        rowDatas = MainPage._rowsData.Where(p => DataService.Instance.VN30List.Contains(p.Index));
                    }
                    btnVN30.IsEnabled = false;
                    break;
            }

            int count = rowDatas.Count();
            for (int i = 0; i < count; i++)
                rowDatas.ElementAt(i).Num = i;

            tbCount.Text = count.ToString();
            securityTable.DataContext = rowDatas;
            securityTable.UpdateLayout();
        }

        void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = txtSearch.Text.ToUpper();
            timer.Start();
        }

        void btnThamChieu_Click(object sender, RoutedEventArgs e)
        {
            switchMode(Mode.ThamChieu);
        }

        void btnAll_Click(object sender, RoutedEventArgs e)
        {
            switchMode(Mode.All);
        }

        void btnVN30_Click(object sender, RoutedEventArgs e)
        {
            switchMode(Mode.VN30);
        }

        void btnQuanTam_Click(object sender, RoutedEventArgs e)
        {
            switchMode(Mode.Hot);
        }

        void btnSan_Click(object sender, RoutedEventArgs e)
        {
            switchMode(Mode.San);
        }

        void btnTran_Click(object sender, RoutedEventArgs e)
        {
            switchMode(Mode.Tran);
        }

        void switchMode(Mode m)
        {
            txtSearch.Text = string.Empty;
            _searchText = string.Empty;
            MainPage._mode = m;
            UpdateUI();
        }
    }
}