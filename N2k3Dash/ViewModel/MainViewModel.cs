using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using N2k3Dash.Model;
using N2k3Dash.Navigation;
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
        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                return _loadedCommand
                    ?? (_loadedCommand = new RelayCommand(
                    () =>
                    {
                        ViewModelLocator.NavigateToStartPage();
                    }));
            }
        }
    }
}