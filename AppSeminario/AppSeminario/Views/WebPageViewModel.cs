using AppSeminario.Models;
using AppSeminario.Servicio;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class WebPageViewModel : INavigatedAware
    {
        private INavigationService _navigationService;
        private ServicioHttp _servicio;

        public DelegateCommand ObtenerImagenesCmd { get; set; }

        public ObservableCollection<ImagenMS> Imagenes { get; set; }

        public string Imagen { get; set; }

        public WebPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _servicio = new ServicioHttp();

            ObtenerImagenesCmd = new DelegateCommand(ObtenerImagenesEjecutar);
            Imagenes = new ObservableCollection<ImagenMS>();
        }

        private async void ObtenerImagenesEjecutar()
        {
            var mensajeSalida = await _servicio.ObtenerImagenes();

            Imagenes.Clear();
            foreach (var item in mensajeSalida)
            {
                Imagenes.Add(item);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
