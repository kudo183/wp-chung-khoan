using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;

namespace PhoneApp1
{
    public class PumpingCollection<T>
    {
        private readonly ObservableCollection<T> _renderCollection = new ObservableCollection<T>();

        private IEnumerable<T> _renderDatas;

        public int Count { get; private set; }

        private readonly DispatcherTimer _timerPumping = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0) };

        private volatile bool _isSettingRenderData = false;

        public PumpingCollection()
        {
            this._timerPumping.Tick += _timerRender_Tick;
        }

        public event EventHandler PumpingFinished;
        public IEnumerable<T> RenderDatas
        {
            get { return _renderDatas; }
            set
            {
                _isSettingRenderData = true;
                this._timerPumping.Stop();
                _renderDatas = value;
                Count = _renderDatas.Count();
                this.RenderCollection.Clear();
                this._timerPumping.Start();
                _isSettingRenderData = false;
            }
        }

        public ObservableCollection<T> RenderCollection
        {
            get { return _renderCollection; }
        }

        void _timerRender_Tick(object sender, EventArgs e)
        {
            if (_isSettingRenderData == true)
                return;

            if (this.RenderCollection.Count == Count)
            {
                OnPumpingFinished();
                return;
            }

            this.RenderCollection.Add(this._renderDatas.ElementAt(this.RenderCollection.Count));
        }

        void OnPumpingFinished()
        {
            if (PumpingFinished != null)
                PumpingFinished(this, null);
        }

        public void Stop()
        {
            this._timerPumping.Stop();
        }
    }
}
