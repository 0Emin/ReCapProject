using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction  // A hesabından B hesabına para yatırılırken A hesabından para çıkışı olup B hesabına para geçmediyse bu durumu Trasaction la yönetebiliriz
{
    public class TransactionScopeAspect : MethodInterception
    {                            
        public override void Intercept(IInvocation invocation) //Intercept demek bu bloğu çalıştır demek o metod yerine: invocation.Proceed()
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed(); 
                    transactionScope.Complete();
                }
                catch (System.Exception e)                  //using li kısım bizim şablonumuz - 25. satıra kadar -
                {                                              
                    transactionScope.Dispose();            //unitofwork kullanmıyoz, çünkü unitofwork doğru bir implementasyon değil. Çünkü saveChanges() unitofwork ün kendisi. Mesela entity frameworkteki savechanges dedi Engin Bey
                    throw;                        
                }                                         //Bu olaylar veri tabanında oluyor. Fakat transaction kısmı tamamlanmadan kayda geçmiyor. İlk önce bellekte tutuluyor, işlemde sıkıntı çıkarsa kayda alınmıyor
            }
        }
    }
}
