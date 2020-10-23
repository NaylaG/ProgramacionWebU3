﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad_1_FruitShop.Models.ViewModels
{
    public class ProductosViewModel
    {
        public IEnumerable<Categorias> Categorias { get; set; }
        public Productos Producto { get; set; }
        public IFormFile Archivo { get; set; }
        public string Imagen { set; get; }
    }
}
