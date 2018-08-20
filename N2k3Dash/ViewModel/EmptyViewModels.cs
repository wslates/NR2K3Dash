using N2k3Dash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace N2k3Dash.ViewModel
{
    class Hybrid1ViewModel:AnalogViewModel
    {
        public Hybrid1ViewModel()
        {
            RunDashboard();
        }

        private void RunDashboard()
        {
            tach = Gauge.GetInstance();
            tach.GaugeUpdated += RefreshDash;

            //tachometer setup
            TachNeedleAngle = 0;
            RPMColor = Brushes.Black;
            RPM = 0;
            TachFacePath = "tach_face.png";

            //water temperature setup
            WaterTempColor = Brushes.White;
            WaterTemp = 0;

            //oil temperature setup
            OilTemp = 0;

            //fuel pressure setup
            FuelPressure = 0;
            FuelPressureColor = Brushes.White;

            //oil pressure setup
            OilPressure = 0;
            OilPressureColor = Brushes.White;

            //voltage
            Voltage = 0;

        }

        private void RefreshDash(object sender, GaugeUpdatedEventArgs e)
        {
            //RPM
            RPM = e._gaugeData.rpm;
            TachNeedleAngle = _RPM;
            if (GetBit(e._gaugeData.warnings, 0))
            {
                if (RPMColor.Equals(Brushes.Black))
                {
                    TachFacePath = "tach_face_red.png";
                    RPMColor = Brushes.White;
                }
            }
            else if (RPMColor.Equals(Brushes.White))
            {
                TachFacePath = "tach_face.png";
                RPMColor = Brushes.Black;
            }

            //Oil Temp
            OilTemp = e._gaugeData.oilTemp * 1.8f + 32.0f;

            //water temp
            WaterTemp = e._gaugeData.waterTemp * 1.8f + 32.0f;

            if (GetBit(e._gaugeData.warnings, 1))
            {
                if (WaterTempColor.Equals(Brushes.White))
                {
                    WaterTempColor = Brushes.Red;
                }
            }
            else if (OilPressureColor.Equals(Brushes.Red))
            {
                OilPressureColor = Brushes.White;
            }

            //oil pressure
            OilPressure = e._gaugeData.oilPress * 14.5038f;

            if (GetBit(e._gaugeData.warnings, 2))
            {
                if (OilPressureColor.Equals(Brushes.White))
                {
                    OilPressureColor = Brushes.Red;
                }

            }
            else if (OilPressureColor.Equals(Brushes.Red))
            {
                OilPressureColor = Brushes.White;
            }

            //voltage
            Voltage = e._gaugeData.voltage;

            //fuel pressure
            FuelPressure = e._gaugeData.fuelPress * 14.5038f;

            if (GetBit(e._gaugeData.warnings, 3))
            {
                if (FuelPressureColor.Equals(Brushes.White))
                {
                    FuelPressureColor = Brushes.Red;
                }

            }
            else if (FuelPressureColor.Equals(Brushes.Red))
            {
                FuelPressureColor = Brushes.White;
            }

        }
    }
}
