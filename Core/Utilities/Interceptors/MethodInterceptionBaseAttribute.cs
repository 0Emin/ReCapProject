using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {            //Hangi attribute önce çalışssın ( Priority)
        public int Priority { get; set; }
        //invocation bizim metodumuz. GetAll, Get... gibi
        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
