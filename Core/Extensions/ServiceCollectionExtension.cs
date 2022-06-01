using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{//extension olduğu için static
    public static class ServiceCollectionExtension //service koleksiyonunu da extend edebilelim diye oluşturduk
    {                                                        //buradaki this le genişletmek istediğimizi belirtiyoruz            
        public static IServiceCollection AddDependencyResolvers
            (this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);               //polimorfizm yapıyoruz
            }

            return ServiceTool.Create(serviceCollection);  
        }
    }
}
// Bu yaptığımız hareketle, bizim ekleyeceğimiz tüm injection ları bir arada toplayabileceğimiz bir yapıya dönüştü