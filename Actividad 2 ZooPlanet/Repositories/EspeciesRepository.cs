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

        public override Especies GetById(object id)
        {
            return Context.Especies.Include(x => x.IdClaseNavigation).FirstOrDefault(x => x.Id == (int)id);
		}

        public override bool Validate(Especies entidad)
        {
            if(string.IsNullOrWhiteSpace(entidad.Especie))
		    {
				throw new Exception("Debe indicar el nombre de la espacie");
			}
			if (string.IsNullOrWhiteSpace(entidad.Habitat))
			{
				throw new Exception("Debe indicar el habitat de la espacie");
			}
			
			if (entidad.Peso==null || entidad.Peso<=0)
			{
				throw new Exception("Debe indicar el peso de la espacie");
			}
			if (entidad.Tamaño==null|| entidad.Tamaño<=0)
			{
				throw new Exception("Debe indicar un tamaño valido a la espacie");
			}
			if (string.IsNullOrWhiteSpace(entidad.Observaciones))
			{
				throw new Exception("Debe indicar las observaciones de la espacie");
			}
			if (Context.Especies.Any(x=>x.Especie == entidad.Especie&& x.Id!=entidad.Id))
            {
				throw new Exception("Ya existe una especie registrada con el mismo nombre");
			}

			if (!Context.Clase.Any(x=>x.Id==entidad.IdClase))
			{
				throw new Exception("No existe la clasificación especificada");
			}


			return true;
		}

    }
}
