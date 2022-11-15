using GeoGenTwo.Core;
using GeoGenTwo.MainModule.ViewModels;
using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace GeoGenTwo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IContainerExtension _container;
        private IRegionManager _regionManager;
        private MainWindowViewModel _viewModel;

        public MainWindow(IContainerExtension container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new MainWindowViewModel(_container, _regionManager);
        }
    }
}