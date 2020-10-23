using Actividad_1_FruitShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad_1_FruitShop.Services
{
    public class CategoriaService
    {
        public List<Categorias> Categorias { get; set; }

        public CategoriaService()
        {
            using (fruteriashopContext context = new fruteriashopContext())
            {
                Repositories.Repository<Categorias> repos = new Repositories.Repository<Categorias>(context);
                Categorias = repos.GetAll().Where(x => x.Eliminado == false).OrderBy(x => x.Nombre).ToList();
            }

        }
    }
}
