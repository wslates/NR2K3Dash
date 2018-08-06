using System.Windows;
using N2k3Dash.ViewModel;

namespace N2k3Dash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void Tach_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}