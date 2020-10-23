using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad_1_FruitShop.Models.ViewModels
{
    public class ProductosIndexViewModel
    {
        public IEnumerable<Categorias> Categorias { get; set; }
        public IEnumerable<Productos> Productos { get; set; }
        public int IdCategoria { get; set; }


    }
}
