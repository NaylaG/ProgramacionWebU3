using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Actividad_2_ZooPlanet.Models;
using Actividad_2_ZooPlanet.Models.ViewModels;
using Actividad_2_ZooPlanet.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_2_ZooPlanet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IWebHostEnvironment Environment { get; set; }
        animalesContext context;
        public HomeController(animalesContext ctx, IWebHostEnvironment env)
        {
            context = ctx;
            Environment = env;
        }
        public IActionResult Index()
        {
            EspeciesRepository repos = new EspeciesRepository(context);

            return View(repos.GetAll());
        }


        public IActionResult Agregar()
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            ClasesRepository claseRepo = new ClasesRepository(context);

            vm.Clasifiacacion = claseRepo.GetAll();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(EspeciesViewModel vm)
        {
            try
            {        
                EspeciesRepository repo = new EspeciesRepository(context);
                repo.Insert(vm.Especie);
               
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository claseRepo = new ClasesRepository(context);

                vm.Clasifiacacion = claseRepo.GetAll();
                return View(vm);
                
            }
        }


        public IActionResult Editar(int id)
        {
            EspeciesViewModel vm = new EspeciesViewModel();
            EspeciesRepository especieRepo = new EspeciesRepository(context);
            ClasesRepository claseRepo = new ClasesRepository(context);
            vm.Especie = especieRepo.GetById(id);

            if(vm.Especie==null)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            vm.Clasifiacacion = claseRepo.GetAll();
           
            return View(vm);
        }
        [HttpPost]
        public IActionResult Editar(EspeciesViewModel vm)
        {
            try
            {
                EspeciesRepository repo = new EspeciesRepository(context);
                var esp = repo.GetById(vm.Especie.Id);
                if (esp != null)
                {
                    esp.Especie = vm.Especie.Especie;
                    esp.Habitat = vm.Especie.Habitat;
                    esp.IdClase = vm.Especie.IdClase;
                    esp.Observaciones = vm.Especie.Observaciones;
                    esp.Tamaño = vm.Especie.Tamaño;
                    esp.Peso = vm.Especie.Peso;
                    repo.Update(esp);
                }


                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ClasesRepository claseRepo = new ClasesRepository(context);

                vm.Clasifiacacion = claseRepo.GetAll();
                return View(vm);
            }
        }


        public IActionResult Eliminar(int id)
        {
            EspeciesRepository repos = new EspeciesRepository(context);

            return View(repos.GetById(id));
        }
        [HttpPost]
        public IActionResult Eliminar(Especies es)
        {
            EspeciesRepository repos = new EspeciesRepository(context);
            var especie = repos.GetById(es.Id);

            if(especie!=null)
            {
                repos.Delete(especie);
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            else
            {
                ModelState.AddModelError("", "La especie no existe o ya ha sido eliminada.");
                return View(especie);
            }

            
        }



        public IActionResult Imagen(int id)
        {
            EspeciesRepository repos = new EspeciesRepository(context);
            EspeciesViewModel vm = new EspeciesViewModel();

            vm.Especie = repos.GetById(id);

            if (System.IO.File.Exists(Environment.WebRootPath + $"/especies/{vm.Especie.Id}.jpg"))
            {
                vm.Imagen = vm.Especie.Id + ".jpg";
            }
            else
            {
                vm.Imagen = "no-disponible.png";
            }

            return View(vm);
        }
        [HttpPost]
        public IActionResult Imagen(EspeciesViewModel vm)
        {

            try
            {
                if (vm.Archivo.ContentType != "image/jpeg" || vm.Archivo.Length > 1024 * 1024 * 2)
                {
                    ModelState.AddModelError("", "Debe seleccionar un archivo jpg menor a 2MB");
                    if (System.IO.File.Exists(Environment.WebRootPath + $"/especies/{vm.Especie.Id}.jpg"))
                    {
                        vm.Imagen = vm.Especie.Id + ".jpg";
                    }
                    else
                    {
                        vm.Imagen = "no-disponible.png";
                    }
                    return View(vm);
                }
                EspeciesRepository repos = new EspeciesRepository(context);
                var especieimg = repos.GetById(vm.Especie.Id);

                if (especieimg != null && vm.Archivo != null)
                {
                    FileStream fs = new FileStream(Environment.WebRootPath + "/especies/" + vm.Especie.Id + ".jpg", FileMode.Create);
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }


                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                
                return View(vm);
            }
        }

    }
}
