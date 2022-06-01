using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
//bu değerleri kullanarak appsettingsteki TokenOptions kısmını mapliyoruz
//Buraya TokenOption yerine TokenOptions diyoruz çünkü verdiğimiz property lerin her biri birer option. Dto lardan farklı kullanım var, o yönden burası bir helper class