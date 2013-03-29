using System.Windows.Media;

namespace PhoneApp1.Helper
{
    public class TextColorUtils
    {
        public static SolidColorBrush GetTextColor(double giaThamChieu, double gia, double tran, double san)
        {
            if (gia == tran)
            {
                return Constant.SolidColorBrush_MauTran;
            }
            if (gia == san)
            {
                return Constant.SolidColorBrush_MauSan;
            }

            if (gia > giaThamChieu)
            {
                return Constant.SolidColorBrush_MauTang;
            }
            if (gia < giaThamChieu)
            {
                return Constant.SolidColorBrush_MauGiam;
            }

            return Constant.SolidColorBrush_MauThamChieu;
        }
    }
}
