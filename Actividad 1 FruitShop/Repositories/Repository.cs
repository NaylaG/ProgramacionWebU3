﻿using Actividad_1_FruitShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actividad_1_FruitShop.Repositories
{
    public class Repository<T> where T : class
    {
        public fruteriashopContext Context { get; set; }

        public Repository(fruteriashopContext context)
        {
            Context = context;
        }
        //virtual es un permiso apra que cada metodo pueda ser modificado
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();

        }

        public virtual T Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual void Insert(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Add<T>(entidad);
                Context.SaveChanges();
            }

        }

        public virtual void Update(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Update<T>(entidad);
                Context.SaveChanges();
            }
        }

        public virtual void Delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }
        //polimorfismo: Cada clase que hereda de un padre puede realizar sus acciones a su propia manera
        public virtual bool Validate(T entidad)
        {
            return true;
        }



    }
}
