using GalaSoft.MvvmLight;
using N2k3Dash.Model;
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
        #region FIELDS
        private double _tachNeedleAngle;
        private double _waterTempNeedleAngle;
        private double _voltageNeedleAngle;
        private double _oilPressureNeedleAngle;
        private double _oilTemperatureNeedleAngle;
        private double _fuelPressureNeedleAngle;
        private string _tachFacePath;
        private string _waterTempFacePath;
        private string _fuelPressureFacePath;
        private string _oilPressureFacePath;
        #endregion

        #region PROPERTIES
        public static string AssemblyDirectory
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public double TachNeedleAngle
        {
            get
            {
                return _tachNeedleAngle;
            }

            set
            {

                Set(ref _tachNeedleAngle, 110 + ((value / 1000) * 28) );
            }
        }

        public double WaterTemperatureNeedleAngle
        {
            get
            {
                return _waterTempNeedleAngle;
            }
            set
            {
                if (value > 300)
                {
                    Set(ref _waterTempNeedleAngle, 496);
                }
                else if (value > 100)
                {
                    Set(ref _waterTempNeedleAngle, 224f + ((value - 100f) / 25f) * 34f);
                }
                else
                {
                    Set(ref _waterTempNeedleAngle, 224);
                }
            }
        }

        public double OilTemperatureNeedleAngle
        {
            get
            {
                return _oilTemperatureNeedleAngle;
            }
            set
            {
                if (value > 300)
                {
                    Set(ref _oilTemperatureNeedleAngle, 496);
                }
                else if (value > 100)
                {
                    Set(ref _oilTemperatureNeedleAngle, 224f + ((value - 100f) / 25f) * 34f);
                }
                else
                {
                    Set(ref _oilTemperatureNeedleAngle, 224);
                }
            }
        }

        public double FuelPressureNeedleAngle
        {
            get
            {
                return _fuelPressureNeedleAngle;
            }
            set
            {
                Set(ref _fuelPressureNeedleAngle, 224 + (value / 4) * 34);
            }
        }

        public double OilPressureNeedleAngle
        {
            get
            {
                return _oilPressureNeedleAngle;
            }
            set
            {
                Set(ref _oilPressureNeedleAngle, 224 + (value / 12.50) * 34);
            }
        }

        public double VoltageNeedleAngle
        {
            get
            {
                return _voltageNeedleAngle;
            }
            set
            {
                if (value > 18)
                {
                    Set(ref _voltageNeedleAngle, 496);
                }
                else if (value > 10)
                {
                    Set(ref _voltageNeedleAngle, 224 + (value - 10) * 34);
                } else
                {
                    Set(ref _voltageNeedleAngle, 224);
                }
            }
        }
        public static string OilTemperatureFacePath
        {
            get
            {
                return AssemblyDirectory + "\\Images\\oil_temp_face.png";
            }
        }
        public static string VoltageFacePath
        {
            get
            {
                return AssemblyDirectory + "\\Images\\volts_face.png";
            }
        }

        public static string NeedlePath
        {
            get
            {
                return AssemblyDirectory + "\\Images\\needle.png";
            }
        }

        public string TachFacePath
        {
            get
            {
                return _tachFacePath;
            }

            set
            {
                Set(ref _tachFacePath, AssemblyDirectory + "\\Images\\" + value);
            }
        }

        public string WaterTemperatureFacePath
        {
            get
            {
                return _waterTempFacePath;
            } 
            set
            {
                Set(ref _waterTempFacePath, AssemblyDirectory + "\\Images\\" + value);
            }
        }

        public string OilPressureFacePath
        {
            get
            {
                return _oilPressureFacePath;
            } 
            set
            {
                Set(ref _oilPressureFacePath, AssemblyDirectory + "\\Images\\" + value);
            }
        }

        public string FuelPressureFacePath
        {
            get
            {
                return _fuelPressureFacePath;
            }
            set
            {
                Set(ref _fuelPressureFacePath, AssemblyDirectory + "\\Images\\" + value);
            }
        }

        
        #endregion

        public AnalogViewModel()
        {
            RunDashboard();

        }

        private void RunDashboard()
        {
            Tachometer tach = Tachometer.GetInstance();
            tach.GaugeUpdated += RefreshDash;

            //tachometer setup
            TachNeedleAngle = 0;
            RPMColor = Brushes.Black;
            RPM = "0";
            TachFacePath = "tach_face.png";

            //water temperature setup
            WaterTemperatureNeedleAngle = 0;
            WaterTempColor = Brushes.Black;
            WaterTemp = "0";
            WaterTemperatureFacePath = "water_temp_face.png";

            //oil temperature setup
            OilTemperatureNeedleAngle = 0;
            OilTemp = "0";
            
            //fuel pressure setup
            FuelPressureNeedleAngle = 0;
            FuelPressure = "0";
            FuelPressureColor = Brushes.Black;
            FuelPressureFacePath = "fuel_press_face.png";

            //oil pressure setup
            OilPressureNeedleAngle = 0;
            OilPressure = "0";
            OilPressureColor = Brushes.Black;
            OilPressureFacePath = "oil_press_face.png";

            //voltage
            VoltageNeedleAngle = 0;
            Voltage = "0";
            
        }

        private void RefreshDash(object sender, GaugeUpdatedEventArgs e)
        {
            //RPM
            RPM = Math.Round(e._gaugeData.rpm).ToString();
            TachNeedleAngle = e._gaugeData.rpm;
            if (GetBit(e._gaugeData.rpmWarning, 0))
            {
                if (RPMColor.Equals(Brushes.Black))
                {
                    TachFacePath = "tach_face_red.png";
                    RPMColor = Brushes.White;
                }             
            } else if (RPMColor.Equals(Brushes.White))
            {
                TachFacePath = "tach_face.png";
                RPMColor = Brushes.Black;
            }

            //Oil Temp
            OilTemp = Math.Round(e._gaugeData.oilTemp * 1.8f + 32.0f).ToString();
            OilTemperatureNeedleAngle = e._gaugeData.oilTemp * 1.8f + 32.0f;

            //water temp
            WaterTemp = Math.Round(e._gaugeData.waterTemp * 1.8f + 32.0f).ToString();
            WaterTemperatureNeedleAngle = e._gaugeData.waterTemp * 1.8f + 32.0f;

            if (GetBit(e._gaugeData.rpmWarning, 1))
            {
                if (WaterTempColor.Equals(Brushes.Black))
                {
                    WaterTemperatureFacePath = "water_temp_face_red.png";
                    WaterTempColor = Brushes.White;
                }             
            }
            else if (OilPressureColor.Equals(Brushes.White))
            {
                OilPressureFacePath = "water_temp_face.png";
                OilPressureColor = Brushes.Black;
            }

            //oil pressure
            OilPressure = Math.Round(e._gaugeData.oilPress * 14.5038).ToString();
            OilPressureNeedleAngle = e._gaugeData.oilPress * 14.5038;

            if (GetBit(e._gaugeData.rpmWarning, 2))
            {
                if (OilPressureColor.Equals(Brushes.Black))
                {
                    OilPressureFacePath = "oil_press_face_red.png";
                    OilPressureColor = Brushes.White;
                }
                
            }
            else if (OilPressureColor.Equals(Brushes.White))
            {
                OilPressureFacePath = "oil_press_face.png";
                OilPressureColor = Brushes.Black;
            }

            //voltage
            Voltage = e._gaugeData.voltage.ToString();
            VoltageNeedleAngle = e._gaugeData.voltage;

            //fuel pressure
            FuelPressure = Math.Round(e._gaugeData.fuelPress * 14.5038).ToString();
            FuelPressureNeedleAngle = e._gaugeData.fuelPress * 14.5038;

            if (GetBit(e._gaugeData.rpmWarning, 3))
            {
                if (FuelPressureColor.Equals(Brushes.Black))
                {
                    FuelPressureFacePath = "fuel_press_face_red.png";
                    FuelPressureColor = Brushes.White;
                }
               
            }
            else if (FuelPressureColor.Equals(Brushes.White))
            {
                FuelPressureFacePath = "fuel_press_face.png";
                FuelPressureColor = Brushes.Black;
            }

        }

        private void TestGauge()
        {
            for (int i = 0; i < 12; i += 1)
            {
                TachNeedleAngle = 110 + (28 * i);
                Thread.Sleep(1000);
            }
        }
    }
}
