using System.Windows.Media;

namespace PhoneApp1
{
    public class Constant
    {
        public const string ExcelFileUrl = "ExcelFileUrl.txt";

        public static Color Color_MauTang = Color.FromArgb(0xff, 0x33, 0xFF, 0x00);
        public static Color Color_MauGiam = Colors.Red;
        public static Color Color_MauTran = Color.FromArgb(0xff, 0xFF, 0x00, 0xFF);
        public static Color Color_MauSan = Colors.Cyan;
        public static Color Color_MauThamChieu = Colors.Yellow;

        public static SolidColorBrush SolidColorBrush_MauTang = new SolidColorBrush(Constant.Color_MauTang);
        public static SolidColorBrush SolidColorBrush_MauGiam = new SolidColorBrush(Constant.Color_MauGiam);
        public static SolidColorBrush SolidColorBrush_MauTran = new SolidColorBrush(Constant.Color_MauTran);
        public static SolidColorBrush SolidColorBrush_MauSan = new SolidColorBrush(Constant.Color_MauSan);
        public static SolidColorBrush SolidColorBrush_MauThamChieu = new SolidColorBrush(Constant.Color_MauThamChieu);

        public const int Int_VnIndex = 0;
        public const int Int_VnIndexChange = 1;
        public const int Int_PrevVNIndex = 2;
        public const int Int_SoLuongGiaoDich = 3;
        public const int Int_KhoiLuongKhopLenh = 4;
        public const int Int_GiaTriKhopLenh = 5;
        public const int Int_Ngay = 6;
        public const int Int_Gio = 7;
        public const int Int_Phien = 8;

        public const int Int_MaCk = 0;
        public const int Int_ThamChieu = 1;
        public const int Int_Tran = 2;
        public const int Int_San = 3;
        public const int Int_MuaGia3 = 4;
        public const int Int_MuaKL3 = 5;
        public const int Int_MuaGia2 = 6;
        public const int Int_MuaKL2 = 7;
        public const int Int_MuaGia1 = 8;
        public const int Int_MuaKL1 = 9;
        public const int Int_BanGia3 = 10;
        public const int Int_BanKL3 = 11;
        public const int Int_BanGia2 = 12;
        public const int Int_BanKL2 = 13;
        public const int Int_BanGia1 = 14;
        public const int Int_BanKL1 = 15;
        public const int Int_GiaKhop = 16;
        public const int Int_KLGD = 17;
    }
}
