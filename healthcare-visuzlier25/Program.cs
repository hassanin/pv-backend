using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<healthcare_visuzlier25.Models.healthcareContext>(
                   options => options.UseNpgsql("Host=localhost;Database=healthcare;Username=mhassanin;Password=magical_password"));
builder.Services.AddCors();
//builder.Configuration.AddJsonFile()
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(configurePolicy =>
//{
//    // Local dev server for React
//    configurePolicy.WithOrigins(new[] { "https://localhost:3000", "http://localhost:3000" });
//    configurePolicy.AllowAnyOrigin();
//});

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "my-app", "build")),
    RequestPath = "/static"
});

app.Run();
