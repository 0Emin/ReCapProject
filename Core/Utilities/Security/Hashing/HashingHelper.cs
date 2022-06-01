using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {//using tab tab  //hmac:Kriptografi sınıfnda kullandığımız classa karşılık geliyor
         //SHA 512
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // Buradaki " key " yukarıda kullandığımız SHA 512 algoritmasının o an oluşturduğu key değeridir. Her kullanıcı için farklı bir key oluşturur yani.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//password un byte değerini almamız gerektiği için parantez içerisine byte karşılığını almamızı sağlayacak kodları yazdık 
            }//Verdiğimiz bir password değerinin salt ve hash değerlerini oluşturuyoruz bu kısımda
        }

        //burada out kullanmıyoruz, çünkü bu değerleri biz vereceğiz.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {//Buradaki password (yukarıdaki) kullanıcının sisteme tekrardan girmeye çalışırkenki verdiği parola
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            
        }//VerifyPasswordHash: Salt değerle hash değerin eşleşip eşleşmediğini kontrol ettiğimiz yer. Yani sonradan sisteme girmek isteyen birisi için bu kısım
    }
}
