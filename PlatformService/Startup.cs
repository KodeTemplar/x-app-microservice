using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
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

            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            Console.WriteLine("--> Using MSSQL Server db");

#if DEBUG
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Development.json")
                    .Build();
#else
            var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.Production.json")
                   .Build();
#endif

            var conn = configuration.GetConnectionString("PlatformsConn");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(conn,
              b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            Console.WriteLine($"--> Command Service Endpoint: {configuration["CommandService"]}{configuration["CommandServiceInboundEndpoint"]}");
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                PredDb.PrepPopulation(app);
            }

            PredDb.PrepPopulation(app);
            //app.UseHttpsRedirection();

            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
