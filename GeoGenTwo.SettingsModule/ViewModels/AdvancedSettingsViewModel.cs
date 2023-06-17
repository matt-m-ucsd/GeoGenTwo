﻿using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Linq;
using GeoGenTwo.Core;
using System;
using System.Collections.Generic;

namespace GeoGenTwo.SettingsModule.ViewModels
{
    public class AdvancedSettingsViewModel : RegionViewModelBase, IRegionMemberLifetime
    {
        #region Fields/Constants

        private IEventAggregator _eventAggregator;
        private ISettings _settings;
        private SolidColorBrushItem _lineBrush;
        private SolidColorBrushItem _backgroundBrush;
        private Resolution _portraitResolution;
        private Resolution _landscapeResolution;

        #endregion

        #region Properties

        public ObservableCollection<SolidColorBrushItem> BrushColorOptions { get; set; }
        public ObservableCollection<Resolution> PortraitResolutionOptions { get; set; }
        public ObservableCollection<Resolution> LandscapeResolutionOptions { get; set; }

        public ISettings Settings
        {
            get { return _settings; }
            set
            {
                SetProperty(ref _settings, value);
            }
        }

        public SolidColorBrushItem LineBrush
        {
            get { return _lineBrush; }
            set
            {
                SetProperty(ref _lineBrush, value);
                _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(Settings);
            }
        }

        public SolidColorBrushItem BackgroundBrush
        {
            get { return _backgroundBrush; }
            set
            {
                SetProperty(ref _backgroundBrush, value);
                _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(Settings);
            }
        }

        public Resolution PortraitResolution
        {
            get { return _portraitResolution; }
            set
            {
                SetProperty(ref _portraitResolution, value);
                _eventAggregator.GetEvent<SettingsChangedEvent>().Publish(Settings);
            }
        }

        public Resolution LandscapeResolution
        {
            get { return _landscapeResolution; }
            set 
            { 
                SetProperty(ref _landscapeResolution, value);
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
            PopulateColorOptionsList();
            SetDefaultColors();

            PopulateResolutionOptionsLists();
            SetDefaultResolutions();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        private void PopulateColorOptionsList()
        {
            BrushColorOptions = new ObservableCollection<SolidColorBrushItem>
            {
                new SolidColorBrushItem(Colors.Red, "Red"),
                new SolidColorBrushItem(Colors.Green, "Green"),
                new SolidColorBrushItem(Colors.Blue, "Blue"),
                new SolidColorBrushItem(Colors.Black, "Black"),
                new SolidColorBrushItem(Colors.White, "White")
            };
        }

        private void SetDefaultColors()
        {
            var lineBrushColor = ((SolidColorBrush)_settings.LineBrush).Color;
            var backgroundBrushColor = ((SolidColorBrush)_settings.BackgroundBrush).Color;
            LineBrush = BrushColorOptions.FirstOrDefault(item => item.ColorBrush.Color.Equals(lineBrushColor));
            BackgroundBrush = BrushColorOptions.FirstOrDefault(item => item.ColorBrush.Color.Equals(backgroundBrushColor));
        }

        private void PopulateResolutionOptionsLists()
        {
            LandscapeResolutionOptions = new ObservableCollection<Resolution>
            {
                new Resolution(1080, 1920)
            };

            PortraitResolutionOptions = new ObservableCollection<Resolution>
            {
                new Resolution(1920, 1080)
            };
        }
        private void SetDefaultResolutions()
        {
            PortraitResolution = PortraitResolutionOptions.FirstOrDefault(item => item.Equals(Settings.PortraitResolution));
            LandscapeResolution = LandscapeResolutionOptions.FirstOrDefault(item => item.Equals(Settings.LandscapeResolution));
        }

        #endregion

        #region Callbacks

        private void SwitchToBaseSettingsMode_Command()
        {
            _eventAggregator.GetEvent<SettingsModeChangedEvent>().Publish(false);
        }

        #endregion
    }
}
