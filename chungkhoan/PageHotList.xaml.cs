using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Controls;

namespace PhoneApp1
{
    public partial class PageHotList : PhoneApplicationPage
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        private string _searchText;
        public PageHotList()
        {
            InitializeComponent();
            this.btnOk.Click += new RoutedEventHandler(btnOk_Click);
            this.btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            this.btnSave.Click += new RoutedEventHandler(btnSave_Click);
            this.txtExcelFileUrl.GotFocus += new RoutedEventHandler(txtExcelFileUrl_GotFocus);
            this.txtSearch.TextChanged += new TextChangedEventHandler(txtSearch_TextChanged);
            this.txtSearch.GotFocus += new RoutedEventHandler(txtSearch_GotFocus);
            this.timer.Tick += new EventHandler(timer_Tick);
            this.Loaded += new RoutedEventHandler(PageHotList_Loaded);
        }

        void txtExcelFileUrl_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtExcelFileUrl.Select(0, this.txtExcelFileUrl.Text.Length);
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            using (var isoFileStream = new IsolatedStorageFileStream(Constant.ExcelFileUrl, FileMode.Create, myStore))
            {
                //Write the data
                using (var isoFileWriter = new StreamWriter(isoFileStream))
                {
                    isoFileWriter.Write(this.txtExcelFileUrl.Text);
                    DataService.Instance.ExcelFileUrl = this.txtExcelFileUrl.Text;
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.DataContext = DataService.Instance.RowsData.Where(p => p.MaCk.Contains(this._searchText));
        }

        void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtSearch.Select(0, this.txtSearch.Text.Length);
        }

        void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            this._searchText = txtSearch.Text.ToUpper();
            this.timer.Start();
        }

        void PageHotList_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = DataService.Instance.RowsData;
            this.txtExcelFileUrl.Text = DataService.Instance.ExcelFileUrl;
        }

        void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //// Obtain the virtual store for the application.
            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            using (var isoFileStream = new IsolatedStorageFileStream(Constant.HotListFile, FileMode.Create, myStore))
            {
                //Write the data
                using (var isoFileWriter = new StreamWriter(isoFileStream))
                {
                    List<string> hotlist = new List<string>();
                    foreach (var rowData in DataService.Instance.RowsData)
                    {
                        if (rowData.IsSelected == true)
                        {
                            isoFileWriter.Write(rowData.MaCk + "*");
                            hotlist.Add(rowData.MaCk);
                        }
                    }
                    DataService.Instance.HotList = hotlist.ToArray();
                }
            }
            //this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}