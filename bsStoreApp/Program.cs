using bsStoreApp.Extensions;
using bsStoreApp.Repositories.EFCore;
using bsStoreApp.Services.Contract;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);
//NLog nlog.config tanýmlattýrdýk.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

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
builder.Services.ConfigureLoggerService();



var app = builder.Build();

//ExceptionsMiddlewareExtensions sýnýfýna tanýmlanan configure burada bu þekilde geçmelisin çünkü mimari oluþtuktan sonra bu kod çalýþacaktýr.
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Productions için gerekli kod eklendi.
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
