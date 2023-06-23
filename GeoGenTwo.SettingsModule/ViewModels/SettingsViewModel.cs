using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace GeoGenTwo.SettingsModule.ViewModels
{
    public class SettingsViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region Fields/Constants

        private IEventAggregator _eventAggregator;
        private ISettings _settings;
        private int _numLines = SettingsConstants.DEFAULT_NUM_LINES;

        #endregion

        #region Properties

        public bool KeepAlive => true;

        public int NumLines
        {
            get { return _numLines; }
            set { 
                SetProperty(ref _numLines, value);
                Settings.NumLines = _numLines;
                _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(Settings);
            }
        }

        public ISettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }


        #endregion

        #region Commands

        public DelegateCommand SwitchSettingsModeCommand { get; private set; }

        #endregion

        #region Constructor

        public SettingsViewModel(IEventAggregator eventAggregator, ISettings settings) 
        {
            _eventAggregator = eventAggregator;
            _settings = settings;

            SwitchSettingsModeCommand = new DelegateCommand(SwitchSettingsMode_Command);

            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEvent);
        }

        #endregion

        #region Callbacks

        private void SwitchSettingsMode_Command()
        {
            _eventAggregator.GetEvent<SettingsModeChangedEvent>().Publish(true);
        }

        private void OnSettingsChangedEvent(ISettings settings)
        {
            Settings = settings;
        }

        #endregion

        #region Methods

        public override void Destroy()
        {
            base.Destroy();
            _eventAggregator.GetEvent<SettingsChangedEvent>().Unsubscribe(OnSettingsChangedEvent);
        }

        #endregion
    }
}
