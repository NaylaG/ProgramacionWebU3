using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actividad_1_FruitShop.Models;
using Actividad_1_FruitShop.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_1_FruitShop.Controllers
{
    public class CategoriasController : Controller
    {
        [Route("Categorias")]
        public IActionResult Index()
        {
            fruteriashopContext context = new fruteriashopContext();
            Repositories.Repository<Categorias> repos = new Repositories.Repository<Categorias>(context);

            return View(repos.GetAll().Where(x=>x.Eliminado==false).OrderBy(x=>x.Nombre));
        }
        //Get
        public IActionResult Agregar()
        {          

            return View();
        }
        //Post se utiliza sobre carga para que una vez la envie por get 
        [HttpPost]
        public IActionResult Agregar(Categorias c)
        {
            try
            {
                fruteriashopContext context = new fruteriashopContext();
                CategoriasRepository repos = new CategoriasRepository(context);
                repos.Insert(c);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(c);


            }
        }
        //editar por get
        public IActionResult Editar(int id)
        {
            using(fruteriashopContext context = new fruteriashopContext())
            {
                CategoriasRepository repos = new CategoriasRepository(context);
                var categoria = repos.Get(id);
                if(categoria == null)
                {
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
            
        }
        //editar pos post
        [HttpPost]
        public IActionResult Editar(Categorias categoriaTemporal)
        {
            try
            {
                using(fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var categoriaBd = repos.Get(categoriaTemporal.Id);

                    if(categoriaBd != null )
                    {

                        categoriaBd.Nombre = categoriaTemporal.Nombre;
                        repos.Update(categoriaBd);
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(categoriaTemporal);
            }
           
        }

        public IActionResult Eliminar(int id)
        {
          
                //Eliminacion fisica
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);
                    var categoriaBd = repos.Get(id);

                    if (categoriaBd == null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                        return View(categoriaBd);
                   
                }           

        }

        [HttpPost]
        public IActionResult Eliminar(Categorias categoriaTemporal)
        {
            //try
            //{
            //    //ELIMINACION FISICA
            //    using(fruteriashopContext context = new fruteriashopContext())
            //    {
            //        CategoriasRepository repos = new CategoriasRepository(context);

            //        var categoriaBD = repos.Get(categoriaTemporal.Id);
            //        repos.Delete(categoriaBD);

            //        return RedirectToAction("Index");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message);
            //    return View(categoriaTemporal);
            //}

            //ELIMINACION LOGICA

            try
            {
               
                using (fruteriashopContext context = new fruteriashopContext())
                {
                    CategoriasRepository repos = new CategoriasRepository(context);

                    var categoriaBD = repos.Get(categoriaTemporal.Id);
                    categoriaBD.Eliminado = true;
                    repos.Update(categoriaBD);

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(categoriaTemporal);
            }

        }
    }
}
