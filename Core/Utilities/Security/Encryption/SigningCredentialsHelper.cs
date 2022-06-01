using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {// bu kısımda hangi anahtar ve hangi algoritmanın kullanılacağını belirtiyoruz, AspNET yani webapi nin de ihtiyacını gidermek için
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}

//HashingHelper hash oluşturmaya ve onu doğrulamaya yarıyor.
//Hash oluştururken hangi algoritmayı kullanacağımızı söylüyoruz.
//Doğrularken de yine aynı algoritmayı ama daha önce onu oluştururken kullandığımız tuzu kullanarak onu doğruluyoruz.

//SecurityKeyHelper appsettings içerisine yazdığımız SecurityKey' i byte array haline getirmeye yarıyor.Simetrik anahtar haline getirmeye yarıyor

//SigningCredentialsHelper da (burada) ise sisteme JWT sistemini yöneteceğini ve güvenlik anahtarı ile şifreleme algoritmasının ne olduğunu söylüyoruz.