using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace N2k3Dash.Model
{
 
    class NR2003Binding
    {
        [DllImport(@"NR2003.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Setup();

        [DllImport(@"NR2003.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool WaitForSimToRun();

        [DllImport(@"NR2003.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool CanRequestData();

        [DllImport(@"NR2003.dll")]
        public static extern IntPtr GetRPM();

        
    }
}
