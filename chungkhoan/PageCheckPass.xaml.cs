using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace PhoneApp1
{
    public partial class PageCheckPass : PhoneApplicationPage
    {
        public PageCheckPass()
        {
            InitializeComponent();
            btnOk.Click += new RoutedEventHandler(btnOk_Click);
            Loaded += new RoutedEventHandler(PageCheckPass_Loaded);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.IsNavigationInitiator == false)
            {
                this.NavigationService.Navigate(new Uri("/MainPage.xaml?mode=Hot", UriKind.Relative));
                return;
            }
            base.OnNavigatedTo(e);
        }

        void PageCheckPass_Loaded(object sender, RoutedEventArgs e)
        {
            passBox.Focus();
        }

        void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (passBox.Password != "kudo")
            {
                this.NavigationService.Navigate(new Uri("/MainPage.xaml?mode=Hot", UriKind.Relative));
                return;
            }

            string url = "https://skydrive.live.com/MobileProtocol?ptcl=office%3a&dav=https%3a%2f%2fd.docs.live.net%2ff1b769de785994c7%2f%5e.Documents%2fck.xlsx";
            //url = HttpUtility.UrlEncode(url);
            //NavigationService.Navigate(new Uri("/PageBrowser.xaml?url=" + url, UriKind.Relative));
            var webBrowserTask = new WebBrowserTask
            {
                Uri = new Uri(url, UriKind.Absolute)
            };
            webBrowserTask.Show();
        }
    }
}