using Actividad_2_ZooPlanet.Models;
using Actividad_2_ZooPlanet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad_2_ZooPlanet.Services
{
	public class MenuServices
	{
		public IEnumerable<Clase> Clases { get; private set; }

		public MenuServices()
		{
			using (animalesContext ctx = new animalesContext())
			{
				ClasesRepository repository = new ClasesRepository(ctx);
				Clases = repository.GetAll().ToList();
			}
		}



	}
}
