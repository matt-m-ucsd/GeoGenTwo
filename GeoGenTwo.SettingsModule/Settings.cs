using GeoGenTwo.Core;
using GeoGenTwo.Core.Interfaces;
using Prism.Mvvm;
using System.IO;
using System.Windows.Media;

namespace GeoGenTwo.SettingsModule
{
    public class Settings : BindableBase, ISettings
    {
        #region Fields

        private int _numLines;
        private Brush _lineColor;
        private Brush _backgroundColor;
        private Resolution _portraitResolution;
        private Resolution _landscapeResolution;
        private string _saveDirectoryFilePath;

        #endregion

        #region Properties

        public int NumLines
        {
            get { return _numLines; }
            set { SetProperty(ref _numLines, value); }
        }

        public Brush LineBrush
        {
            get { return _lineColor;  } 
            set { SetProperty(ref _lineColor, value); }
        }

        public Brush BackgroundBrush
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }

        public Resolution PortraitResolution
        {
            get { return _portraitResolution; }
            set { SetProperty(ref _portraitResolution, value); }
        }

        public Resolution LandscapeResolution
        {
            get { return _landscapeResolution; }
            set { SetProperty(ref _landscapeResolution, value); }
        }

        public string SaveDirectoryFilePath
        {
            get { return _saveDirectoryFilePath; }
            set { SetProperty(ref _saveDirectoryFilePath, value); }
        }

        #endregion

        #region Constructors

        public Settings()
        {
            NumLines = SettingsConstants.DEFAULT_NUM_LINES;
            LineBrush = SettingsConstants.DEFAULT_LINE_COLOR;
            BackgroundBrush = SettingsConstants.DEFAULT_BACKGROUND_COLOR;
            PortraitResolution = new Resolution(1080, 1920);
            LandscapeResolution = new Resolution(1920, 1080);
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string defaultFolderPath = Path.Combine(solutionDirectory, "OutputImages");
            SaveDirectoryFilePath = defaultFolderPath;
        }

        #endregion

    }
}
