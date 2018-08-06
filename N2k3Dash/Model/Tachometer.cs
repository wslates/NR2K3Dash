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
        public byte rpmWarning;

    }
    class Tachometer
    {
        private static Tachometer _obj;
        public event EventHandler<GaugeUpdatedEventArgs> GaugeUpdated;
        public event EventHandler<bool> NR2003LoadedOrThrewError;
        public event EventHandler<bool> AddressSpaceLoadedOrThrewError;

        private Tachometer()
        {
            Thread GaugeUpdateThread = new Thread(UpdateGauge)
            {
                IsBackground = true
            };
            GaugeUpdateThread.Start();
        }

        public static Tachometer GetInstance()
        {
            if (_obj == null)
                _obj = new Tachometer();
            return _obj;
        }


        private void UpdateGauge()
        {
            OnAddressSpaceLoadedOrThrewError(NR2003Binding.Setup());

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
                        IntPtr ptr = NR2003Binding.GetRPM();
                        args = new GaugeUpdatedEventArgs
                        {
                            _gaugeData = (GaugeData)Marshal.PtrToStructure(ptr, typeof(GaugeData))
                        };
                        OnGaugeUpdate(args);
                        //RefreshDash(ref data);
                    }
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
