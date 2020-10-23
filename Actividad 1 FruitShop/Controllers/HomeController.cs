using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actividad_1_FruitShop.Models;
using Actividad_1_FruitShop.Models.ViewModels;
using Actividad_1_FruitShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_1_FruitShop.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/Index")]
        [Route("Home")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("{id}")]
        public IActionResult Categoria(string id)
        {

            using (fruteriashopContext context = new fruteriashopContext())
            {
                //fruteriashopContext context = new fruteriashopContext();
                ProductosRepository repos = new ProductosRepository(context);

                CategoriaViewModel vm = new CategoriaViewModel();
                vm.Nombre = id;
                vm.Productos = repos.GetProductosByCategoria(id).ToList();//to list es mas rapido porque no lo consulta en la bd si no que ya se encuentra en la memoria
                return View(vm);
            }
            

        }
        [Route("Detalles/{categoria}/{id}")]
        public IActionResult Ver(string categoria, string id)
        {
            using (fruteriashopContext context = new fruteriashopContext())//el contexto no se instancia dentro del repositorio ya que no queremos que se hagan multiples conexiones
            {
                ProductosRepository repos = new ProductosRepository(context);

                Productos p = repos.GetProductosbyCategoriaNombre(categoria, id.Replace("-", " "));

                return View(p);
            }
          
        }
    }

}
