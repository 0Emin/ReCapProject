using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            //direk newlesek de olurdu, ama bu şekilde daha performanslı bir ürün geliştirmiş oluyoruz
            // Buradaki using => IDisposable pattern implementation of c#
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); // referansı yakala
                addedEntity.State = EntityState.Added;   // o aslında eklenecek bir nesne
                context.SaveChanges();                   // ve şimdi ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //filtre vermemişse hepsini, vermişsse ona göre getirmesini sağlayacağız
                return filter == null
                    ? context.Set<TEntity>().ToList()                    //bu kısım select * from cars döndürüyor: Filtre null mı ? evetse getir hepsini
                    : context.Set<TEntity>().Where(filter).ToList();     //burada da sadece filtreli kısmı
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified; //Updated yok, Modified var :)
                context.SaveChanges();
            }
        }
    }
}
