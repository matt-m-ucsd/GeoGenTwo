using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.SettingsModule.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoGenTwo.SettingsModule.ViewModels
{
    public class SettingsViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region Fields/Constants

        private IEventAggregator _eventAggregator;
        private ISettings _settings;

        #endregion

        #region Properties

        public bool KeepAlive
        {
            get { return true; }
        }

        public ISettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        public DelegateCommand GeneratePatternCommand { get; private set; }
        
        #endregion

        #region Constructor(s)

        public SettingsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, ISettings settings) 
            : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _settings = settings;

            GeneratePatternCommand = new DelegateCommand(GeneratePattern);
        }

        #endregion

        #region Callbacks

        private void GeneratePattern()
        {

        }

        #endregion
    }
}
