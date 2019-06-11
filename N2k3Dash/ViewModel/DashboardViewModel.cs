using GalaSoft.MvvmLight;
using N2k3Dash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace N2k3Dash.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        protected string _status;
        protected float _RPM = 0;
        protected float _waterTemp = 0;
        protected float _oilTemp = 0;
        protected float _oilPressure = 0;
        protected float _voltage = 0;
        protected float _fuelPressure = 0;
        private string _laptime = "0.000";
        protected bool _RPMWarning;
        protected bool _waterTempWarning;
        protected bool _fuelPressureWarning;
        protected bool _oilPressureWarning;
        protected Gauge tach;

        private Brush _RPMColor;
        private Brush _WaterTempColor;
        private Brush _FuelPressureColor;
        private Brush _OilPressureColor;
        private Brush _TachColor;

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

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
