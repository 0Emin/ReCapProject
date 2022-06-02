using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {                                                    //Çalıştırılmak istenen metot ( MethodInfo method )
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)                //çalıştırılmak istenen metodun üzerine bakar
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); //Oradaki interceptorleri buluyor, yani aspectleri
            classAttributes.AddRange(methodAttributes);                     //ve onları çalıştırıyo
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));  // Burası yazılacak , Eğer biz performans aspect ini buraya bunun gibi eklersek bu benim mevcutta ve ilerde ekleyeceğim tüm metotlara eklenir

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
