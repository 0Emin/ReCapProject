using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public void Add(Car entity)
        {
            //direk newlesek de olurdu, ama bu şekilde daha performanslı bir ürün geliştirmiş oluyoruz
            // Buradaki using => IDisposable pattern implementation of c#
            using (ReCapContext context = new ReCapContext())
            {
                var addedEntity = context.Entry(entity); // referansı yakala
                addedEntity.State = EntityState.Added;   // o aslında eklenecek bir nesne
                context.SaveChanges();                   // ve şimdi ekle
            }
        }

        public void Delete(Car entity)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (ReCapContext context = new ReCapContext())
            {
                return context.Set<Car>().SingleOrDefault(filter);
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                //filtre vermemişse hepsini, vermişsse ona göre getirmesini sağlayacağız
                return filter == null
                    ? context.Set<Car>().ToList()                    //bu kısım select * from cars döndürüyor: Filtre null mı ? evetse getir hepsini
                    : context.Set<Car>().Where(filter).ToList();     //burada da sadece filtreli kısmı
            }
        }

        public void Update(Car entity)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified; //Updated yok, Modified var :)
                context.SaveChanges();
            }
        }
    }
}
