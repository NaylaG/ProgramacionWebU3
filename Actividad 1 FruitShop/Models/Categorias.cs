﻿using System;
using System.Collections.Generic;

namespace Actividad_1_FruitShop.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Productos = new HashSet<Productos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }

        public virtual ICollection<Productos> Productos { get; set; }
    }
}
