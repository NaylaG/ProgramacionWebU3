using Actividad1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Actividad1.Repositories
{
    public class Repository<T> where T : class //repositorio que puede recibir cualquier tipo de dato
        //un repositorio de T donde sea una clase y no sea una de bytes o de enteros
    {
        public sistem14_mapacurricularContext Context { get; set; }//primero tengo que recibir un contexto

        public Repository(sistem14_mapacurricularContext context)//
        {
            Context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T Get(object id)
        {
            return Context.Find<T>(id);
            //el metodo find busca automaticamente un objeto de tipo T basado en su campo llave id
        }
        public void Insert(T entidad)
        {
            Context.Add<T>(entidad);
            Context.SaveChanges();
        }

        public void Delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }
        public void Update(T entidad)
        {
            Context.Update<T>(entidad);
            Context.SaveChanges();
        }

    }
}
