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
        {                           //ICoreModule da bunu verecez. Yükleme iþini o yapacak
            services.AddControllers();
            //services.AddSingleton<ICarService, CarManager>(); //bu kodu biz yazdýk, bizim yerimize arka planda carManager ý new liyor. Singleton performansý da artýrýyor.
            //services.AddSingleton<ICarDal, EfCarDal>();   // Katmanlar birbirlerine _carDal gibi yapýlarla baðlý olduðu için (CarManager ICarDal a baðlý çünkü) bu kodu yazdýk.
            //Bunlar bizim için IoC Container yapýsý sunuyor: Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject
            //AOP imkanýyla kodlarýmýzý yazýyor olucaz.

            //services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();  //15. DERS BAÞINDA YAZILDI sonra sildik core da zaten CoreModule kýsmýnda buralarý kurumsallaþtýrmaya baþladýk-----------------

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)   // Bu sistemde Authentication olarak JwtBearer kullanýlacak. Bunu AspNET WebAPI ya diyoruz. 
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
            /* services.AddDependencyResolvers();*/ // Buraya sadece CoreModule ü deðil de baþka modülleri de eklemek istiyorum
            services.AddDependencyResolvers(new ICoreModule[]
            {           //params, array ister koleksiyon olarak yap, biz yukarda [] array yaptýk mesela
                new CoreModule()                                    //Buraya istediðimiz kadar Module ekleyebiliriz (injectionlarý için(?) )
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Middleware; Sýrasýyla devreye sokuyoruz aþaðý kýsma yazdýklarýmýzý
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); // Bu satýrý biz yazdýk.

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
