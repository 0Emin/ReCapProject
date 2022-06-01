using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        {                           //ICoreModule da bunu verecez. Y�kleme i�ini o yapacak
            services.AddControllers();
            //services.AddSingleton<ICarService, CarManager>(); //bu kodu biz yazd�k, bizim yerimize arka planda carManager � new liyor. Singleton performans� da art�r�yor.
            //services.AddSingleton<ICarDal, EfCarDal>();   // Katmanlar birbirlerine _carDal gibi yap�larla ba�l� oldu�u i�in (CarManager ICarDal a ba�l� ��nk�) bu kodu yazd�k.
            //Bunlar bizim i�in IoC Container yap�s� sunuyor: Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject
            //AOP imkan�yla kodlar�m�z� yaz�yor olucaz.

            //services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();  //15. DERS BA�INDA YAZILDI sonra sildik core da zaten CoreModule k�sm�nda buralar� kurumsalla�t�rmaya ba�lad�k-----------------

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)   // Bu sistemde Authentication olarak JwtBearer kullan�lacak. Bunu AspNET WebAPI ya diyoruz. 
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            /* services.AddDependencyResolvers();*/ // Buraya sadece CoreModule � de�il de ba�ka mod�lleri de eklemek istiyorum
            services.AddDependencyResolvers(new ICoreModule[]
            {           //params, array ister koleksiyon olarak yap, biz yukarda [] array yapt�k mesela
                new CoreModule()                                    //Buraya istedi�imiz kadar Module ekleyebiliriz (injectionlar� i�in(?) )
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Middleware; S�ras�yla devreye sokuyoruz a�a�� k�sma yazd�klar�m�z�
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Bu sat�r� biz yazd�k.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
