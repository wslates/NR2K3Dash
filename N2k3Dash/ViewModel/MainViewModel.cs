using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using N2k3Dash.Model;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Media;

namespace N2k3Dash.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        readonly static DefaultViewModel _digital1ViewModel = new DefaultViewModel();
        readonly static AnalogViewModel _analogViewModel = new AnalogViewModel();
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel != value)
                {
                    Set(ref _currentViewModel, value);
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            CurrentViewModel = _analogViewModel;


        }


        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }

   
}