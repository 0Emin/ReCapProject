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
        internal static string MaintenanceTime="Sistem bakımda";
        internal static string CarsListed = "Arabalar Listelendi";
    }
}
