using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    //Constant: sabit demek
    //newlemek zorunda kalmamak için static verdik
    public static class Messages
    {
        public static string CarAdded = "Araba Eklendi";
        public static string CarNameInvalid = "Araba ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string CarsListed = "Arabalar Listelendi";
        public static string CarCountOfBrandError = "Bir markada en fazla 10 araba olabilir";
        public static string CarUpdated = "Araba bilgileri güncellendi";
        public static string CarNameAlreadyExist = "Bu isme sahip başka bir araba zaten mevcut";
        public static string BrandLimitExceded = "Marka sayısı maksimum limite (15) ulaştı";
    }
}
