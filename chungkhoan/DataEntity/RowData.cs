using System;
using System.ComponentModel;
using System.Windows.Media;
using PhoneApp1.Helper;

namespace PhoneApp1.Data
{
    public class RowData : INotifyPropertyChanged
    {
        #region property notify change

        private string _muaGia3;
        public string MuaGia3
        {
            get { return _muaGia3; }
            set
            {
                if (_muaGia3 == value)
                    return;

                _muaGia3 = value;
                OnPropertyChanged("MuaGia3");
            }
        }

        private string _muaKl3;
        public string MuaKL3
        {
            get { return _muaKl3; }
            set
            {
                if (_muaKl3 == value)
                    return;

                _muaKl3 = value;
                OnPropertyChanged("MuaKL3");
            }
        }

        private string _muaGia2;
        public string MuaGia2
        {
            get { return _muaGia2; }
            set
            {
                if (_muaGia2 == value)
                    return;

                _muaGia2 = value;
                OnPropertyChanged("MuaGia2");
            }
        }

        private string _muaKl2;
        public string MuaKL2
        {
            get { return _muaKl2; }
            set
            {
                if (_muaKl2 == value)
                    return;

                _muaKl2 = value;
                OnPropertyChanged("MuaKL2");
            }
        }

        private string _muaGia1;
        public string MuaGia1
        {
            get { return _muaGia1; }
            set
            {
                if (_muaGia1 == value)
                    return;

                _muaGia1 = value;
                OnPropertyChanged("MuaGia1");
            }
        }

        private string _muaKl1;
        public string MuaKL1
        {
            get { return _muaKl1; }
            set
            {
                if (_muaKl1 == value)
                    return;

                _muaKl1 = value;
                OnPropertyChanged("MuaKL1");
            }
        }

        private string _giaKhop;
        public string GiaKhop
        {
            get { return _giaKhop; }
            set
            {
                if (_giaKhop == value)
                    return;

                _giaKhop = value;
                OnPropertyChanged("GiaKhop");
            }
        }

        private string _thayDoi;
        public string ThayDoi
        {
            get { return _thayDoi; }
            set
            {
                if (_thayDoi == value)
                    return;

                _thayDoi = value;
                OnPropertyChanged("ThayDoi");
            }
        }

        private string _banGia3;
        public string BanGia3
        {
            get { return _banGia3; }
            set
            {
                if (_banGia3 == value)
                    return;

                _banGia3 = value;
                OnPropertyChanged("BanGia3");
            }
        }

        private string _banKl3;
        public string BanKL3
        {
            get { return _banKl3; }
            set
            {
                if (_banKl3 == value)
                    return;

                _banKl3 = value;
                OnPropertyChanged("BanKL3");
            }
        }

        private string _banGia2;
        public string BanGia2
        {
            get { return _banGia2; }
            set
            {
                if (_banGia2 == value)
                    return;

                _banGia2 = value;
                OnPropertyChanged("BanGia2");
            }
        }

        private string _banKl2;
        public string BanKL2
        {
            get { return _banKl2; }
            set
            {
                if (_banKl2 == value)
                    return;

                _banKl2 = value;
                OnPropertyChanged("BanKL2");
            }
        }

        private string _banGia1;
        public string BanGia1
        {
            get { return _banGia1; }
            set
            {
                if (_banGia1 == value)
                    return;

                _banGia1 = value;
                OnPropertyChanged("BanGia1");
            }
        }

        private string _banKl1;
        public string BanKL1
        {
            get { return _banKl1; }
            set
            {
                if (_banKl1 == value)
                    return;

                _banKl1 = value;
                OnPropertyChanged("BanKL1");
            }
        }

        private string _tklgd;
        public string TKLGD
        {
            get { return _tklgd; }
            set
            {
                if (_tklgd == value)
                    return;

                _tklgd = value;
                OnPropertyChanged("TKLGD");
            }
        }

        private string _klth;
        public string KLTH
        {
            get { return _klth; }
            set
            {
                if (_klth == value)
                    return;

                _klth = value;
                OnPropertyChanged("KLTH");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region property
        public bool IsSelected { get; set; }
        public int Index { get; set; }
        public int Num { get; set; }
        public string TenCty { get; set; }
        public string MaCk { get; set; }
        public string ThamChieu { get; set; }
        public string Tran { get; set; }
        public string San { get; set; }

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

            KLTH = StringHelper.FormatInt(data[Constant.Int_KLTH]);
            TKLGD = StringHelper.FormatInt(data[Constant.Int_TKLGD]);

            DThamChieu = StringHelper.ParseDouble(data[Constant.Int_ThamChieu]);
            DTran = StringHelper.ParseDouble(data[Constant.Int_Tran]);
            DSan = StringHelper.ParseDouble(data[Constant.Int_San]);
            DGiaKhop = StringHelper.ParseDouble(data[Constant.Int_GiaKhop]);

            ThayDoi = (DGiaKhop == 0) ? "0" : Math.Round((DGiaKhop - DThamChieu) * 100 / DThamChieu, 1).ToString("N1");

            TenCty = DataService.Instance.ThongTinCty[MaCk];

            DMuaGia1 = StringHelper.ParseDouble(MuaGia1);
            DMuaGia2 = StringHelper.ParseDouble(MuaGia2);
            DMuaGia3 = StringHelper.ParseDouble(MuaGia3);

            DBanGia1 = StringHelper.ParseDouble(BanGia1);
            DBanGia2 = StringHelper.ParseDouble(BanGia2);
            DBanGia3 = StringHelper.ParseDouble(BanGia3);

            UpdateTextColor();
        }

        public void UpdateTextColor()
        {
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
