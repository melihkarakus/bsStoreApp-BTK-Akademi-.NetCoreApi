using bsStoreApp.Extensions;
using bsStoreApp.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//assembly reference vermezsek presentation controller çalýþmaz.
builder.Services.AddControllers().AddApplicationPart(typeof(bsStoreApp.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extensions klasörü tanýmlama yapýldý buraya geçildi.
builder.Services.ConfigureSqlContext(builder.Configuration);

//Repository birbirlerine anlatmak için yazýlan kod Extensions klasörüne bak.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();




var app = builder.Build();

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
