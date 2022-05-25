using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ICarService, CarManager>(); //bu kodu biz yazd�k, bizim yerimize arka planda carManager � new liyor. Singleton performans� da art�r�yor.
            services.AddSingleton<ICarDal, EfCarDal>();   // Katmanlar birbirlerine _carDal gibi yap�larla ba�l� oldu�u i�in (CarManager ICarDal a ba�l� ��nk�) bu kodu yazd�k.
            //Bunlar bizim i�in IoC Container yap�s� sunuyor: Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject
        }   //AOP imkan�yla kodlar�m�z� yaz�yor olucaz.

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
