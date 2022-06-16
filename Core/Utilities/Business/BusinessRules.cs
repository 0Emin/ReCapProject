using Core.Utilities.Results;

namespace Core.Utilities.Business
{                              // buradaki logic de iş kurallarımızı temsil eder
    public class BusinessRules
    {                           //params yazdığımız zaman içerisine istediğimiz kadar kural gönderebiliriz
        public static IResult Run(params IResult[] logics) //Run metodu çalıştır demek
        {
            foreach (var logic in logics)   //bütün kuralları gez
            {
                if (!logic.Success)
                {
                    return logic;  //kurala uymayan varsa o uymayan kuralı döndür
                }
            }

            return null;
        }
    }
}


//Utilities => Bizim için araçlar demek
//Buraya bir metot yazıcaz. Sonra gidip onu Businesstan çağıracağız.
//logic imiz "CheckIfCarNameExist veya CheckIfCarCountOfBrandCorrect" gibi iş kurallarımız olabilir