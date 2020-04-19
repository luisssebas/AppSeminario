using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Models
{
    public class ImagenMS
    {
        public string id { get; set; }
        public string description { get; set; }
        public string alt_description { get; set; }
        public UrlMS urls { get; set; }
        public LinkMS links { get; set; }
    }
}
