using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AuctionZ.Utils;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IRoleService = ApplicationCore.Interfaces.IRoleService;


namespace AuctionZ
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddViewLocalization();

            services.AddDbContext<AuctionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("AuctionZ")));


            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ILotRepository, LotRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ILotsService, LotsService>();
            services.AddScoped<IBidsService, BidsService>();
            services.AddScoped<IUserServices, UsersService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddHostedService<AuctionHostedService>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuctionContext>()
                .AddDefaultTokenProviders();
            

//
//            services.AddScoped(provider => 
//                new Lazy<UserManager<User>>(provider.GetService<UserManager<User>>()));
//            services.AddScoped(provider =>
//                new Lazy<RoleManager<Role>>(provider.GetService<RoleManager<Role>>()));
//            services.AddScoped(provider =>
//                new Lazy<SignInManager<User>>(provider.GetService<SignInManager<User>>()));


            Mapper.Initialize(cfg => cfg.AddProfiles(
                Assembly.GetAssembly(typeof(AuctionContext)),
                Assembly.GetAssembly(typeof(Startup)))); 
       
            // Add memory cache services
            services.AddMemoryCache();

            services.AddCors();
            //Add SignalR
           
            services.AddSignalR();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer initializer)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseSignalR(routes =>
            {
                routes.MapHub<LotHub>("/lotHub");
            });
            //initializer.Seed();
        }
    }
}
