using GalaSoft.MvvmLight;
using N2k3Dash.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace N2k3Dash.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Constant Variables
        protected const string TACH_FACE = "tach_face.png";
        protected const string TACH_FACE_RED = "tach_face_red.png";
        protected const string FUELP_FACE = "fuel_press_face.png";
        protected const string FUELP_FACE_RED = "fuel_press_face_red.png";
        protected const string OILP_FACE = "oil_press_face.png";
        protected const string OILP_FACE_RED = "oil_press_face_red.png";
        protected const string OILT_FACE = "oil_temp_face.png";
        protected const string OILT_FACE_RED = "oil_temp_face_red.png";
        protected const string WATT_FACE = "water_temp_face.png";
        protected const string WATT_FACE_RED = "water_temp_face_red.png";
        #endregion

        private string _status;
        private float _RPM = 0;
        private float _waterTemp = 0;
        private float _oilTemp = 0;
        private float _oilPressure = 0;
        private float _voltage = 0;
        private float _fuelPressure = 0;
        private string _laptime = "0.000";
        private bool _RPMWarning;
        private bool _waterTempWarning;
        private bool _fuelPressureWarning;
        private bool _oilPressureWarning;
        private float _tachPercentage;
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

        protected Gauge tach;

        private Brush _RPMColor;
        private Brush _WaterTempColor;
        private Brush _FuelPressureColor;
        private Brush _OilPressureColor;
        private Brush _TachColor;



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
        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                Set(ref _status, value);
            }
        }

        public float RPM
        {
            get
            {
                return (int)_RPM;
            }
            set
            {
                Set(ref _RPM, value);
            }
        }

        public bool RPMWarning
        {
            get
            {
                return _RPMWarning;
            }
            set
            {
                Set(ref _RPMWarning, value);
            }
        }

        public float WaterTemp
        {
            get
            {
                return (int)_waterTemp;
            }
            set
            {
                Set(ref _waterTemp, value);
            }
        }

        public bool WaterTempWarning
        {
            get
            {
                return _waterTempWarning;
            }

            set
            {
                Set(ref _waterTempWarning, value);
            }
        }
        public float OilTemp
        {
            get
            {
                return (int)_oilTemp;
            }
            set
            {
                Set(ref _oilTemp, value);
            }
        }

        public float OilPressure
        {
            get
            {
                return (int)_oilPressure;
            }
            set
            {
                Set(ref _oilPressure, value);
            }
        }

        public bool OilPressureWarning
        {
            get
            {
                return _oilPressureWarning;
            }
            set
            {
                Set(ref _oilPressureWarning, value);
            }
        }

        public float Voltage
        {
            get
            {
                return _voltage;
            }
            set
            {
                Set(ref _voltage, value);
            }
        }

        public float FuelPressure
        {
            get
            {
                return (int)_fuelPressure;
            }
            set
            {
                Set(ref _fuelPressure, value);
            }
        }

        public bool FuelPressureWarning
        {
            get
            {
                return _fuelPressureWarning;
            }
            set
            {
                Set(ref _fuelPressureWarning, value);
            }
        }

        public string LapTime
        {
            get
            {
                return _laptime;
            } 
            set
            {
                Set(ref _laptime, value);
            }
        }

        public Brush RPMColor
        {
            get
            {
                return _RPMColor;
            }
            set
            {
                Set(ref _RPMColor, value);
            }
        }

        public Brush WaterTempColor
        {
            get
            {
                return _WaterTempColor;
            }
            set
            {
                Set(ref _WaterTempColor, value);
            }
        }

        public Brush FuelPressureColor
        {
            get
            {
                return _FuelPressureColor;
            }
            set
            {
                Set(ref _FuelPressureColor, value);
            }
        }

        public Brush OilPressureColor
        {
            get
            {
                return _OilPressureColor;
            }
            set
            {
                Set(ref _OilPressureColor, value);
            }
        }

        public Brush TachColor
        {
            get
            {
                return _TachColor;
            }
            set
            {
                Set(ref _TachColor, value);
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

                Set(ref _tachNeedleAngle, 110 + ((value / 1000) * 28));
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
                }
                else
                {
                    Set(ref _voltageNeedleAngle, 224);
                }
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
                Set(ref _tachFacePath, "\\img\\" + value);
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
                Set(ref _waterTempFacePath, "\\img\\" + value);
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
                Set(ref _oilPressureFacePath, "\\img\\" + value);
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
                Set(ref _fuelPressureFacePath, "\\img\\" + value);
            }
        }

        public void AddressSpaceLoaded(object sender, bool e)
        {
            if (e)
            {
                Status = "NR2003 address space loaded!";
            }
            else
            {
                Status = "NR2003 address space failed to load.";
            }
        }

        public void NR2003Loaded(object sender, bool NR2003Loaded)
        {
            if (NR2003Loaded)
            {
                Status = "NR2003 Running!";
            }
            else
            {
                Status = "NR2003 failed to load.";
            }
        }


        protected bool GetBit(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }

        public void LapTimeUpdated(object sender, LapTimeUpdatedEventArgs e)
        {
            LapTime = e.lapTime;
        }

    }
}
