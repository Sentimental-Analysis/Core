using System;

namespace Core.Models
{
    public class Tweet
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Language { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Sentiment { get; set; }
    }
}