using AppSpotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppSpotify.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsPage : ContentPage
    {
        public MapsPage(string name, double latitude, double longitude)
        {
            InitializeComponent();

            SongModel cancion = new SongModel
            {
                SongName = name,
                Latitude = latitude,
                Longitude = longitude
            };


            MapControl.MoveToRegion(
                  MapSpan.FromCenterAndRadius(
                      new Position(
                          cancion.Latitude,
                          cancion.Longitude
                      ), Distance.FromMiles(.5)
                  )
              ); ;

            
        }
    }
}