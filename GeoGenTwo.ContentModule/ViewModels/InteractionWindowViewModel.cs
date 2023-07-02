using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Shapes;

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
            _eventAggregator.GetEvent<ReturnLinesEvent>().Subscribe(OnReturnLinesEventReceived);
        }

        #endregion

        #region Overrides

        public override void Destroy()
        {
            base.Destroy();
            _eventAggregator.GetEvent<SettingsChangedEvent>().Unsubscribe(OnSettingsChangedEventReceived);
        }

        #endregion

        #region Callbacks

        private void GeneratePattern_Command()
        {
            _eventAggregator.GetEvent<GeneratePatternEvent>().Publish();
        }

        private void SaveToImage_Command(OutputOrientationType? orientation)
        {
            if (!orientation.HasValue) { return; }

            switch (orientation)
            {
                case OutputOrientationType.Portrait:
                    _eventAggregator.GetEvent<RequestLinesEvent>().Publish(OutputOrientationType.Portrait);
                    break;
                case OutputOrientationType.Landscape:
                    _eventAggregator.GetEvent<RequestLinesEvent>().Publish(OutputOrientationType.Landscape);
                    break;
            }
        }

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            Settings = settings;
        }

        private void OnReturnLinesEventReceived(List<Line> lines)
        {
            // TODO: save image logic
        }

        #endregion
    }
}
