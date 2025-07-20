using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebColliersCore.Models;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Humanizer;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace WebColliersCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHmNvVJXTf52HE/U/7dB1sPO9LtX/ZHOW+wZ6eKJtMnVkjuSIu" +
"puc3LLrtzM8WexLzPGU2GxQRP0q8Sy7HYkAJNkdlki70n5Uu10lKuBOMf+tOkXbdMfFTjl15noRAlLN+4vOyGFzZYZ" +
"CxZOHBWQAfZpF3aV3bXqgsFRgVare/GYyKW9HTi1LrXL6Aw7AOBkKgV/Ma+bYCfd8sCDeHi8RDNesP0kF/xjHh5MO8" +
"XwouGBg/EkI61hQMPPCzhFYMRoHTSrTxQcnR68jzjvMA0g2iGMHTQAwJGXzaeOvPDQuR12mJmLYn0x7c5QF6dWAnfI" +
"cszqSDtjeJs5X/S/rD8QEq6NicyGzO8z4IcCTnepwZ0logGOUCatuABZb7t/ewJP8lfoOIyf1ZyScTjz/S9Yat4V/W" +
"g1bfZuOBv5KuKB9z1KZYbWcS3aLusCJpnlZ8T3Ek2MRT5HJCBr1h2wOYCMQxck391pOERD6c6NaZJdq9J/A7yiyAbg" +
"IBl7aR2fdll3G6N+tSenE8HVB1F/NzYwY4wemNEHf7/hsxTSA4/3mIJm0A==";

            var jsonString = File.ReadAllText("json.json");
            ConfigJson jsonModel = JsonSerializer.Deserialize<ConfigJson>(jsonString);
            SystemComplementos.configJson = jsonModel;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Old Way
            services.AddMvc();

            //services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = long.MaxValue;
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("VendedorConCategorias",
            //                     policy => policy.RequireClaim("Categorías"));
            //});

            //services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(50);
            });
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.AccessDeniedPath = "/InicioSesion/ErrorAcceso";
            });

            //Necesitamos indicar un provedor de almacenamiento para tempdata
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddControllersWithViews(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();



            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ADMINISTRADORES", policy => policy.RequireRole("ADMIN"));
            //});

            //services.AddHttpContextAccessor();

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            //Subir imagenes
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseHsts();

            //Autorización
            app.UseAuthentication();
            app.UseSession();


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //            name: "default",
            //            template: "{controller=Home}/{action=Index}/{id?}"
            //        );
            //});

            //app.Use(async (context, next) =>
            //{
            //    Thread.CurrentPrincipal = context.User;
            //    await next(context);
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) => {
                if (!context.Request.IsHttps)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("HTTPS required!");
                }
                else
                {
                    await next(context);
                }
            });



        }
    }
}
