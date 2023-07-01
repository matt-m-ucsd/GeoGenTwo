using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using GeoGenTwo.Core;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class InteractionWindowViewModel : RegionViewModelBase
    {
        #region Commands

        public DelegateCommand GeneratePatternCommand { get; private set; }
        public DelegateCommand<OutputOrientationType?> SaveToImageCommand { get; private set; }

        #endregion

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

        public InteractionWindowViewModel(IEventAggregator eventAggregator, ISettings settings) : base()
        {
            _eventAggregator = eventAggregator;
            Settings = settings;

            GeneratePatternCommand = new DelegateCommand(GeneratePattern_Command);
            SaveToImageCommand = new DelegateCommand<OutputOrientationType?>(SaveToImage_Command);

            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEventReceived);
        }

        #endregion

        #region Methods

        public override void Destroy()
        {
            base.Destroy();
            _eventAggregator.GetEvent<SettingsChangedEvent>().Unsubscribe(OnSettingsChangedEventReceived);
        }

        #region Callbacks

        private void GeneratePattern_Command()
        {
            // publish event for CVM
            _eventAggregator.GetEvent<GeneratePatternEvent>().Publish();
        }

        private void SaveToImage_Command(OutputOrientationType? orientation)
        {
            if (!orientation.HasValue) { return; }

            switch (orientation)
            {
                case OutputOrientationType.Portrait:
                    SaveImage(Settings.PortraitResolution);
                    break;
                case OutputOrientationType.Landscape:
                    SaveImage(Settings.LandscapeResolution);
                    break;
            }
        }

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            Settings = settings;
        }

        #endregion

        private void SaveImage(Resolution resolution)
        {
            
        }

        #endregion
    }
}
