using System;
using System.Collections.Generic;
using System.Text;

namespace AppSpotify.Models
{
    public class SongModel
    {
        // Propiedades
        public int ID { get; set; }
        public string SongName { get; set; }
        public string Singer { get; set; }
        public string Picture { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ImageBase64 { get; set; }
    }
}
