using AppSpotify.Models;
using AppSpotify.Service;
using AppSpotify.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppSpotify.ViewModels
{
    public class ListSongsViewModel: BaseViewModel
    {
        static ListSongsViewModel instance;
        
        

        private List<SongModel> _GasStations;
        public List<SongModel> GasStations
        {
            get => _GasStations;
            set => SetProperty(ref _GasStations, value);
        }

        private SongModel _SongSelected;
        public SongModel SongSelected
        {
            get => _SongSelected;
            set
            {
                if (SetProperty(ref _SongSelected, value))
                {
                    SelectSongAction();
                }
            }
        }

        public ListSongsViewModel()
        {
            instance = this;

      
        }

        public static ListSongsViewModel GetInstance()
        {
            return instance;
        }

        private void NewAction(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new ListSongsPage());
        }

        private void SelectSongAction()
        {
            //Application.Current.MainPage.Navigation.PushAsync(new TabbedPage1(SongSelected));
        }
       
       

        
    }
}
