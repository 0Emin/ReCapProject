using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch; //stopwatch bizim timer ımız. Bu metot ne kadar sürecek ?

        public PerformanceAspect(int interval) //interval; CarManager da GetById() metodunun üzerinde verdiğimiz PerformanceAspect attribute umuzun 5 saniyesi
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>(); //
        }


        protected override void OnBefore(IInvocation invocation)  //metodun önünde kronometreyi başlatıyorum
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)   //Metot bitince o ana kadar geçen süreyi hesaplıyorum
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");  // Metot gerçekleşirken interval süresi aşılırsa konsola log olarak yazdır
            }
            _stopwatch.Reset();
        }
    }
}
