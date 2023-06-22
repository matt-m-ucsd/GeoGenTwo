namespace GeoGenTwo.Core
{
    public class Resolution
    {    
        public int Width { get; set; }
        public int Height { get; set; }

        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            if (obj is Resolution otherResolution)
            {
                return otherResolution.Height == this.Height && otherResolution.Width == this.Width;
            }    
            return false;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }
    }
}
