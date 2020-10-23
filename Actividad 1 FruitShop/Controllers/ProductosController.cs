using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Actividad_1_FruitShop.Models;
using Actividad_1_FruitShop.Models.ViewModels;
using Actividad_1_FruitShop.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Actividad_1_FruitShop.Controllers
{
    public class ProductosController : Controller
    {
        //inyeccion en el controller
        public IWebHostEnvironment Enviroment { get; set; }

        public ProductosController(IWebHostEnvironment env)
        {
            Enviroment = env;
        }
        public IActionResult Index()
        {

            ProductosIndexViewModel vm = new ProductosIndexViewModel();
            fruteriashopContext context = new fruteriashopContext();
            //Una sola conexion pero dla utilizan los dos repositorios
            CategoriasRepository categoriasrepository = new CategoriasRepository(context);
            ProductosRepository productosRepository = new ProductosRepository(context);

            int? id = 0;

            vm.Categorias = categoriasrepository.GetAll();
            vm.Productos = productosRepository.GetProductosByCategoria(id);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ProductosIndexViewModel vm)
        {


            fruteriashopContext context = new fruteriashopContext();

            CategoriasRepository categoriasrepository = new CategoriasRepository(context);
            ProductosRepository productosRepository = new ProductosRepository(context);



            vm.Categorias = categoriasrepository.GetAll();
            vm.Productos = productosRepository.GetProductosByCategoria(vm.IdCategoria);

            return View(vm);
        }

        public IActionResult Agregar()
        {
            ProductosViewModel vm = new ProductosViewModel();
            fruteriashopContext context = new fruteriashopContext();

            CategoriasRepository categoriasRepository = new CategoriasRepository(context);

            vm.Categorias = categoriasRepository.GetAll();


            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(ProductosViewModel pvm)
        {
            fruteriashopContext context = new fruteriashopContext();
            //guardar el archivo de la imagen
            

            try
            {
               if(pvm.Archivo==null)
                {
                    ModelState.AddModelError("", "Debe seleccionar una imagen para el producto");
                    CategoriasRepository categoriasRepository = new CategoriasRepository(context);

                    pvm.Categorias = categoriasRepository.GetAll();

                    return View(pvm);
                }
               else
                {
                    if (pvm.Archivo.ContentType != "image/jpeg" || pvm.Archivo.Length > 1024 * 1024 * 2)
                    {
                        ModelState.AddModelError("", "Debe seleccionar un archivo jpg de menos de 2MB.");
                        CategoriasRepository categoriasRepository = new CategoriasRepository(context);

                        pvm.Categorias = categoriasRepository.GetAll();

                        return View(pvm);
                    }

                }


                ProductosRepository repos = new ProductosRepository(context);

                repos.Insert(pvm.Producto);

               if(pvm.Archivo!=null)
                {
                    FileStream fs = new FileStream(Enviroment.WebRootPath + "/img_frutas/" + pvm.Producto.Id + ".jpg", FileMode.Create);
                    pvm.Archivo.CopyTo(fs);
                    fs.Close();
                }

             
                





                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                CategoriasRepository categoriasRepository = new CategoriasRepository(context);

                pvm.Categorias = categoriasRepository.GetAll();

                return View(pvm);
            }
        }

        public IActionResult Editar(int id)
        {
            fruteriashopContext context = new fruteriashopContext();
            ProductosViewModel pvm = new ProductosViewModel();
            ProductosRepository p = new ProductosRepository(context);

            pvm.Producto = p.Get(id);

            if(pvm.Producto==null)
            {
                return RedirectToAction("Index");
            }

            CategoriasRepository cr = new CategoriasRepository(context);
            pvm.Categorias = cr.GetAll();

            if(System.IO.File.Exists(Enviroment.WebRootPath+$"/img_frutas/{pvm.Producto.Id}.jpg"))
            {
                pvm.Imagen = pvm.Producto.Id + ".jpg";
            }
            else
            {
                pvm.Imagen = "no-disponible.png";
            }


            return View(pvm);
        }

        [HttpPost]
        public IActionResult Editar(ProductosViewModel pvm)
        {
            fruteriashopContext context = new fruteriashopContext();
            
          
            if(pvm.Archivo!=null)
            {
                if (pvm.Archivo.ContentType != "image/jpeg" || pvm.Archivo.Length > 1024 * 1024 * 2)
                {
                    ModelState.AddModelError("", "Debe seleccionar un archivo jpg de menos de 2MB.");
                    CategoriasRepository categoriasRepository = new CategoriasRepository(context);

                    pvm.Categorias = categoriasRepository.GetAll();

                    return View(pvm);
                }
            }
            try
            {

                ProductosRepository repos = new ProductosRepository(context);
                //busca el producto que queremos editar
                var p = repos.Get(pvm.Producto.Id);

                if(p!=null)
                {

                    p.Nombre = pvm.Producto.Nombre;
                    p.IdCategoria = pvm.Producto.IdCategoria;
                    p.Precio = pvm.Producto.Precio;
                    p.Descripcion = pvm.Producto.Descripcion;
                    p.UnidadMedida = pvm.Producto.UnidadMedida;
                    repos.Update(p);
                }

               if(pvm.Archivo!=null)
                {
                    FileStream fs = new FileStream(Enviroment.WebRootPath + "/img_frutas/" + pvm.Producto.Id + ".jpg", FileMode.Create);
                    pvm.Archivo.CopyTo(fs);
                    fs.Close();
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                CategoriasRepository categoriasRepository = new CategoriasRepository(context);

                pvm.Categorias = categoriasRepository.GetAll();

                return View(pvm);
            }
        }


        public IActionResult Eliminar(int id)
        {
            using(fruteriashopContext context = new fruteriashopContext())
            {
                ProductosRepository repos = new ProductosRepository(context);
                var p = repos.Get(id);

                if(p!=null)
                {
                    return View(p);

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }




        [HttpPost]
        public IActionResult Eliminar(Productos p)
        {
            using(fruteriashopContext context =new fruteriashopContext())
            {
                ProductosRepository repos = new ProductosRepository(context);
                var producto = repos.Get(p.Id);

                if(producto !=null)//este producto si existe
                {
                    repos.Delete(producto);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "El producto no existe o ya ha sido eliminado.");
                    return View(producto);
                }


               
            }



           
        }
    }
}
