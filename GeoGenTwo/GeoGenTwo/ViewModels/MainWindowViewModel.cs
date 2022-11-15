﻿using GeoGenTwo.ContentModule.Views;
using GeoGenTwo.Core;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace GeoGenTwo.MainModule.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Fields/Members/Constants

        private string _title = "Geo Gen (5 years later ver.)";
        private IRegionManager _regionManager;
        private IContainerExtension _container;
        private IRegion _interactionRegion;
        private IRegion _contentRegion;

        private CanvasView _canvasView;
        private SettingsView _settingsView;
        //private AdvancedSettingsView _advSettingsView;
        #endregion

        #region Properties

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Constructor(s)

        public MainWindowViewModel(IContainerExtension container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;

            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _canvasView = _container.Resolve<CanvasView>();
            _settingsView = _container.Resolve<SettingsView>();
            //_advSettingsView = _container.Resolve<AdvancedSettingsView>();

            _contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
            _contentRegion.Add(_canvasView);

            _interactionRegion = _regionManager.Regions[RegionNames.InteractionRegion];
            _interactionRegion.Add(_settingsView);
        }

        #endregion
    }
}
