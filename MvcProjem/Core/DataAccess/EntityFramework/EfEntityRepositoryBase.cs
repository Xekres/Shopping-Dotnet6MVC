using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        //Sadece Ef için kullanılacak olan Base class ım.
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity=context.Entry(entity);
                addedEntity.State = EntityState.Added;
                //nesnemle ilgili bir Entry oluşturup onun eklenmesi gereken bir nesne oldugunu bil
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                //nesnemle ilgili bir Entry oluşturup onun eklenmesi gereken bir nesne oldugunu bil
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter )
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()      //filtre null ise
                    : context.Set<TEntity>().Where(filter).ToList();  //Filtre null değilse
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                //nesnemle ilgili bir Entry oluşturup onun eklenmesi gereken bir nesne oldugunu bil
                context.SaveChanges();
            }
        }
    }
}
