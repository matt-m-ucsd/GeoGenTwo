using System.Windows.Media;

namespace GeoGenTwo.Core.Interfaces
{
    public interface ISettings
    {
        int NumLines { get; set; }
        Color LineColor { get; set; }
        Color BackgroundColor { get; set; }
    }
}
