using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);  //claim leri getir
        void Add(User user);                       //Kullanıcı ekle
        User GetByMail(string email);             //Email e göre kullanıcı getir       
    }
}
