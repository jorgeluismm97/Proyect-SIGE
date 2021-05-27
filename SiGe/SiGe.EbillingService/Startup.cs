using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGe.EbillingService
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
            services.AddScoped<IIdentityDocumentTypeRepository, IdentityDocumentTypeRepository>();
            services.AddScoped<IIdentityDocumentTypeService, IdentityDocumentTypeService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonCompanyService, PersonCompanyService>();
            services.AddScoped<IPersonCompanyRepository, PersonCompanyRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPaymentPlanRepository, PaymentPlanRepository>();
            services.AddScoped<IPaymentPlanService, PaymentPlanService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentOperationRepository, PaymentOperationRepository>();
            services.AddScoped<IPaymentOperationService, PaymentOperationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBranchOfficeService, BranchOfficeService>();
            services.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();

            services.AddScoped<ISecurityCommandText, SecurityCommandText>();
            services.AddScoped<IMainCommandText, MainCommandText>();
            services.AddScoped<IPaymentCommandText, PaymentCommandText>();
            services.AddScoped<IProductCommandText, ProductCommandText> ();
            services.AddMvc();
            services.AddSession();
            services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", config =>
            {
                config.Cookie.Name = "UserLoginCookie";
                config.LoginPath = "/Account/Login";
            });
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
