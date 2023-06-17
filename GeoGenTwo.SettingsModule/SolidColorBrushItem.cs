using System.Windows.Media;

namespace GeoGenTwo.SettingsModule
{
    public class SolidColorBrushItem
    {
        public SolidColorBrush ColorBrush { get; }
        public string ColorName { get; }

        public SolidColorBrushItem(SolidColorBrush colorBrush, string colorName)
        {
            ColorBrush = colorBrush;
            ColorName = colorName;
        }
    }
}
