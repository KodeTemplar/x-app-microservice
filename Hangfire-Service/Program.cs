using EmailServices;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire_Service.Service;
using Schehuler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(x => x.UseRecommendedSerializerSettings().UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISendEmailService, SendEmailService>();

var app = builder.Build();

var optionsStorage = new Hangfire.SqlServer.SqlServerStorageOptions
{
    CommandBatchMaxTimeout = TimeSpan.FromMinutes(10),
    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    QueuePollInterval = TimeSpan.FromMinutes(5),
    UseRecommendedIsolationLevel = true,
    UsePageLocksOnDequeue = true,
    DisableGlobalLocks = true
};

//Hangfire Configure Starts
app.UseHangfireDashboard("/Hangfire", new DashboardOptions
{
    DashboardTitle = "Hangfire Service Test",
    Authorization = new[] { new  CustomAuthorizationFilter()
        },
});

HangFireService.InitializeService();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class CustomAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        HttpContext httpContext = context.GetHttpContext();


        // Allow all authenticated users to see the Dashboard (potentially dangerous). //Use this when you have admin login so that him alone can access the hangfire.
        //return httpContext.User.Identity.IsAuthenticated;

        // Only admin users can access the dashboard
        //var user = new OwinContext(owinEnvironment).Authentication.User;
        //return user.Identity.IsAuthenticated && user.IsInRole(RoleKeys.Admin);

        return true;
    }
}