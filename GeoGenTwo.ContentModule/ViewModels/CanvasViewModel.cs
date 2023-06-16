﻿using GeoGenTwo.Core.Interfaces;
using GeoGenTwo.Core.Mvvm;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows.Shapes;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class CanvasViewModel : RegionViewModelBase
    {
        #region Fields

        private ObservableCollection<Line> _lines;
        private IEventAggregator _eventAggregator;
        private ISettings _settings;

        #endregion

        #region Properties

        public ISettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        public ObservableCollection<Line> Lines
        {
            get { return _lines; }
            set { SetProperty(ref _lines, value); }
        }

        #endregion

        #region Constructor

        public CanvasViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager)
        {
            Lines = new ObservableCollection<Line>();

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<GeneratePatternEvent>().Subscribe(OnGeneratePatternEventReceived);
        }

        #endregion

        #region Callbacks

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            Settings = settings;
        }


        private void OnGeneratePatternEventReceived()
        {
            GenerateLines();
        }

        #endregion

        #region Methods

        private void GenerateLines()
        {
            // TODO: line generation
        }

        #endregion
    }
}
