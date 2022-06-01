using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{//Yetki kontrolü yapacağız, business ta if li kodlar yazıp orayı doldurmayalım diye buraya yazıp oradan çağırırız. 
    //SecuredOperation yani bu kısım JWT için
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles) // bana rolleri ver diyor (claim)
        {
            _roles = roles.Split(','); // bir metni belirttiğin karaktere göre ayırıp array e atıyor (roles.Split)
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();  // Consructor olmadan direk injection yapabilmek için kullandığımız bir yer
            //Biz API da injection yaptık. IoC kullanıyoruz güzel fakat bu bir windows form uygulaması olsayı onda nasıl kullanacaktık? o dependency leri yakalayabilmek için " Service Tool " yazmışız
            //Injection altyapısını aynen okuyabilmemize yarayan bir araç olacak
        }
        //OnBefore MethodInterception dan gelior
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}

//bir üst kısımda yetkisi var mı diye rolleri dolaşıyoruz, metodun başında(OnBefore), eğer yoksa 'yetkin yok' hatası ver diyoruz