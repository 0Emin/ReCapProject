using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager   //Burası bütün alternatiflerimizin interface i. Biz memory kullancaz ama başka bir cache yöntemi eklemek istersek yine bundan implemente edebiliriz
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value,int duration);
        bool IsAdd(string key); //bellekte böyle bir cache değeri var mı?
        void Remove(string key); //Cache den uçurmak için 
        void RemoveByPattern(string pattern); //mesela içerisinde "brand" olanları uçurmak istersek diye, (CarManagerdaki operasyonlarımızda) 
    }
}
