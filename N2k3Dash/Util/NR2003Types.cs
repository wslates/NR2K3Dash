using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace N2k3Dash.Util
{
    public class NR2003Types
    {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct GaugeData
        {
            public float rpm;
            public float waterTemp;
            public float fuelPress;
            public float oilTemp;
            public float oilPress;
            public float voltage;
            public byte warnings;

            public static bool operator ==(GaugeData x, GaugeData y)
            {
                return (x.rpm == y.rpm &&
                        x.waterTemp == y.waterTemp &&
                        x.fuelPress == y.fuelPress &&
                        x.oilTemp == y.oilTemp &&
                        x.oilPress == y.oilPress &&
                        x.voltage == y.voltage &&
                        x.warnings == y.warnings);

            }

            public static bool operator !=(GaugeData x, GaugeData y)
            {
                return (x.rpm != y.rpm ||
                        x.waterTemp != y.waterTemp ||
                        x.fuelPress != y.fuelPress ||
                        x.oilTemp != y.oilTemp ||
                        x.oilPress != y.oilPress ||
                        x.voltage != y.voltage ||
                        x.warnings != y.warnings);

            }

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CurrentWeekend
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool atTrack;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string track;
            public float trackLength;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sessions;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string options;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct LapCrossing
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] carIdx;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] lapNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] flags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] crossedAt;

            public static bool operator ==(LapCrossing x, LapCrossing y)
            {
                return (BitConverter.ToInt32(x.carIdx, 0) == BitConverter.ToInt32(y.carIdx, 0) &&
                        BitConverter.ToInt32(x.lapNum, 0) == BitConverter.ToInt32(y.lapNum, 0) &&
                        BitConverter.ToInt32(x.flags, 0) == BitConverter.ToInt32(y.flags, 0) &&
                        BitConverter.ToDouble(x.crossedAt, 0) == BitConverter.ToDouble(y.crossedAt, 0));
            }

            public static bool operator !=(LapCrossing x, LapCrossing y)
            {
                return (BitConverter.ToInt32(x.carIdx, 0) != BitConverter.ToInt32(y.carIdx, 0) ||
                        BitConverter.ToInt32(x.lapNum, 0) != BitConverter.ToInt32(y.lapNum, 0) ||
                        BitConverter.ToInt32(x.flags, 0) != BitConverter.ToInt32(y.flags, 0) ||
                        BitConverter.ToDouble(x.crossedAt, 0) != BitConverter.ToDouble(y.crossedAt, 0));
            }
        }
    }
}
