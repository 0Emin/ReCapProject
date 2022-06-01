using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper  //bizim her şeyi byte formatına getirmemiz gerekiyor. AspNET in anlayacağı hale getirmek için
    {//appsettings te oluşturduğumuz securitykey
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); // simetrik bir key kullandık, bir de asimetrik olanlar var burada
        }
    }
}
