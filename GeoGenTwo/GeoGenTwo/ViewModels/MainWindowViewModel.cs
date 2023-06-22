using GeoGenTwo.ContentModule.Views;
using GeoGenTwo.Core;
using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.SettingsModule.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace GeoGenTwo.MainModule.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields/Members/Constants

        private IRegionManager _regionManager;
        private IContainerExtension _container;
        private IEventAggregator _eventAggregator;
        private IRegion _settingsRegion;
        private IRegion _contentRegion;
        private IRegion _interactionRegion;

        private CanvasView _canvasView;
        private SettingsView _settingsView;
        private AdvancedSettingsView _advSettingsView;
        private InteractionWindowView _interactionWindowView;

        #endregion

        #region Properties

        #endregion

        #region Constructor(s)

        public MainWindowViewModel(IContainerExtension container, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _container = container;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            Initialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// init method
        /// </summary>
        private void Initialize()
        {
            ResolveViewsAndAddToRegions();

            _eventAggregator.GetEvent<SettingsModeChangedEvent>().Subscribe(OnSettingsModeChangedEventReceived);
        }

        /// <summary>
        /// Helper method, if we need to add more regions
        /// </summary>
        private void ResolveViewsAndAddToRegions()
        {
            _canvasView = _container.Resolve<CanvasView>();
            _contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            _contentRegion.Add(_canvasView);

            _settingsView = _container.Resolve<SettingsView>();
            _advSettingsView = _container.Resolve<AdvancedSettingsView>();
            _settingsRegion = _regionManager.Regions[RegionNames.SettingsRegion];
            _settingsRegion.Add(_settingsView);
            _settingsRegion.Add(_advSettingsView);

            _interactionWindowView = _container.Resolve<InteractionWindowView>();
            _interactionRegion = _regionManager.Regions[RegionNames.InteractionRegion];
            _interactionRegion.Add(_interactionWindowView);
        }

        #region Callbacks

        /// <summary>
        /// callback for SettingsModeChangedEvent, requests navigation in the region manager
        /// </summary>
        /// <param name="msg">string pair of form {regionName, source} </param>
        private void OnSettingsModeChangedEventReceived(bool isChangedToAdvancedSettings)
        {
            string source = isChangedToAdvancedSettings ? "AdvancedSettingsView" : "SettingsView";
            _regionManager.RequestNavigate(RegionNames.SettingsRegion, source);
        }

        #endregion

        #endregion
    }
}
