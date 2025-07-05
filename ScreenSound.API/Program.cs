using ScreenSound.Modelos;
using System.Text.Json.Serialization;
using ScreenSound.Banco;
using Microsoft.AspNetCore.Mvc;

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
app.MapPost("/AdicionarArtista", ([FromBody] Artista artista) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    dal.Adicionar(artista);
    return Results.Ok();
});
app.MapDelete("/DeletarArtista", (int id) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artista = dal.RecuperarPor(a => a.Id == id);
    if (artista == null)
    {
        return Results.NotFound();
    }
    dal.Deletar(artista);
    return Results.Ok();

});
app.MapPut("/AtualizarArtista", ([FromBody] Artista artista) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artistaExistente = dal.RecuperarPor(a => a.Id == artista.Id);
    if (artistaExistente == null)
    {
        return Results.NotFound();
    }
    dal.Atualizar(artista);
    return Results.Ok();
});

app.Run();
