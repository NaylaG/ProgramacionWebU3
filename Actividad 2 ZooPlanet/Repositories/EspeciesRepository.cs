using Actividad_2_ZooPlanet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Actividad_2_ZooPlanet.Repositories
{
	public class EspeciesRepository : Repository<Especies>
	{

		public EspeciesRepository(animalesContext ctx) : base(ctx)
		{

		}


		public override IEnumerable<Especies> GetAll()
		{
			return base.GetAll().OrderBy(x => x.Especie);
		}

		public IEnumerable<Especies> GetEspeciesByClase(string Id)
		{

		
			return Context.Especies.Include(x => x.IdClaseNavigation).Where(x => x.IdClaseNavigation.Nombre == Id).OrderBy(x => x.Especie);
		}

        public Especies GetEspecie(int id)
        {
			return Context.Especies.Include(x=>x.IdClaseNavigation).FirstOrDefault(x => x.Id == id);
        }

    }
}
