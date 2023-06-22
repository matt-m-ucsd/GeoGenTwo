using GeoGenTwo.Core.Interfaces;
using GeoGenTwo.Core.Mvvm;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class CanvasViewModel : RegionViewModelBase
    {
        #region Fields

        private ObservableCollection<Line> _lines;
        private IEventAggregator _eventAggregator;
        private ISettings _settings;
        private Brush _canvasBrush;

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

        public Brush CanvasBrush
        {
            get { return _canvasBrush; }
            set { SetProperty(ref _canvasBrush, value); }
        }

        #endregion

        #region Constructor/Destructor

        public CanvasViewModel(IEventAggregator eventAggregator, ISettings settings) : base()
        {
            Lines = new ObservableCollection<Line>();
            Settings = settings;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<GeneratePatternEvent>().Subscribe(OnGeneratePatternEventReceived);
        }

        public override void Destroy()
        {
            _eventAggregator.GetEvent<SettingsChangedEvent>().Unsubscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<GeneratePatternEvent>().Unsubscribe(OnGeneratePatternEventReceived);
        }

        #endregion

        #region Callbacks

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            Settings = settings;
            CanvasBrush = settings.BackgroundBrush;
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
