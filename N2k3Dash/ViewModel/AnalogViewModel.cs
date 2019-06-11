using GalaSoft.MvvmLight;
using N2k3Dash.Model;
using N2k3Dash.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace N2k3Dash.ViewModel
{
    public class AnalogViewModel : DashboardViewModel
    {
        public AnalogViewModel()
        {
            RunDashboard();

        }

        private void RunDashboard()
        {
            tach = Gauge.GetInstance();
            tach.GaugeUpdated += RefreshDash;
            tach.LapTimeUpdated += LapTimeUpdated;

            //tachometer setup
            TachNeedleAngle = 0;
            RPMColor = Brushes.Black;
            RPM = 0;
            TachFacePath = TACH_FACE;

            //water temperature setup
            WaterTemperatureNeedleAngle = 0;
            WaterTempColor = Brushes.Black;
            WaterTemp = 0;
            WaterTemperatureFacePath = WATT_FACE;

            //oil temperature setup
            OilTemperatureNeedleAngle = 0;
            OilTemp = 0;
            
            //fuel pressure setup
            FuelPressureNeedleAngle = 0;
            FuelPressure = 0;
            FuelPressureColor = Brushes.Black;
            FuelPressureFacePath = FUELP_FACE;

            //oil pressure setup
            OilPressureNeedleAngle = 0;
            OilPressure = 0;
            OilPressureColor = Brushes.Black;
            OilPressureFacePath = OILP_FACE;

            //voltage
            VoltageNeedleAngle = 0;
            Voltage = 0;
            
        }

        private void RefreshDash(object sender, GaugeUpdatedEventArgs e)
        {
            //RPM
            RPM = e._gaugeData.rpm;
            TachNeedleAngle = e._gaugeData.rpm;
            if (GetBit(e._gaugeData.warnings, 0))
            {
                if (RPMColor.Equals(Brushes.Black))
                {
                    TachFacePath = TACH_FACE_RED;
                    RPMColor = Brushes.White;
                }             
            } else if (RPMColor.Equals(Brushes.White))
            {
                TachFacePath = TACH_FACE;
                RPMColor = Brushes.Black;
            }

            //Oil Temp
            OilTemp = UtilFunctions.CelsiusToFarenheit(e._gaugeData.oilTemp); 
            OilTemperatureNeedleAngle = UtilFunctions.CelsiusToFarenheit(e._gaugeData.oilTemp);

            //water temp
            WaterTemp = UtilFunctions.CelsiusToFarenheit(e._gaugeData.waterTemp);
            WaterTemperatureNeedleAngle = UtilFunctions.CelsiusToFarenheit(e._gaugeData.waterTemp);

            if (GetBit(e._gaugeData.warnings, 1))
            {
                if (WaterTempColor.Equals(Brushes.Black))
                {
                    WaterTemperatureFacePath = WATT_FACE_RED;
                    WaterTempColor = Brushes.White;
                }             
            }
            else if (OilPressureColor.Equals(Brushes.White))
            {
                OilPressureFacePath = WATT_FACE;
                OilPressureColor = Brushes.Black;
            }

            //oil pressure
            OilPressure = UtilFunctions.KPAToPSI(e._gaugeData.oilPress);
            OilPressureNeedleAngle = UtilFunctions.KPAToPSI(e._gaugeData.oilPress);

            if (GetBit(e._gaugeData.warnings, 2))
            {
                if (OilPressureColor.Equals(Brushes.Black))
                {
                    OilPressureFacePath = OILP_FACE_RED;
                    OilPressureColor = Brushes.White;
                }
                
            }
            else if (OilPressureColor.Equals(Brushes.White))
            {
                OilPressureFacePath = OILP_FACE;
                OilPressureColor = Brushes.Black;
            }

            //voltage
            Voltage = e._gaugeData.voltage;
            VoltageNeedleAngle = e._gaugeData.voltage;

            //fuel pressure
            FuelPressure = UtilFunctions.KPAToPSI(e._gaugeData.fuelPress);
            FuelPressureNeedleAngle = UtilFunctions.KPAToPSI(e._gaugeData.fuelPress);

            if (GetBit(e._gaugeData.warnings, 3))
            {
                if (FuelPressureColor.Equals(Brushes.Black))
                {
                    FuelPressureFacePath = FUELP_FACE_RED;
                    FuelPressureColor = Brushes.White;
                }
               
            }
            else if (FuelPressureColor.Equals(Brushes.White))
            {
                FuelPressureFacePath = FUELP_FACE;
                FuelPressureColor = Brushes.Black;
            }

        }


        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
