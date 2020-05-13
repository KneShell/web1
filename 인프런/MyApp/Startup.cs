using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Data;
using MyApp.Data.Repositories;
using MyApp.Models;

namespace MyApp
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyAppContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("MyAppConnection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<MyAppContext>();


            services.AddTransient<DbSeeder>();
            // 트랜지언트 -> 한번쓰고 버리는 휘발성 인스턴스 생성
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            // 스코프 -> 생성할때마다 새 인스턴스를 만듬
            //services.AddSingleton
            // 싱글톤 -> 어플리케이션의 생명주기 동안 단 한번 인스턴스 생성
            // HTTP 요청이 있을떄마다 똑같은 인스턴스를 쓰게함
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            //  인증을 활성화시키는 메서드, 즉 UseAuthentication는 반드시 UseMvc보다 먼저 선언해야 작동
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });

            seeder.seedDatabase().Wait();
        }
    }
}
