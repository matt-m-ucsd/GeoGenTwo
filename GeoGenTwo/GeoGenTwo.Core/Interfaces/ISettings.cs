using System.Windows.Media;

namespace GeoGenTwo.Core.Interfaces
{
    public interface ISettings
    {
        int NumLines { get; set; }
        SolidColorBrush LineColor { get; set; }
        SolidColorBrush BackgroundColor { get; set; }
    }
}
