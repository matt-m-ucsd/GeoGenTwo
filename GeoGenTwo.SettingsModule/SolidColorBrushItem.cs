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

        public SolidColorBrushItem(Color color, string colorName)
        {
            ColorBrush = new SolidColorBrush(color);
            ColorName = colorName;
        }

        public override bool Equals(object obj)
        {
            if (obj is SolidColorBrushItem otherObj)
            {
                return ColorBrush.Color.Equals(otherObj.ColorBrush.Color);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ColorBrush.Color.GetHashCode();
        }
    }
}
