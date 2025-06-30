using ScreenSound.Modelos;
using System.Text.Json.Serialization;
using ScreenSound.Banco;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

app.MapGet("/", () => {
    var dal = new DAL<Artista>(new ScreenSoundContext());
    dal.Listar();

});

app.Run();
