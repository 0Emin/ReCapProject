using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validators.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//AuotfacCoreModule isminden emin değilim
namespace Core.Aspects.Autofac.Validation
{              //ValidationAspect bir attribute
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;//bana validator tipi ver diyor
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            { //IValidator gelmesi lazım diyor. Eğer backend geliştiricinin yazdığı şey bir validator değilse ona kız diyor
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //bu satır reflection, yani bi şeyleri çalışma anında new liyor. Mesela burada carValidator ün bir instance ını oluştur diyo.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  //sonra carValidator ün çalışma tipini bul diyo. Base tipinden Generic argümanlarından ilkini yani <Car> zaten ilki car
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);// onun parametrelerini bul (Validator un tipine eşit olan parametreleri bul )  //invocation metot demek
            foreach (var entity in entities) //Her birini tek tek gez
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
