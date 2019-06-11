using System;
using System.Runtime.InteropServices;
using System.Threading;
using static N2k3Dash.Util.NR2003Types;

namespace N2k3Dash.Model
{


    /// <summary>
    /// Singleton class that "runs" the tachometer. This entails constantly checking for telemetry updates 
    /// from the game on a separate thread and firing an event if it gets a new data point.
    /// </summary>
    public class Gauge
    {
        private static Gauge _obj;
        /// <summary>
        /// Event that fires if the gauge has been updated.
        /// </summary>
        public event EventHandler<GaugeUpdatedEventArgs> GaugeUpdated;


        public event EventHandler<LapTimeUpdatedEventArgs> LapTimeUpdated;
        /// <summary>
        /// Event that fires if NR2003 has loaded or thrown an error.
        /// </summary>
        public event EventHandler<bool> NR2003LoadedOrThrewError;

        /// <summary>
        /// Event that fires if the request for NR2003 data has been successfully requested or if the application encountered an error.
        /// </summary>
        public event EventHandler<bool> AddressSpaceLoadedOrThrewError;

        private Gauge()
        {
            Thread GaugeUpdateThread = new Thread(UpdateGauge)
            {
                IsBackground = true
            };
            GaugeUpdateThread.Start();
        }

        /// <summary>
        /// Gets the Gauge instance.
        /// </summary>
        /// <returns>Returns the gauge instance.</returns>
        public static Gauge GetInstance()
        {
            if (_obj == null)
                _obj = new Gauge();
            return _obj;
        }

        /// <summary>
        /// Constantly checks if the game has provided new telemetry data. If it has, an event is fired.
        /// </summary>
        private void UpdateGauge()
        {
            bool loaded = NR2003Binding.Setup();
            OnAddressSpaceLoadedOrThrewError(loaded);

            if (NR2003Binding.WaitForSimToRun())
            {
                OnNR2003LoadedOrThrewError(true);

                TimeSpan lap = new TimeSpan(0, 0, 0, 0, 0);
                LapCrossing lapCache = new LapCrossing()
                {
                    carIdx = new byte[] { 0, 0, 0, 0 },
                    lapNum = new byte[] { 0, 0, 0, 0 },
                    flags = new byte[] { 0, 0, 0, 0 },
                    crossedAt = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                };
                GaugeData gaugeCache = new GaugeData();
                while (true)
                {
                    NR2003Binding.RequestData();
                    IntPtr gd = NR2003Binding.GetGaugeData();
                    if (gd != IntPtr.Zero)
                    {

                        GaugeData gauge = (GaugeData)Marshal.PtrToStructure(gd, typeof(GaugeData));
                        if (gauge != gaugeCache)
                        {
                            OnGaugeUpdate(new GaugeUpdatedEventArgs { _gaugeData = gauge });
                        }

                    }

                    IntPtr ptrlap = NR2003Binding.GetLapCrossing();
                    if (ptrlap != IntPtr.Zero)
                    {

                        LapCrossing _lap = (LapCrossing)Marshal.PtrToStructure(ptrlap, typeof(LapCrossing));
                        if (_lap != lapCache)
                        {
                            double crossed = BitConverter.ToDouble(_lap.crossedAt, 0);
                            int seconds = Convert.ToInt32(Math.Truncate(crossed));
                            int milliseconds = Convert.ToInt32((crossed - seconds) * 1000);
                            TimeSpan crossedAt = new TimeSpan(0, 0, 0, seconds, milliseconds);
                            int carIdx = BitConverter.ToInt32(_lap.carIdx, 0);
                            if (carIdx == 0 && crossedAt > lap)
                            {
                                OnLapTimeUpdate(new LapTimeUpdatedEventArgs { lapTime = string.Format("{0:0.000}", (crossedAt - lap).TotalSeconds) });
                                lap = crossedAt;
                                lapCache = _lap;
                            }
                            else if (carIdx == 0)
                            {
                                lap = crossedAt;
                                lapCache = _lap;
                            }

                        }


                    }

                    //game only provides data at 36 hz
                    Thread.Sleep(20);
                }
            } else
            {
                OnNR2003LoadedOrThrewError(false);
            }
        }

        
        protected virtual void OnGaugeUpdate(GaugeUpdatedEventArgs e)
        {
            GaugeUpdated?.Invoke(this, e);
        }

        protected virtual void OnLapTimeUpdate(LapTimeUpdatedEventArgs e)
        {
            LapTimeUpdated?.Invoke(this, e);
        }
        protected virtual void OnNR2003LoadedOrThrewError(bool e)
        {
            NR2003LoadedOrThrewError?.Invoke(this, e);
        }

        protected virtual void OnAddressSpaceLoadedOrThrewError(bool e)
        {
            AddressSpaceLoadedOrThrewError?.Invoke(this, e);  
        }
    }

    public class GaugeUpdatedEventArgs : EventArgs
    {
       public GaugeData _gaugeData { get; set; }
    }

    public class LapTimeUpdatedEventArgs : EventArgs
    {
        public string lapTime { get; set; }
    }
}
