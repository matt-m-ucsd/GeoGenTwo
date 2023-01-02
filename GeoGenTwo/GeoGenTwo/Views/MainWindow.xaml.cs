using GeoGenTwo.Core;
using GeoGenTwo.MainModule.ViewModels;
using Prism.Events;
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
        #region Properties

        private IContainerExtension _container;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private MainWindowViewModel _viewModel;

        #endregion

        #region Constructor

        public MainWindow(IContainerExtension container, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            this.Loaded += MainWindow_Loaded;
        }

        #endregion

        #region Methods

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new MainWindowViewModel(_container, _regionManager, _eventAggregator);
        }

        #endregion
    }
}