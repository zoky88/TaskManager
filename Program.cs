using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.HelperClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core with SQL Server
builder.Services.AddDbContext<TasksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure static file serving for your Angular app.
// Update the RootPath to match your Angular build output folder.
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist/client-app";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // In Development, enable Swagger and optionally use the Angular CLI server.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
else
{
    // In Production, use the built Angular files.
    app.UseSpaStaticFiles();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

// Configure SPA Middleware to serve the Angular front end
app.UseSpa(spa =>
{
    // The SourcePath is where your Angular project is located.
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        // In development, if you wish to work with the Angular CLI dev server,
        // run "ng serve" in the ClientApp folder and proxy requests:
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
});

app.Run();
