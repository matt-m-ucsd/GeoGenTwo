using GeoGenTwo.Core.Interfaces;
using GeoGenTwo.Core.Mvvm;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class CanvasViewModel : RegionViewModelBase
    {
        #region Fields

        private IEventAggregator _eventAggregator;
        private ISettings _settings;

        #endregion

        #region Properties

        public ISettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        #endregion

        #region Constructor

        public CanvasViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<GeneratePatternEvent>().Subscribe(OnGeneratePatternEventReceived);
        }

        #endregion

        #region Methods

        #region Callbacks

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            // update current settings
        }


        private void OnGeneratePatternEventReceived()
        {
            // TODO: implementation
            CanvasString = "TEST CANVAS VM STRING #2";
        }

        #endregion

        #endregion
    }
}
