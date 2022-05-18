using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{//generic constraint

    //T tipini sınırlandırıyoruz
    //Sadece Entities katmanındaki verilerim(car,brand,color)
    //gelebilsin istediim için class yazıyoruz. Buradaki class demek referans tip olabilir demek
    //herhangi bir class yazılmasın diye de IEntity yazıyoruz.
    //bunu yazarak IEntity veya ondan implemente olan bir şey olabileceğini belirtiyoruz.
    //T yerine IEntity de yazılamasın diye newlenebilir bir şey olması gerektiğini söylüyoruz ( new() )
    public interface IEntityRepository<T> where T : class,IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>>filter=null);
        T Get(Expression<Func<T, bool>>filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
