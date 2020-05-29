using Acr.UserDialogs;
using AppSeminario.Models;
using AppSeminario.Servicio;
using Plugin.Geolocator;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class MapaPageViewModel : INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private readonly ServicioGoogle _servicioGoogle;

        public ObservableCollection<Pin> Pines { get; set; }
        public ObservableCollection<Position> Rutas { get; set; }

        public ObservableCollection<RutaVisitadasMS> RutaVisitadas { get; set; }

        private RutasMS _ruta;

        public MapaPageViewModel(INavigationService navigationService, IUserDialogs userDialogs)
        {
            _navigationService = navigationService;
            _userDialogs = userDialogs;
            _servicioGoogle = new ServicioGoogle();

            Pines = new ObservableCollection<Pin>();
            Rutas = new ObservableCollection<Position>();
            RutaVisitadas = new ObservableCollection<RutaVisitadasMS>();

            //RutaVisitadasMS rutaVisitadas = new RutaVisitadasMS() { Latitud = "-0.1923356", Longitud = "-78.4999312,19" };
            //RutaVisitadas.Add(rutaVisitadas);
            RutaVisitadasMS rutaVisitadas = new RutaVisitadasMS() { Latitud = "-0.1687671", Longitud = "-78.4703572" };
            RutaVisitadas.Add(rutaVisitadas);
        }

        public async void ObtenerDatos()
        {
            try
            {
                string tempLatitud, tempLongitud;
                var position = await ProcesaTareaObtenerPosicion(TimeSpan.FromSeconds(10000));
                tempLatitud = position.Latitude.ToString();
                tempLongitud = position.Longitude.ToString();
                Rutas.Clear();
                Pines.Clear();
                Pines.Add(new Pin() { Position = new Position(Convert.ToDouble(tempLatitud), Convert.ToDouble(tempLongitud)), Type = PinType.Generic, Label = "Yo" });

                foreach (var rutas in RutaVisitadas)
                {
                    RutasME ruta = new RutasME() { RutaInicial = new UbicacionMS() { Latitud = tempLatitud, Longitud = tempLongitud }, RutaFinal = new UbicacionMS() { Latitud = rutas.Latitud, Longitud = rutas.Longitud } };

                    _ruta = await _servicioGoogle.ObtenerRuta(ruta);

                    Rutas.Add(new Position(Convert.ToDouble(tempLatitud), Convert.ToDouble(tempLongitud)));

                    foreach (var item in _ruta.routes)
                    {
                        foreach (var item2 in item.legs)
                        {
                            foreach (var item3 in item2.steps)
                            {
                                Rutas.Add(new Position(Convert.ToDouble(item3.start_location.lat), Convert.ToDouble(item3.start_location.lng)));
                                List<Position> decodificacion = DecodePolyline(item3.polyline.points);
                                foreach (var item4 in decodificacion)
                                {
                                    Rutas.Add(new Position(Convert.ToDouble(item4.Latitude), Convert.ToDouble(item4.Longitude)));
                                }
                                Rutas.Add(new Position(Convert.ToDouble(item3.end_location.lat), Convert.ToDouble(item3.end_location.lng)));
                            }
                        }
                    }
                    Rutas.Add(new Position(Convert.ToDouble(ruta.RutaFinal.Latitud), Convert.ToDouble(ruta.RutaFinal.Longitud)));
                    tempLatitud = rutas.Latitud;
                    tempLongitud = rutas.Longitud;
                    Pines.Add(new Pin() { Position = new Position(Convert.ToDouble(tempLatitud), Convert.ToDouble(tempLongitud)), Type = PinType.Generic, Label = "lugar" });
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Toast(ex.Message);
            }
        }

        private async Task<Plugin.Geolocator.Abstractions.Position> ProcesaTareaObtenerPosicion(TimeSpan mensajeEntrada)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            return await locator.GetPositionAsync(mensajeEntrada);
        }

        private List<Position> DecodePolyline(string encodedPoints)
        {
            if (string.IsNullOrWhiteSpace(encodedPoints))
                return null;


            int index = 0;
            var polylineChars = encodedPoints.ToCharArray();
            var poly = new List<Position>();
            int currentLat = 0;
            int currentLng = 0;
            int next5Bits;

            while (index < polylineChars.Length)
            {
                int sum = 0;
                int shifter = 0;
                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                }
                while (next5Bits >= 32 && index < polylineChars.Length);
                if (index >= polylineChars.Length)
                {
                    break;
                }
                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                sum = 0;
                shifter = 0;
                do
                {
                    next5Bits = polylineChars[index++] - 63;
                    sum |= (next5Bits & 31) << shifter;
                    shifter += 5;
                }
                while (next5Bits >= 32 && index < polylineChars.Length);
                if (index >= polylineChars.Length && next5Bits >= 32)
                {
                    break;
                }

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                var mLatLng = new Position(Convert.ToDouble(currentLat) / 100000.0, Convert.ToDouble(currentLng) / 100000.0);
                poly.Add(mLatLng);
            }

            return poly;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ObtenerDatos();
        }
    }
}
