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

        private void RunDashboard()
        {
            tach = Gauge.GetInstance();
            tach.AddressSpaceLoadedOrThrewError += AddressSpaceLoaded;
            tach.NR2003LoadedOrThrewError += NR2003Loaded;
            tach.GaugeUpdated += RefreshDash;
            tach.LapTimeUpdated += LapTimeUpdated;
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
            RPMWarning = GetBit(e._gaugeData.warnings, 0);
            RPMColor = (GetBit(e._gaugeData.warnings, 0)) ? Brushes.Red : Brushes.White;
            TachColor = (GetBit(e._gaugeData.warnings, 0)) ? Brushes.Red : Brushes.Yellow;
            TachPercentage = (e._gaugeData.rpm / 10000) * 100;

            //Oil Temp
            OilTemp = e._gaugeData.oilTemp * 1.8f + 32.0f;

            //Water Temp
            WaterTemp = e._gaugeData.waterTemp * 1.8f + 32.0f;
            WaterTempWarning= GetBit(e._gaugeData.warnings, 1);
            //WaterTempColor = (GetBit(e._gaugeData.rpmWarning, 1)) ? Brushes.Red : Brushes.White;

            //Oil Pressure
            OilPressure = e._gaugeData.oilPress * 14.5038f;
            OilPressureWarning= GetBit(e._gaugeData.warnings, 2);
            //OilPressureColor = (GetBit(e._gaugeData.rpmWarning, 2)) ? Brushes.Red : Brushes.White;

            //Voltage
            Voltage = e._gaugeData.voltage;

            //Fuel Pressure
            FuelPressure = e._gaugeData.fuelPress * 14.5038f;
            FuelPressureWarning = GetBit(e._gaugeData.warnings, 3);
            //FuelPressureColor = (GetBit(e._gaugeData.rpmWarning, 3)) ? Brushes.Red : Brushes.White;

        }

        private void CheckAndDisplayWarnings()
        { 

            while(true)
            {
                if (RPMWarning)
                {
                    RPMColor = (RPMColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (RPMColor.Equals(Brushes.Red))
                {
                    RPMColor = Brushes.White;
                }

                if(WaterTempWarning)
                {
                    WaterTempColor = (WaterTempColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (WaterTempColor.Equals(Brushes.Red))
                {
                    WaterTempColor = Brushes.White;
                }
                
                if (OilPressureWarning)
                {
                    OilPressureColor = (OilPressureColor.Equals(Brushes.Red)) ? Brushes.White : Brushes.Red;
                } else if (OilPressureColor.Equals(Brushes.Red))
                {
                    OilPressureColor = Brushes.White;
                }

                if (FuelPressureWarning)
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
