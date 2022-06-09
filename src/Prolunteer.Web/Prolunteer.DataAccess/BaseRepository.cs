using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prolunteer.Common;
using Prolunteer.DataAccess.EntityFramework;

namespace Prolunteer.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ProlunteerContext Context;

        public BaseRepository(ProlunteerContext Context)
        {
            this.Context = Context;
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void InsertRange(ICollection<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void UpdateRange(ICollection<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
        }
    }
}
