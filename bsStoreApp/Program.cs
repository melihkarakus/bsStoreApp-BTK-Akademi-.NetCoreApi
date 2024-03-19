using bsStoreApp.Extensions;
using bsStoreApp.Presentation.ActionFilters;
using bsStoreApp.Repositories.EFCore;
using bsStoreApp.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);
//NLog nlog.config tanýmlattýrdýk.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
//assembly reference vermezsek presentation controller çalýþmaz.
builder.Services.AddControllers(config =>
{
    //Ýçerik Pazarlýðý Konfigrasyon
    config.RespectBrowserAcceptHeader = true; //Ýçerik pazarlýðýný yapmamýz için gereken konfigrasyon
    config.ReturnHttpNotAcceptable = true; //Api gönderilen isteðin bu içerikle pazarlama yapýlmadýðýný belirtir.
})
    .AddXmlDataContractSerializerFormatters() //XML Formatýnda dönmesi için
    .AddApplicationPart(typeof(bsStoreApp.Presentation.AssemblyReference).Assembly);



// Hata davranýþýný devre dýþý býrakmak için
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extensions klasörü tanýmlama yapýldý buraya geçildi.
builder.Services.ConfigureSqlContext(builder.Configuration);

//Repository birbirlerine anlatmak için yazýlan kod Extensions klasörüne bak.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();

//Automapper Konfigrasyonu
builder.Services.AddAutoMapper(typeof(Program));

//Action Filters Konfigrasyonu Extentions Klasörüne tanýmlandý.
builder.Services.ConfigureActionFilters();

//Page Konfigrasyon iþlemi
builder.Services.ConfigureCors();

//DataShaper Konfigrasyon iþlemi
builder.Services.ConfigureDataShapper();

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

app.UseCors("corsPolicy");//Policy Page Konfigrasyonun içinde orasýda Api->Extensions

app.UseAuthorization();

app.MapControllers();

app.Run();
