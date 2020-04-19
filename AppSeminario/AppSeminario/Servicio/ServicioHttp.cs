using AppSeminario.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppSeminario.Servicio
{
    public class ServicioHttp
    {
        public static string ApiUrl = "http://172.30.2.108:6969";

        public async Task<List<ProductoMS>> ObtenerDatos()
        {
            List<ProductoMS> response2 = new List<ProductoMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = ApiUrl + "/api/Servicio/ObtenerProductos";
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                response2 = JsonConvert.DeserializeObject<List<ProductoMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response2;
        }

        public async Task<List<ImagenMS>> ObtenerImagenes()
        {
            List<ImagenMS> response2 = new List<ImagenMS>();
            try
            {
                HttpClient client = new HttpClient();

                string uri = "https://api.unsplash.com/photos/?client_id=OfQMZq1pVWpITJBh5DvkcSgNpaq245y-pxfNWXfh2TU";
                HttpResponseMessage response = await client.GetAsync(uri);
                var responseString = await response.Content.ReadAsStringAsync();

                response2 = JsonConvert.DeserializeObject<List<ImagenMS>>(responseString);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response2;
        }

    }
}
