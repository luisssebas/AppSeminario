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
        public DelegateCommand ObtenerDatosCmd { get; set; }

        public ObservableCollection<ProductoMS> Productos { get; set; }

        public string Imagen { get; set; }

        public WebPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _servicio = new ServicioHttp();

            ObtenerDatosCmd = new DelegateCommand(ObtenerDatosEjecutar);
            ObtenerImagenesCmd = new DelegateCommand(ObtenerImagenesEjecutar);
            Productos = new ObservableCollection<ProductoMS>();
        }

        private async void ObtenerImagenesEjecutar()
        {
            var mensajeSalida = await _servicio.ObtenerImagenes();
            
            Imagen = mensajeSalida.FirstOrDefault().urls.regular;
        }

        private async void ObtenerDatosEjecutar()
        {
            var mensajeSalida = await _servicio.ObtenerDatos();

            foreach (var item in mensajeSalida)
            {
                ProductoMS producto = new ProductoMS()
                {
                    Nombre = item.Nombre,
                    ProductoId = item.ProductoId,
                    Servicio = item.Servicio
                };

                Productos.Add(producto);
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
