using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.SettingsModule.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoGenTwo.SettingsModule.ViewModels
{
    public class AdvancedSettingsViewModel : SettingsViewModel
    {
        #region Fields/Constants

        private IEventAggregator _eventAggregator;
        private ISettings _settings;

        #endregion

        #region Properties

        public DelegateCommand SwitchToBaseSettingsModeCommand { get; private set; }

        #endregion

        #region Constructor

        public AdvancedSettingsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISettings settings) 
            : base(regionManager, eventAggregator, settings)
        {
            _eventAggregator = eventAggregator;
            _settings = settings;

            SwitchToBaseSettingsModeCommand = new DelegateCommand(SwitchToBaseSettingsMode_Command);
        }

        #endregion

        #region Methods

        #region Callbacks

        #region Overrides

        private void SwitchToBaseSettingsMode_Command()
        {
            _eventAggregator.GetEvent<SettingsModeChangedEvent>().Publish(false);
        }

        #endregion

        #endregion

        #endregion
    }
}
