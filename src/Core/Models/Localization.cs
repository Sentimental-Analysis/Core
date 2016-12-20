namespace Core.Models
{
    public sealed class Localization
    {
        public Localization(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Longitude { get; }
        public double Latitude { get; }
    }
}