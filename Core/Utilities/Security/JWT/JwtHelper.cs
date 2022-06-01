using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {//microsoft.extension... (IConfiguration çözümü)
        //IConfiguration appsettingsteki değerleri okumamıza yarıyor
        //Okuduğum değerleri TokenOptions diye bir nesneye atıcam
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {//Configuration olan kısımları görünce aklıma appsettings gelmeli
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //appsettings teki TokenOptions kısmını al ve onu " TokenOptions " sınıfın değerlerini kullanarak maple
        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //Tokenimize şu andan itibaren 10 dk ekliyoruz. 10 dk appsettings e yazdığımız değer (AccessTokenExpiration)
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //güvenlik anahtarına ihtiyacı var; SecurityKeyHelper yazdık onun da CreateSecurityKey i var, tokenOption Security Key i kullanarak onu oluşturabilirsin diyoz  
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //hangi güvenlik algoritmasını kullanayım, anahtar nedir? SigningCredentialHelper diye bir şey yazdık, orada da onlar var diyoz
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); //TokenOption ları kullanarak ilgili kullanıcı için ilgili credential leri kullanarak kulanıcıya atanacak claim leri (yetki) bir metod (CreateJwtSecurityToken). Bunu da en aşağıda private metodla yapmışız
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims), //Claim ler bizim için önemli
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");             //2 stringi yanyana göstermek için yazlmış yapı. Başına dollar eklediğimiz için içerisine kod yazabiliyoruz
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());  // Roller ekliyoruz. OperationClaim deki kullanıcının veritabanından çektiğim rollerini, onların name lerini çekip array e basıp rolleri ekliyoruz   

            return claims;
        }
    }
}

//normalde claims in yukarıdaki gibi metodları yok. ClaimExtensions kısmında metod ekliyor olacağız. *BİLGİ*: Bir extension metodu yazabilmek için hem classın metod un static olması gerekmektedir
//Extension Method