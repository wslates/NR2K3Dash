using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace N2k3Dash.Model
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct GaugeData
    {
        public float rpm;
        public float waterTemp;
        public float fuelPress;
        public float oilTemp;
        public float oilPress;
        public float voltage;
        public byte warnings;

    }

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

            //Status = "Waiting for NR2003 to start...";
            if (NR2003Binding.WaitForSimToRun())
            {
                OnNR2003LoadedOrThrewError(true);
                //Status = "NR2003 has started!";
                GaugeUpdatedEventArgs args;
                while (true)
                {
                    if (NR2003Binding.CanRequestData())
                    {
                        IntPtr ptr = NR2003Binding.GetGaugeData();
                        args = new GaugeUpdatedEventArgs
                        {
                            _gaugeData = (GaugeData)Marshal.PtrToStructure(ptr, typeof(GaugeData))
                        };
                        OnGaugeUpdate(args);
                        //RefreshDash(ref data);
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
}
