using ppedv.ThirstyPerson.Domain;
using ppedv.ThirstyPerson.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.ThirstyPerson.Data.EF
{
    public class EFRepository : IRepository
    {
        public EFRepository(EFContext context)
        {
            this.context = context;
        }
        private readonly EFContext context;

        public void Add<T>(T item) where T : Entity
        {
            context.Set<T>().Add(item);
        }

        public void Delete<T>(T item) where T : Entity
        {
            context.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>().ToList();
        }

        public T GetByID<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update<T>(T item) where T : Entity
        {
            var loaded = GetByID<T>(item.ID); // es kann ja sein, dass das element schon gelöscht wurde
            if (loaded != null)
                context.Entry<T>(loaded).CurrentValues.SetValues(item); // Update der Werte
        }
    }
}
