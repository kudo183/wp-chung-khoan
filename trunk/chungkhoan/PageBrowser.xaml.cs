using System;
using Microsoft.Phone.Controls;

namespace PhoneApp1
{
    public partial class PageBrowser : PhoneApplicationPage
    {
        public PageBrowser()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this.NavigationContext.QueryString.ContainsKey("cal")==true)
            {
                int i = int.Parse(this.NavigationContext.QueryString["cal"]);
                calculator.Visibility = (i == 0) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            }
            webBrowser1.Source = new Uri(this.NavigationContext.QueryString["url"], UriKind.Absolute);
        }
    }
}