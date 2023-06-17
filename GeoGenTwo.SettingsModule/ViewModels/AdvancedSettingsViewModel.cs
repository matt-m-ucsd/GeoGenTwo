using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Linq;

namespace GeoGenTwo.SettingsModule.ViewModels
{
    public class AdvancedSettingsViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region Fields/Constants

        private IEventAggregator _eventAggregator;
        private ISettings _settings;
        private SolidColorBrushItem _lineColor;
        private SolidColorBrushItem _backgroundColor;

        #endregion

        #region Properties

        public ISettings Settings
        {
            get { return _settings; }
            set { 
                SetProperty(ref _settings, value); 
            }
        }

        public ObservableCollection<SolidColorBrushItem> ColorOptions { get; set; }

        public SolidColorBrushItem LineColor
        {
            get { return _lineColor; }
            set { 
                SetProperty(ref _lineColor, value);
                _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(Settings);
            }
        }

        public SolidColorBrushItem BackgroundColor
        {
            get { return _backgroundColor; }
            set { 
                SetProperty(ref _backgroundColor, value);
                _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(Settings);
            }
        }

        public bool KeepAlive => true;

        #endregion

        #region Commands

        public DelegateCommand SwitchToBaseSettingsModeCommand { get; private set; }


        #endregion

        #region Constructor

        public AdvancedSettingsViewModel(IEventAggregator eventAggregator, ISettings settings)
        {
            _eventAggregator = eventAggregator;
            _settings = settings;

            SwitchToBaseSettingsModeCommand = new DelegateCommand(SwitchToBaseSettingsMode_Command);
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ColorOptions = new ObservableCollection<SolidColorBrushItem>
            {
                new SolidColorBrushItem(new SolidColorBrush(Colors.Red), "Red"),
                new SolidColorBrushItem(new SolidColorBrush(Colors.Green), "Green"),
                new SolidColorBrushItem(new SolidColorBrush(Colors.Blue), "Blue"),
                new SolidColorBrushItem(new SolidColorBrush(Colors.Black), "Black"),
                new SolidColorBrushItem(new SolidColorBrush(Colors.White), "White")
            };

            LineColor = ColorOptions.FirstOrDefault(item => item.ColorBrush.Color == _settings.LineColor);
            BackgroundColor = ColorOptions.FirstOrDefault(item => item.ColorBrush.Color == _settings.BackgroundColor);
        }

        #endregion

        #region Callbacks

        #region Overrides

        private void SwitchToBaseSettingsMode_Command()
        {
            _eventAggregator.GetEvent<SettingsModeChangedEvent>().Publish(false);
        }

        #endregion

        #endregion
    }
}
