using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validators.FluentValidation
{         //static yaptık. Tek instance oluşturulsun, uygulama boyunca o kullanılsın diye
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)                                 //BU NOTLARI CAR MANAGER DAN ÇEKTİM KODLARLA BİRLİKTE
        {
            var context = new ValidationContext<object>(entity);      //Car için doğrulama yapacağımızı söylüyoruz 
                                                                      //burada sadece Car için kod yazdık, bütün projelerimizde kullanabileceğimiz bir yapı için Core katmanına gidiyoruz.

            var result = validator.Validate(context);        //Yazdığımız validatör kurallarını kullanarak ilgili contexti yani car ' ı doğrula
            if (!result.IsValid)
            {
                //Sonuç geçerli değilse hata fırlat


                throw new ValidationException(result.Errors);
            }
        }
    }
}


