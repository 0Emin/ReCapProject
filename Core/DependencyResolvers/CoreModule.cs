using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Core.CrossCuttingConcerns.Caching;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection servicecollection)
        {
            // MemoryCacheManager da  "_memoryCache" yazdığımız zaman injection otomatik eklenmiş oluyor. Arkaplanda memorycache instance ı oluşuyor.  
            servicecollection.AddMemoryCache(); //.NET in kendisine ait. .NET Core kendisi otomatik injeciton yapıyor. Redis e geçersek buna gerek kalmayacak. Burayı da MemoryCacheManager için ekledik
            servicecollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            servicecollection.AddSingleton<ICacheManager, MemoryCacheManager>(); //Birisi senden ICacheManager ı isterse ona MemoryCacheManager i ver
                                                                                 //Redis i ekleyecek olursak Memory yerine Redis yazmamız  yeterli olacak
            servicecollection.AddSingleton<Stopwatch>(); //performans
        }
    }
}
