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

        private SolidColorBrush _textColor;
        public SolidColorBrush TextColor
        {
            get { return _textColor; }
            set
            {
                if (_textColor == value)
                    return;
                _textColor = value;

                OnPropertyChanged("TextColor");
            }
        }

        private SolidColorBrush _textColorMua3;
        public SolidColorBrush TextColorMua3
        {
            get { return _textColorMua3; }
            set
            {
                if (_textColorMua3 == value)
                    return;
                _textColorMua3 = value;

                OnPropertyChanged("TextColorMua3");
            }
        }

        private SolidColorBrush _textColorMua2;
        public SolidColorBrush TextColorMua2
        {
            get { return _textColorMua2; }
            set
            {
                if (_textColorMua2 == value)
                    return;
                _textColorMua2 = value;

                OnPropertyChanged("TextColorMua2");
            }
        }

        private SolidColorBrush _textColorMua1;
        public SolidColorBrush TextColorMua1
        {
            get { return _textColorMua1; }
            set
            {
                if (_textColorMua1 == value)
                    return;
                _textColorMua1 = value;

                OnPropertyChanged("TextColorMua1");
            }
        }

        private SolidColorBrush _textColorBan3;
        public SolidColorBrush TextColorBan3
        {
            get { return _textColorBan3; }
            set
            {
                if (_textColorBan3 == value)
                    return;
                _textColorBan3 = value;

                OnPropertyChanged("TextColorBan3");
            }
        }

        private SolidColorBrush _textColorBan2;
        public SolidColorBrush TextColorBan2
        {
            get { return _textColorBan2; }
            set
            {
                if (_textColorBan2 == value)
                    return;
                _textColorBan2 = value;

                OnPropertyChanged("TextColorBan2");
            }
        }

        private SolidColorBrush _textColorBan1;
        public SolidColorBrush TextColorBan1
        {
            get { return _textColorBan1; }
            set
            {
                if (_textColorBan1 == value)
                    return;
                _textColorBan1 = value;

                OnPropertyChanged("TextColorBan1");
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

        #endregion

        public RowData()
        {
        }

        public RowData(string row)
        {
            System.Diagnostics.Debug.WriteLine("RowData: " + row);

            var data = row.Split('|');

            MaCk = data[Constant.Int_MaCk];
            TenCty = DataService.Instance.ThongTinCty[MaCk];
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

            DThamChieu = StringHelper.ParseDouble(ThamChieu);
            DTran = StringHelper.ParseDouble(Tran);
            DSan = StringHelper.ParseDouble(San);

            UpdateTextColor();
        }

        public void UpdateTextColor()
        {
            DGiaKhop = StringHelper.ParseDouble(GiaKhop);

            DMuaGia1 = StringHelper.ParseDouble(MuaGia1);
            DMuaGia2 = StringHelper.ParseDouble(MuaGia2);
            DMuaGia3 = StringHelper.ParseDouble(MuaGia3);

            DBanGia1 = StringHelper.ParseDouble(BanGia1);
            DBanGia2 = StringHelper.ParseDouble(BanGia2);
            DBanGia3 = StringHelper.ParseDouble(BanGia3);

            ThayDoi = (DGiaKhop == 0) ? "0" : Math.Round((DGiaKhop - DThamChieu) * 100 / DThamChieu, 1).ToString("N1");

            TextColor = TextColorUtils.GetTextColor(DThamChieu, DGiaKhop, DTran, DSan);
            TextColorMua1 = TextColorUtils.GetTextColor(DThamChieu, DMuaGia1, DTran, DSan);
            TextColorMua2 = TextColorUtils.GetTextColor(DThamChieu, DMuaGia2, DTran, DSan);
            TextColorMua3 = TextColorUtils.GetTextColor(DThamChieu, DMuaGia3, DTran, DSan);
            TextColorBan1 = TextColorUtils.GetTextColor(DThamChieu, DBanGia1, DTran, DSan);
            TextColorBan2 = TextColorUtils.GetTextColor(DThamChieu, DBanGia2, DTran, DSan);
            TextColorBan3 = TextColorUtils.GetTextColor(DThamChieu, DBanGia3, DTran, DSan);
        }

        public void UpdateRowData(RowData row)
        {
            this.GiaKhop = row.GiaKhop;

            this.KLTH = row.KLTH;
            this.TKLGD = row.TKLGD;
            this.MuaGia1 = row.MuaGia1;
            this.MuaGia2 = row.MuaGia2;
            this.MuaGia3 = row.MuaGia3;
            this.MuaKL1 = row.MuaKL1;
            this.MuaKL2 = row.MuaKL2;
            this.MuaKL3 = row.MuaKL3;
            this.BanGia1 = row.BanGia1;
            this.BanGia2 = row.BanGia2;
            this.BanGia3 = row.BanGia3;
            this.BanKL1 = row.BanKL1;
            this.BanKL2 = row.BanKL2;
            this.BanKL3 = row.BanKL3;

            this.UpdateTextColor();
        }
    }
}
