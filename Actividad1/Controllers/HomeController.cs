using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actividad1.Models;
using Actividad1.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Actividad1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agregar()
        {
            Carreras c = new Carreras()
            {
                Clave = "N",
                Descripcion = "Una carrera de prueba",
                Especialidad = "Nombre de la Especialidad",
                Nombre = "Carrera de prueba",
                Plan = "ABC123"
            };

            sistem14_mapacurricularContext context = new sistem14_mapacurricularContext();
            Repository<Carreras> repos = new Repository<Carreras>(context);

            repos.Insert(c);

            return Ok("La carrera se agrego");

        }
    }
}
