using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad_2_ZooPlanet.Models.ViewModels
{
    public class EspeciesViewModel
    {
        public Especies Especie { get; set; }
        public IEnumerable<Clase> Clasifiacacion { get; set; }
        public IFormFile Archivo { get; set; }
        public string Imagen { get; set; }
    }
}
