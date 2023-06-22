using System.Windows.Media;

namespace GeoGenTwo.Core.Interfaces
{
    public interface ISettings
    {
        int NumLines { get; set; }
        Brush LineBrush { get; set; }
        Brush BackgroundBrush { get; set; }
        Resolution LandscapeResolution { get; set; }
        Resolution PortraitResolution { get; set; }

        string SaveDirectoryFilePath { get; set; }
    }
}
