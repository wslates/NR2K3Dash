using N2k3Dash.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace N2k3Dash.ViewModel
{
    public class DefaultViewModel : DashboardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public DefaultViewModel()
        {
            RunDashboard();
        }
        //public string Name => throw new NotImplementedException();

       
        private float _tachPercentage;
        
        public float TachPercentage
        {
            get
            {
                return _tachPercentage;
            }
            set
            {
                Set(ref _tachPercentage, value);
            }
        }
      

        private void RunDashboard()
        {
            Tachometer tach = Tachometer.GetInstance();
            tach.AddressSpaceLoadedOrThrewError += AddressSpaceLoaded;
            tach.NR2003LoadedOrThrewError += NR2003Loaded;
            tach.GaugeUpdated += RefreshDash;
            RPMColor = Brushes.White;
            WaterTempColor = Brushes.White;
            OilPressureColor = Brushes.White;
            FuelPressureColor = Brushes.White;

            Thread WarningsThread = new Thread(CheckAndDisplayWarnings)
            {
                IsBackground = true
            };

            WarningsThread.Start();
            /*
            NR2003Binding.Setup();
            Status = "Waiting for NR2003 to start...";
            if (NR2003Binding.WaitForSimToRun())
            {
                Status = "NR2003 has started!";
                GaugeData data;
                while (true)
                {
                    if (NR2003Binding.CanRequestData())
                    {
                        IntPtr ptr = NR2003Binding.GetRPM();
                        data = (GaugeData)Marshal.PtrToStructure(ptr, typeof(GaugeData));
                        RefreshDash(ref data);
                    }
                    Thread.Sleep(20);
                }
            }
           */
        }



        private void RefreshDash(object sender, GaugeUpdatedEventArgs e)
        {
            
            //RPM
            RPM = Math.Round(e._gaugeData.rpm).ToString();
            _RPMWarning = GetBit(e._gaugeData.rpmWarning, 0);
            RPMColor = (GetBit(e._gaugeData.rpmWarning, 0)) ? Brushes.Red : Brushes.White;
            TachColor = (GetBit(e._gaugeData.rpmWarning, 0)) ? Brushes.Red : Brushes.Yellow;
            TachPercentage = (e._gaugeData.rpm / 10000) * 100;

            //Oil Temp
            OilTemp = Math.Round(e._gaugeData.oilTemp * 1.8f + 32.0f).ToString();

            //Water Temp
            WaterTemp = Math.Round(e._gaugeData.waterTemp * 1.8f + 32.0f).ToString();
            _waterTempWarning = GetBit(e._gaugeData.rpmWarning, 1);
            //WaterTempColor = (GetBit(e._gaugeData.rpmWarning, 1)) ? Brushes.Red : Brushes.White;

            //Oil Pressure
            OilPressure = Math.Round(e._gaugeData.oilPress * 14.5038).ToString();
            _oilPressureWarning = GetBit(e._gaugeData.rpmWarning, 2);
            //OilPressureColor = (GetBit(e._gaugeData.rpmWarning, 2)) ? Brushes.Red : Brushes.White;

            //Voltage
            Voltage = e._gaugeData.voltage.ToString();

            //Fuel Pressure
            FuelPressure = Math.Round(e._gaugeData.fuelPress * 14.5038).ToString();
            _fuelPressureWarning = GetBit(e._gaugeData.rpmWarning, 3);
            //FuelPressureColor = (GetBit(e._gaugeData.rpmWarning, 3)) ? Brushes.Red : Brushes.White;

        }

        private void CheckAndDisplayWarnings()
        { 

            while(true)
            {
                if (_RPMWarning)
                {
                    RPMColor = (RPMColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (RPMColor.Equals(Brushes.Red))
                {
                    RPMColor = Brushes.White;
                }

                if(_waterTempWarning)
                {
                    WaterTempColor = (WaterTempColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (WaterTempColor.Equals(Brushes.Red))
                {
                    WaterTempColor = Brushes.White;
                }
                
                if (_oilPressureWarning)
                {
                    OilPressureColor = (OilPressureColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (OilPressureColor.Equals(Brushes.Red))
                {
                    OilPressureColor = Brushes.White;
                }

                if (_fuelPressureWarning)
                {
                    FuelPressureColor = (FuelPressureColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (FuelPressureColor.Equals(Brushes.Red))
                {
                    FuelPressureColor = Brushes.White;
                }

                Thread.Sleep(500);
            }
        }
    }
}
