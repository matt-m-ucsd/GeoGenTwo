using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class InteractionWindowViewModel : RegionViewModelBase
    {
        #region Fields

        private IEventAggregator _eventAggregator;

        #endregion

        #region Commands

        public DelegateCommand GeneratePatternCommand { get; private set; }

        #endregion

        #region Constructor

        public InteractionWindowViewModel(IEventAggregator eventAggregator) : base()
        {
            _eventAggregator = eventAggregator;

            GeneratePatternCommand = new DelegateCommand(GeneratePattern_Command);
        }

        #endregion

        #region Methods

        #region Callbacks

        private void GeneratePattern_Command()
        {
            // publish event for CVM
            _eventAggregator.GetEvent<GeneratePatternEvent>().Publish();
        }

        #endregion

        #endregion
    }
}
