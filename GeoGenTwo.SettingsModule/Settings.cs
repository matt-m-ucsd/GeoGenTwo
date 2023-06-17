using GeoGenTwo.Core.Interfaces;
using Prism.Mvvm;
using System.Windows.Media;

namespace GeoGenTwo.SettingsModule
{
    public class Settings : BindableBase, ISettings
    {
        #region Fields

        private int _numLines;
        private Color _lineColor;
        private Color _backgroundColor;

        #endregion

        #region Properties

        public int NumLines
        {
            get { return _numLines; }
            set { SetProperty(ref _numLines, value); }
        }

        public Color LineColor
        {
            get { return _lineColor;  } 
            set { SetProperty(ref _lineColor, value); }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }

        #endregion

        #region Constructors

        public Settings()
        {
            NumLines = SettingsConstants.DEFAULT_NUM_LINES;
            LineColor = SettingsConstants.DEFAULT_LINE_COLOR;
            BackgroundColor = SettingsConstants.DEFAULT_BACKGROUND_COLOR;
        }

        #endregion

    }
}
