using System;
using System.Windows.Media;
using PhoneApp1.Helper;

namespace PhoneApp1.Data
{
    public class RowData
    {
        #region property
        public bool IsSelected { get; set; }
        public int Index { get; set; }
        public int Num { get; set; }
        public string TenCty { get; set; }
        public string MaCk { get; set; }
        public string ThamChieu { get; set; }
        public string Tran { get; set; }
        public string San { get; set; }
        public string MuaGia3 { get; set; }
        public string MuaKL3 { get; set; }
        public string MuaGia2 { get; set; }
        public string MuaKL2 { get; set; }
        public string MuaGia1 { get; set; }
        public string MuaKL1 { get; set; }
        public string GiaKhop { get; set; }
        public string ThayDoi { get; set; }
        public string BanGia3 { get; set; }
        public string BanKL3 { get; set; }
        public string BanGia2 { get; set; }
        public string BanKL2 { get; set; }
        public string BanGia1 { get; set; }
        public string BanKL1 { get; set; }
        public string KLGD { get; set; }

        public double DThamChieu { get; set; }
        public double DTran { get; set; }
        public double DSan { get; set; }
        public double DGiaKhop { get; set; }

        public double DMuaGia1 { get; set; }
        public double DMuaGia2 { get; set; }
        public double DMuaGia3 { get; set; }

        public double DBanGia1 { get; set; }
        public double DBanGia2 { get; set; }
        public double DBanGia3 { get; set; }

        public SolidColorBrush TextColor { get; set; }
        public SolidColorBrush TextColorMua3 { get; set; }
        public SolidColorBrush TextColorMua2 { get; set; }
        public SolidColorBrush TextColorMua1 { get; set; }
        public SolidColorBrush TextColorBan3 { get; set; }
        public SolidColorBrush TextColorBan2 { get; set; }
        public SolidColorBrush TextColorBan1 { get; set; }
        #endregion

        public RowData()
        {
        }

        public RowData(string row)
        {
            System.Diagnostics.Debug.WriteLine("RowData: " + row);

            var data = row.Split('|');

            MaCk = data[Constant.Int_MaCk];
            IsSelected = DataService.Instance.IsInHotList(MaCk);
            ThamChieu = data[Constant.Int_ThamChieu];
            Tran = data[Constant.Int_Tran];
            San = data[Constant.Int_San];
            MuaGia3 = data[Constant.Int_MuaGia3];
            MuaKL3 = StringHelper.FormatInt(data[Constant.Int_MuaKL3]);
            MuaGia2 = data[Constant.Int_MuaGia2];
            MuaKL2 = StringHelper.FormatInt(data[Constant.Int_MuaKL2]);
            MuaGia1 = data[Constant.Int_MuaGia1];
            MuaKL1 = StringHelper.FormatInt(data[Constant.Int_MuaKL1]);
            BanGia3 = data[Constant.Int_BanGia3];
            BanKL3 = StringHelper.FormatInt(data[Constant.Int_BanKL3]);
            BanGia2 = data[Constant.Int_BanGia2];
            BanKL2 = StringHelper.FormatInt(data[Constant.Int_BanKL2]);
            BanGia1 = data[Constant.Int_BanGia1];
            BanKL1 = StringHelper.FormatInt(data[Constant.Int_BanKL1]);
            GiaKhop = data[Constant.Int_GiaKhop];
            KLGD = StringHelper.FormatInt(data[Constant.Int_KLGD]);

            DThamChieu = StringHelper.ParseDouble(data[Constant.Int_ThamChieu]);
            DTran = StringHelper.ParseDouble(data[Constant.Int_Tran]);
            DSan = StringHelper.ParseDouble(data[Constant.Int_San]);
            DGiaKhop = StringHelper.ParseDouble(data[Constant.Int_GiaKhop]);
            ThayDoi = Math.Round((DGiaKhop - DThamChieu) * 100 / DThamChieu, 1).ToString("N1");

            TenCty = DataService.Instance.ThongTinCty[MaCk];

            DMuaGia1 = StringHelper.ParseDouble(MuaGia1);
            DMuaGia2 = StringHelper.ParseDouble(MuaGia2);
            DMuaGia3 = StringHelper.ParseDouble(MuaGia3);

            DBanGia1 = StringHelper.ParseDouble(BanGia1);
            DBanGia2 = StringHelper.ParseDouble(BanGia2);
            DBanGia3 = StringHelper.ParseDouble(BanGia3);

            TextColor = GetTextColor(DThamChieu, DGiaKhop, DTran, DSan);
            TextColorMua1 = GetTextColor(DThamChieu, DMuaGia1, DTran, DSan);
            TextColorMua2 = GetTextColor(DThamChieu, DMuaGia2, DTran, DSan);
            TextColorMua3 = GetTextColor(DThamChieu, DMuaGia3, DTran, DSan);
            TextColorBan1 = GetTextColor(DThamChieu, DBanGia1, DTran, DSan);
            TextColorBan2 = GetTextColor(DThamChieu, DBanGia2, DTran, DSan);
            TextColorBan3 = GetTextColor(DThamChieu, DBanGia3, DTran, DSan);
        }

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
