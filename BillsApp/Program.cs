
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BillsConn");

// Add services to the container.
var today = DateTime.Today.Date;

Log.Logger = new LoggerConfiguration()
                .WriteTo.File(today.Year + "_" + today.Month + "_" + today.Day + "_Log.txt")
                .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<BillsDbContext>(options =>
{
    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
},
                    ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                );
builder.Services.AddTransient(typeof(IBillsDbContextFactory), typeof(BillsDbContextFactory));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddApplicationServices();
builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();

var app = builder.Build();

app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true));// Allow any origin  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<BillsDbContext>();
    dataContext.Database.Migrate();
}


app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");


app.Run();
