using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception //CacheAspect imiz bir attribute
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) //default değer vermişiz. Biz süre vermezsek 60 dk sonra sistem onu bellekten atıcak, cache den uçacak
        {
            _duration = duration; //duraiton u burada set etmişiz
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //Hangi CacheManager ı kullandığımı belirtiyorum. Redis kullansak da buraya dokunmayız
        }

        public override void Intercept(IInvocation invocation)
        {//26. , 27. satır reflection 
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //methodun ismini bulmaya çalışıyoz. ReflectedType namespace i, ReflectedType.FullName ise namespace + managerımızı - ilgili classımızı - söyler. Invocation.Method.Name ise metodumuzun ismini belirtir. Örneğin GetAll();
            var arguments = invocation.Arguments.ToList(); // metodun parametrelerin, listeye çevir
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // ?? varsa soldakini, yoksa sağdakini ekle demek, string.Join - bir araya getirmek demektir - bir LINQ sorgusudur. Paremetrelerimin arasına " , " konulmasını istiyorum " string.Join " kullanarak." arguements.Select " parametre değerlerini listeye çevirir.
            if (_cacheManager.IsAdd(key)) // daha önce böyle bi key değeri var mı diye bakıyoruz
            {
                invocation.ReturnValue = _cacheManager.Get(key); // varsa metodu hiç çalıştırmadan geri dön demek. CacheManager dan get et. Çünkü cache de var
                return;
            }
            invocation.Proceed();  // metodu devam ettir ; veritabanından datayı getirdi  //!!!!!!
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //cache de ekliyoruz daha önce cache e eklenmemiş diye
        }
    }
}
// bir key oluşturuyoz. Eğer bu key daha önce yoksa veritabanından al ama cache e ekle. Varsa zaten cache ten al