using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.CarName).NotEmpty();         //boş olamaz
            RuleFor(c => c.CarName).MinimumLength(2);   //araba ismimiz min 2 karakter olmalıdır
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(0);  // Günlük Fiyat 0 dan büyük olmalıdır.
            RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(200).When(c => c.CarId == 1); //id si 1 olan arabalarımın günlük fiyatı 200 e eşit veya daha büyük olmalı
            RuleFor(c => c.CarName).Must(StartWithA).WithMessage("Arabalar 'A' harfi ile başlamalı");

            //Aralarına "." koyarak tek satırda da yazabilirdik hepsini, fakat SOLİD in S harfine aykırı. Mesela 18. satırdaki gibi when li bir şey yazmak isteyebiliriz sonradan, sorun çıkabilir
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
