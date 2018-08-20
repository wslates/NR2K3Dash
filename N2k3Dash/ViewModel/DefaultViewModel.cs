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
        
        public DefaultViewModel()
        {
            RunDashboard();
        }
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
            tach = Gauge.GetInstance();
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

            //WarningsThread.Start();

        }



        private void RefreshDash(object sender, GaugeUpdatedEventArgs e)
        {
            
            //RPM
            RPM = e._gaugeData.rpm;
            _RPMWarning = GetBit(e._gaugeData.warnings, 0);
            RPMColor = (GetBit(e._gaugeData.warnings, 0)) ? Brushes.Red : Brushes.White;
            TachColor = (GetBit(e._gaugeData.warnings, 0)) ? Brushes.Red : Brushes.Yellow;
            TachPercentage = (e._gaugeData.rpm / 10000) * 100;

            //Oil Temp
            OilTemp = e._gaugeData.oilTemp * 1.8f + 32.0f;

            //Water Temp
            WaterTemp = e._gaugeData.waterTemp * 1.8f + 32.0f;
            _waterTempWarning = GetBit(e._gaugeData.warnings, 1);
            //WaterTempColor = (GetBit(e._gaugeData.rpmWarning, 1)) ? Brushes.Red : Brushes.White;

            //Oil Pressure
            OilPressure = e._gaugeData.oilPress * 14.5038f;
            _oilPressureWarning = GetBit(e._gaugeData.warnings, 2);
            //OilPressureColor = (GetBit(e._gaugeData.rpmWarning, 2)) ? Brushes.Red : Brushes.White;

            //Voltage
            Voltage = e._gaugeData.voltage;

            //Fuel Pressure
            FuelPressure = e._gaugeData.fuelPress * 14.5038f;
            _fuelPressureWarning = GetBit(e._gaugeData.warnings, 3);
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
        public override void Cleanup()
        {
            tach.GaugeUpdated -= RefreshDash;
            tach = null;
            base.Cleanup();
        }
    }
}
