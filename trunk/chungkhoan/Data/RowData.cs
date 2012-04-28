using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;

namespace PhoneApp1
{
    public class RowsData : List<RowData>
    {

    }

    public class RowData
    {
        public bool IsSelected { get; set; }
        public int Index { get; set; }
        public int Num { get; set; }
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

        public string MaCkDisplay { get; set; }

        public SolidColorBrush TextColor { get; set; }
        public SolidColorBrush TextColorMua3 { get; set; }
        public SolidColorBrush TextColorMua2 { get; set; }
        public SolidColorBrush TextColorMua1 { get; set; }
        public SolidColorBrush TextColorBan3 { get; set; }
        public SolidColorBrush TextColorBan2 { get; set; }
        public SolidColorBrush TextColorBan1 { get; set; }

        public RowData(string row)
        {
            var data = row.Split('|');
            MaCk = data[Constant.Int_MaCk];
            IsSelected = DataService.Instance.HotList.Any(p => p == MaCk);
            ThamChieu = data[Constant.Int_ThamChieu];
            Tran = data[Constant.Int_Tran];
            San = data[Constant.Int_San];
            MuaGia3 = data[Constant.Int_MuaGia3];
            MuaKL3 = data[Constant.Int_MuaKL3];
            MuaGia2 = data[Constant.Int_MuaGia2];
            MuaKL2 = data[Constant.Int_MuaKL2];
            MuaGia1 = data[Constant.Int_MuaGia1];
            MuaKL1 = data[Constant.Int_MuaKL1];
            BanGia3 = data[Constant.Int_BanGia3];
            BanKL3 = data[Constant.Int_BanKL3];
            BanGia2 = data[Constant.Int_BanGia2];
            BanKL2 = data[Constant.Int_BanKL2];
            BanGia1 = data[Constant.Int_BanGia1];
            BanKL1 = data[Constant.Int_BanKL1];
            GiaKhop = data[Constant.Int_GiaKhop];
            KLGD = data[Constant.Int_KLGD];

            DThamChieu = Parse(data[Constant.Int_ThamChieu]);
            DTran = Parse(data[Constant.Int_Tran]);
            DSan = Parse(data[Constant.Int_San]);
            DGiaKhop = Parse(data[Constant.Int_GiaKhop]);
            ThayDoi = Math.Round((DGiaKhop - DThamChieu) * 100 / DThamChieu, 1).ToString("N1");

            MaCkDisplay = (DGiaKhop == DTran || DGiaKhop == DSan) ? "*" + MaCk : MaCk;

            DMuaGia1 = Parse(MuaGia1);
            DMuaGia2 = Parse(MuaGia2);
            DMuaGia3 = Parse(MuaGia3);

            DBanGia1 = Parse(BanGia1);
            DBanGia2 = Parse(BanGia2);
            DBanGia3 = Parse(BanGia3);

            TextColor = GetTextColor(DThamChieu, DGiaKhop);
            TextColorMua1 = GetTextColor(DThamChieu, DMuaGia1);
            TextColorMua2 = GetTextColor(DThamChieu, DMuaGia2);
            TextColorMua3 = GetTextColor(DThamChieu, DMuaGia3);
            TextColorBan1 = GetTextColor(DThamChieu, DBanGia1);
            TextColorBan2 = GetTextColor(DThamChieu, DBanGia2);
            TextColorBan3 = GetTextColor(DThamChieu, DBanGia3);
        }

        public static double Parse(string s)
        {
            double d;
            if (double.TryParse(s, out d) == true)
                return d;
            return -1;
        }

        public static SolidColorBrush GetTextColor(double giaThamChieu, double gia)
        {
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

        public static List<RowData> GetRowData(string data)
        {
            List<RowData> result = new List<RowData>();

            var rows = data.Split(new string[] { "**" }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            foreach (var row in rows)
            {
                result.Add(new RowData(row) { Num = i, Index = i});
                i++;
            }

            return result;
        }
    }
}
