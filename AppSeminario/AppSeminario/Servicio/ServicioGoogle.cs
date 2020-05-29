using AppSeminario.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppSeminario.Servicio
{
    public class ServicioGoogle
    {
        public static string ApiUrl = "https://maps.googleapis.com/maps/api/directions/json";

        public async Task<RutasMS> ObtenerRuta(RutasME mensajeEntrada)
        {
            RutasMS response2 = new RutasMS();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "App Movil");

                var json = JsonConvert.SerializeObject(mensajeEntrada);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string uri = ApiUrl + "?mode=driving&transit_routing_preference=less_driving&origin=" + mensajeEntrada.RutaInicial.Latitud + "," + mensajeEntrada.RutaInicial.Longitud
                                    + "&destination=" + mensajeEntrada.RutaFinal.Latitud + "," + mensajeEntrada.RutaFinal.Longitud
                                    + "&key=YourApiKeyHere";

                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                response2 = JsonConvert.DeserializeObject<RutasMS>(responseString);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response2;
        }
    }
}
