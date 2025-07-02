using ScreenSound.Modelos;
using System.Text.Json.Serialization;
using ScreenSound.Banco;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

app.MapGet("/", () =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    return Results.Ok(dal.Listar());
});
app.MapGet("/artistas/{nome}", (string nome) => {
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artista = dal.RecuperarPor(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
if (artista != null)
    {
        Results.NotFound();
    }
return Results.Ok(artista);
});

app.Run();
