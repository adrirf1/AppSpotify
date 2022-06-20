using AppSpotify.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.Media;
using AppSpotify.Services;
using AppSpotify.Views;
using AppSpotify.Service;
using Newtonsoft.Json;

namespace AppSpotify.ViewModels
{
    public class ListSongDetailViewModel : BaseViewModel
    {
        static ListSongsViewModel instance;
        public readonly ListSongDetailViewModel ListViewModel;
        private Command _MapsCommand;
        public Command MapsCommand => _MapsCommand ?? (_MapsCommand = new Command(MapsAction));


        private Command _SaveCommand;
        public Command SaveCommand => _SaveCommand ?? (_SaveCommand = new Command(SaveAction));

        private Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteAction));

        private Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        private Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));


        private SongModel _SongSelected;
        public SongModel SongSelected
        {
            get => _SongSelected;
            set => SetProperty(ref _SongSelected, value);
        }

        private string _Picture;
        public string Picture
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }

        public ListSongDetailViewModel(SongModel songSelected)
        {
            SongSelected = songSelected;
        }

        public ListSongDetailViewModel()
        {
            SongSelected = new SongModel();
            LoadSongs();
        }
        private int _ProductID;
        public int ProductID
        {
            get => _ProductID;
            set => SetProperty(ref _ProductID, value);
        }
        private string _SongName;
        public string SongName
        {
            get => _SongName;
            set => SetProperty(ref _SongName, value);
        }
        private string _Singer;
        public string Singer
        {
            get => _Singer;
            set => SetProperty(ref _Singer, value);
        }
        private float _Latitude;
        public float Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }
        private float _Longitude;
        public float Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }
        private async void SaveAction()
        {
            ApiResponse response;
            try
            {
                // Iniciamos el spinner
                IsBusy = true;

                // Creamos el modelo con los datos de los controles
                SongModel model = new SongModel
                {
                    ID = ProductID,
                    SongName = SongName,
                    Singer = Singer,
                    Picture = Picture,
                    Latitude = Latitude,
                    Longitude = Longitude
                };

                if (model.ID == 0)
                {
                    // Crear un nuevo producto
                    response = await new ApiService().PostDataAsync("Product", model);
                }
                else
                {
                    // Actualizar un producto existente
                    response = await new ApiService().PutDataAsync("Product", model);
                }

                // Si no fue satisfactorio enviamos un mensaje y terminamos el método
                if (response == null || !response.IsSuccess)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("AppProducts", response.Message, "Ok");
                    return;
                }

                // Actualizamos el listado de productos
                ListViewModel.RefreshSongs();

                // Cerramos la vista actual
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("AppProducts", $"Se generó un error al guardar el producto: {ex.Message}", "Ok");
            }
        }
    

        private async void DeleteAction()
        {

        }
        private void MapsAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new MapsPage(SongSelected.SongName, SongSelected.Latitude, SongSelected.Longitude));

        }


        private async void TakePictureAction()
        {
            // Inicializa la cámara
            await CrossMedia.Current.Initialize();



            // Si la cámara no está disponible o no está soportada lanza un mensaje y termina este método
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }



            // Toma la fotografía y la regresa en el objeto file
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "AppSpotify",
                Name = "camPicture.jpg"
            });



            // Si el objeto file es null termina este método
            if (file == null) return;



            // Asignamos la ruta de la fotografía en la propiedad de la imagen
            Picture = SongSelected.ImageBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);



            /*image.Source = ImageSource.FromStream(() =>
            {
            var stream = file.GetStream();
            return stream;
            });*/
        }

        private async void SelectPictureAction()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Pick Photo", ":( No pick photo available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            Picture = SongSelected.ImageBase64 = await new ImageService().ConvertImageFileToBase64(file.Path); //file.Path;

            /*image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });*/
        }

        private Command _NewCommand;
        public Command NewCommand => _NewCommand ?? (_NewCommand = new Command(NewAction));

        private List<SongModel> _GasStations;
        public List<SongModel> GasStations
        {
            get => _GasStations;
            set => SetProperty(ref _GasStations, value);
        }

        private SongModel _SongsSelected;
        public SongModel SongsSelected
        {
            get => _SongsSelected;
            set
            {
                if (SetProperty(ref _SongsSelected, value))
                {
                    SelectSongAction();
                }
            }
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

        private List<SongModel> _ListSongs;
        public List<SongModel> ListSongs
        {
            get => _ListSongs;
            set => SetProperty(ref _ListSongs, value);
        }
        private async void LoadSongs()
        {
            IsBusy = true;
            ListSongs = null;
            ApiResponse response = await new ApiService().GetDataAsync("Product");
            if (response == null || !response.IsSuccess)
            {
                // No hubo una respuesta exitosa
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("AppProducts", $"Error al cargar los productos: {response.Message}", "Ok");
                return;
            }
            ListSongs = JsonConvert.DeserializeObject<List<SongModel>>(response.Result.ToString());
            IsBusy = false;
        }

        public void RefreshSongs()
        {
            LoadSongs();
        }


    }
}

