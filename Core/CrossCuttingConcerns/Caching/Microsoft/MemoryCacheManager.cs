using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern: Var olan bir sistemi (inmemorycache i) kendi sistemimize uyarladık. Sadece onu kullanmak üzere çağırmadık yani.
        IMemoryCache _memoryCache; //contructor versek çalışmaz, çünkü aspect, bağımlılık zincirinin içinde değil, bunun için " CoreModule " ümüz var, şimdi ona gidip birisi senden ICacheManager isterse ona Microsoft un MemoryCacheManager ini ver diycez.

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }                                                       //biz IMemoryCache yazdığımızda gidiyo belleğe bakıyo var mı karşılığı diye, gidiyor 14. satırdan alıyor(CoreModule) 

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }
        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //be sadece bellekte böyle bir anahtar var mı yok mu onu bulmak istiyorum. Ben de bana value lazım olmadığı için onun yerine " out _ " yazabiliyorum. Böyle bir kullanım varmış
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        //Çalışma anında bellekten silmeye yarıyor -Reflection-(RemoveByPattern)
        public void RemoveByPattern(string pattern) //E Copy Paste 46-62
        {                                                                       //Cachlenen şey buraya geliyomuş(EntriesCollection)
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic; //Definition ı memoryCache olanları bul
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)    //Herbir cache elemanını gez
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase); //pattern ı bu şekilde oluşturuyoruz;SingleLine olacak,Compile edilmiş olacak, Casesensitivy olmayacak(IgnoreCase)
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); //Şu kurala uyanları ki burası silineceklerin kuralları

            foreach (var key in keysToRemove) // yukarıdaki kurallarla keystToRemove un içine attıklarımızı bellekten uçuruyoruz
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
