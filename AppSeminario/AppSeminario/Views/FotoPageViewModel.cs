using Acr.UserDialogs;
using AppSeminario.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class FotoPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;

        public DelegateCommand TomarFotoCmd { get; set; }
        public DelegateCommand TomarVideoCmd { get; set; }
        public DelegateCommand<Videos> VideoSeleccionadoCmd { get; set; }

        public ObservableCollection<Videos> ListaVideos { get; set; }

        public string RutaImagen { get; set; }
        public string RutaVideo { get; set; }

        public FotoPageViewModel(INavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;

            ListaVideos = new ObservableCollection<Videos>();

            TomarFotoCmd = new DelegateCommand(TomarFotoEjecutar);
            TomarVideoCmd = new DelegateCommand(TomarVideoEjecutar);
            VideoSeleccionadoCmd = new DelegateCommand<Videos>(VideoSeleccionadoEjecutar);
        }

        private async void VideoSeleccionadoEjecutar(Videos item)
        {
            var parameters = new NavigationParameters()
            {
                {"Video", item.Path }
            };

            Application.Current.Properties["Video"] = item.Path;
            await _navigationService.NavigateAsync("VideoPage", parameters);
        }

        private async void TomarVideoEjecutar()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsTakeVideoSupported)
                {
                    await _userDialogs.AlertAsync("No se tiene acceso a grabar videos, revise su configuración", "VideoS", "OK");
                    return;
                }

                MediaFile file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
                {
                    Name = "Video.mp4"
                });

                if (file == null)
                    return;

                RutaVideo = file.Path;
                ObtenerVideos();
            }
            catch (Exception error)
            {
                _userDialogs.Toast(error.Message);
            }
        }

        private async void TomarFotoEjecutar()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await _userDialogs.AlertAsync("No se tiene acceso a tomar fotos, revise su configuración", "Foto", "OK");
                    return;
                }

                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = "Imagen.jpg"
                });

                if (file == null)
                    return;
                
                RutaImagen = file.Path;
            }
            catch (Exception error)
            {
                _userDialogs.Toast(error.Message);
            }
        }

        private void ObtenerVideos()
        {
            RutaVideo = "";
            ListaVideos.Clear();
            int count = 0;

            foreach (var file in System.IO.Directory.GetFiles("/storage/emulated/0/Android/data/com.companyname.appseminario/files/Movies/"))
            {
                count++;
                Videos video = new Videos()
                {
                    Path = file,
                    Titulo = "Video " + count.ToString()
                };
                ListaVideos.Add(video);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            RutaImagen = "";
            ObtenerVideos();
        }
    }
}
