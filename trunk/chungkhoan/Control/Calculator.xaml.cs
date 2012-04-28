using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhoneApp1
{
    public partial class Calculator : UserControl
    {
        public Calculator()
        {
            InitializeComponent();
            this.btn.Click += new RoutedEventHandler(btn_Click);
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            double num1, num2;
            if (double.TryParse(txt1.Text, out num1) == false)
                return;
            if (double.TryParse(txt2.Text, out num2) == false)
                return;

            double percent = (num1 - num2) * 100 / num2;
            txtResult.Text = percent.ToString("N2") + " %";
        }
    }
}
