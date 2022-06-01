using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)                //UserDal a enjekte ediyor
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {                                                   //UserDal dan claim leri çekiyor
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);                             //Kullanıcı ekliyor
        }

        public User GetByMail(string email)
        {                                                   //Emaile göre kullanıcı getiriyor 
            return _userDal.Get(u => u.Email == email);
        }
    }
}
