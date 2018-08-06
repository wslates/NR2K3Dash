using GalaSoft.MvvmLight;
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
        private string _status;
        private string _RPM;
        private string _waterTemp;
        private string _oilTemp;
        private string _oilPressure;
        private string _voltage;
        private string _fuelPressure;
        protected bool _RPMWarning;
        protected bool _waterTempWarning;
        protected bool _fuelPressureWarning;
        protected bool _oilPressureWarning;

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

        public string RPM
        {
            get
            {
                return _RPM;
            }
            set
            {
                Set(ref _RPM, value);
            }
        }

        public string WaterTemp
        {
            get
            {
                return _waterTemp;
            }
            set
            {
                Set(ref _waterTemp, value);
            }
        }

        public string OilTemp
        {
            get
            {
                return _oilTemp;
            }
            set
            {
                Set(ref _oilTemp, value);
            }
        }

        public string OilPressure
        {
            get
            {
                return _oilPressure;
            }
            set
            {
                Set(ref _oilPressure, value);
            }
        }

        public string Voltage
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

        public string FuelPressure
        {
            get
            {
                return _fuelPressure;
            }
            set
            {
                Set(ref _fuelPressure, value);
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
                Status = "Error";
            }
        }

        protected bool GetBit(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }
    }
}
