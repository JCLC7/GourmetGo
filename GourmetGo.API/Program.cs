using GourmetGo.API.Controladores;
using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Servicios;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Infrastructure.Contexto;
using GourmetGo.Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25))
    ));

// Inyección de dependencias
builder.Services.AddScoped<IProductosRepository, ProductosRepository>();
builder.Services.AddScoped<IProductosservices, Productoservices>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Configurar Swagger para servir desde la raíz
var app = builder.Build(); 

if (app.Environment.IsDevelopment()) 
{ 
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();
app.Run();




