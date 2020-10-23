using Actividad_1_FruitShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Actividad_1_FruitShop.Repositories
{
    public class ProductosRepository : Repository<Productos>  //puede usar lo que tiene el repositorio que esta heredando (herencia)
    {

        public ProductosRepository(fruteriashopContext context) : base(context) { } //como el papa nesecita nu context este nuevo repositorio debe tener un context tambien

        public IEnumerable<Productos> GetProductosByCategoria(string nombre)//recibe el nombre de una categoria y regresa todos los productos de esa categoria
        {
            return Context.Productos.Where(x => x.IdCategoriaNavigation.Nombre == nombre);
        }

        public IEnumerable<Productos> GetProductosByCategoria(int? idCategoria)//que acepte la categoria o que acepte null, si es null regresa todos los productos
        {
            return Context.Productos.Include(x=>x.IdCategoriaNavigation).Where(x => idCategoria==0 || x.IdCategoria == idCategoria).OrderBy(x=>x.Nombre);
        }


        public Productos GetProductosbyCategoriaNombre(string categoria, string nombre)

        {
            return Context.Productos.Include(x => x.IdCategoriaNavigation).FirstOrDefault(x => x.IdCategoriaNavigation.Nombre == categoria && x.Nombre == nombre);//el include solo si queremos regresar el nombre de la categoria
        }

        public override bool Validate(Productos entidad)
        {
            //parametro por parametro
            if(entidad.Precio == null && entidad.Precio<=0 )
            {
                throw new Exception("Debe indicar el precio del producto a agragar");

            }
            if(string.IsNullOrWhiteSpace(entidad.UnidadMedida))
            {
                throw new Exception("Debe inidcar la unidad de medida");
            }
            if (string.IsNullOrWhiteSpace(entidad.Descripcion))
            {
                throw new Exception("Debe inidcar la descripcion del producto");
            }

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
            {
                throw new Exception("Debe inidcar el nombre del producto");
            }
            if(Context.Productos.Any(x=>x.Nombre==entidad.Nombre && x.Id!=entidad.Id))
            {
                throw new Exception("Ya existe un producto registardo con el mismo nombre");
            }
            if(!Context.Categorias.Any(x=>x.Id==entidad.IdCategoria && x.Eliminado==false))
            {
                throw new Exception("No existe la categoria especificada");
            }






            return true;
        }


    }
}
