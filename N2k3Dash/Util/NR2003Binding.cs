using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace N2k3Dash.Model
{
 
    public class NR2003Binding
    {
        /// <summary>
        /// Sets up the telemtry session, allowing the program access to the telemetry memory space of NR2003.
        /// </summary>
        /// <returns>Returns "true" if setup succeeded, false if it failed.</returns>
        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Setup();

        /// <summary>
        /// Busy-waits until NR2003 has began.
        /// </summary>
        /// <returns>Returns true if NR2003 is running and telemetry program is now interacting with NR2003. False if an issue occurred.</returns>
        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool WaitForSimToRun();

        /// <summary>
        /// Determines whether the app can request data from the sim.
        /// (i.e. if there is data to consume, or if an issue occurred in the memory queue)
        /// </summary>
        /// <returns>Returns true if the app can request data successfully. False if it cannot.</returns>
        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        public static extern void RequestData();

        /// <summary>
        /// Gets the gauge telemetry from the address space.
        /// </summary>
        /// <returns>Returns an IntPtr pointing to a GaugeData struct.</returns>
        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        public static extern IntPtr GetGaugeData();

        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        public static extern IntPtr GetCurrentWeekend();

        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        public static extern IntPtr GetLapCrossing();

        [DllImport(@"C:\Users\wesle\Documents\Visual Studio 2017\Projects\NR2003\Release\NR2003.dll")]
        public static extern IntPtr GetDriverEntry();


    }
}
