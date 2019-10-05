/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:N2k3Dash.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using N2k3Dash.Model;
using N2k3Dash.Navigation;
using System;

namespace N2k3Dash.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        private static FrameNavigationService _navigationService;
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AnalogViewModel>();
            SimpleIoc.Default.Register<DefaultViewModel>();
            SetupNavigation();

        }

        private static void SetupNavigation()
        {
            _navigationService = new FrameNavigationService();
            _navigationService.Configure("Default", new Uri("../Views/Default.xaml", UriKind.Relative));
            _navigationService.Configure("Analog", new Uri("../Views/Analog.xaml", UriKind.Relative));
            _navigationService.Configure("HMS", new Uri("../Views/Hendrick.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DefaultViewModel DefaultViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DefaultViewModel>();
            }
        }

        public AnalogViewModel Analog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AnalogViewModel>();
            }
        }

        public static void NavigateToStartPage()
        {
            //Navigate to first page
            _navigationService.NavigateTo("Default");
        }
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            _navigationService.Unhook();
        }
    }
}