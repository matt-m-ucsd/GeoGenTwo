using GeoGenTwo.SettingsModule.Interfaces;
using Prism.Mvvm;

namespace GeoGenTwo.SettingsModule
{
    public class Settings : BindableBase, ISettings
    {
        #region Fields

        private int _numLines;

        #endregion

        #region Properties

        public int NumLines
        {
            get { return _numLines; }
            set { SetProperty(ref _numLines, value); }
        }

        #endregion

    }
}
