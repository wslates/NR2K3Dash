using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N2k3Dash.Util
{
    public class UtilFunctions
    {
        public static float CelsiusToFarenheit(float celsius)
        {
            return celsius * 1.8f + 32.0f;
        }

        public static float KPAToPSI (float KPA)
        {
            return KPA * 14.5038f;
        }
    }
}
