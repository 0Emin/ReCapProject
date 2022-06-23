using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}

//Client a girişi yapmaya çalışınca eğer doğru giriş yaparsak api ya dönecek o da CreateToken i çalıştıracak, ilgili claimleri bulacak, orada bir tane jwt üretecek, onları client a geri verecek
