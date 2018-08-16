using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using N2k3Dash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Documents;
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
        DashboardViewModel[] _dashLayouts = new DashboardViewModel[]
        {
            new DefaultViewModel(),
            new AnalogViewModel()
        };        private int _currentLayout = 0;
        private LowLevelKeyboardListener _listener;

        public RelayCommand OnCloseAction { get; private set; }
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
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
            OnCloseAction = new RelayCommand(OnClose);
            CurrentViewModel = _dashLayouts[0];
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += OnKeyPressed;
            _listener.HookKeyboard();
        }

        private void OnKeyPressed(object sender, KeyPressedArgs e)
        {
            Console.WriteLine(e.KeyPressed);
            if (e.KeyPressed.Equals(System.Windows.Input.Key.OemPeriod))
            {
                if (++_currentLayout > _dashLayouts.Length - 1)
                    _currentLayout = 0;
                CurrentViewModel = _dashLayouts[_currentLayout];
            } else if (e.KeyPressed.Equals(System.Windows.Input.Key.OemComma))
            {
                if (--_currentLayout < 0)
                    _currentLayout = _dashLayouts.Length - 1;
                CurrentViewModel = _dashLayouts[_currentLayout];
            }
            

        }

        private void OnClose()
        {
            _listener.UnHookKeyboard();
        }
    }

   
}