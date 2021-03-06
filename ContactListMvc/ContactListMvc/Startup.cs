using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ContactList.Infrastructure.Database;
using ContactList.Abstractions;
using ContactList.Services;
using ContactList.Infrastructure.Database.Repositories;
using ContactList.Infrastructure.Api.Repositories;
using ContactList.Configuration;
using Microsoft.Extensions.Options;
using ContactListMvc.Configuration;
using ContactList.Infrastructure.InMemoryDatabase;

namespace ContactListMvc
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
            InMemoryDatabase memoryDb = new InMemoryDatabase(InMemoryDatabase.CreateSeedData());

            services.Configure<ContactListApiOptions>(Configuration.GetSection("ContactListApi"));
            services.Configure<CompanySettings>(Configuration.GetSection("DevelopmentCompany"));

            services.AddSingleton(memoryDb);
            services.AddSingleton(
                serviceProvider => serviceProvider.GetService<IOptions<ContactListApiOptions>>().Value);

            services.AddControllersWithViews();

            services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));

            services.AddTransient<IContactListEntryService, ContactListEntryService>();
            //services.AddTransient<IContactListEntryRepository, ContactListEntryDbRepository>();
            // services.AddTransient<IContactListEntryRepository, ContactListEntryApiRepository>();
            services.AddTransient<IContactListEntryRepository, ContactListEntryInMemoryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
