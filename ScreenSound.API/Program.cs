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
    artista.Nome =  artistaExistente.Nome;
    dal.Atualizar(artista);
    return Results.Ok();
});
app.MapPatch("/AdicionarMusica/{idArtista}", (int idArtista, [FromBody] Musica musica) =>
{
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artista = dal.RecuperarPor(a => a.Id == idArtista);
    if (artista == null)
    {
        return Results.NotFound();
    }
    artista.AdicionarMusica(musica);
    dal.Atualizar(artista);
    return Results.Ok();
});
app.MapGet("/musicas", () => {
    var dal = new DAL<Musica>(new ScreenSoundContext());
    return Results.Ok(dal.Listar());
});
app.MapGet("/musicas/{nome}", (string nome) => {
    var dal = new DAL<Musica>(new ScreenSoundContext());
    var musica = dal.RecuperarPor(m => m.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    if (musica == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(musica);
});
app.MapPut("/AtualizarMusica", ([FromBody] Musica musica) =>
{
    var dal = new DAL<Musica>(new ScreenSoundContext());
    var musicaExistente = dal.RecuperarPor(m => m.Id == musica.Id);
    if (musicaExistente == null)
    {
        return Results.NotFound();
    }
    dal.Atualizar(musica);
    return Results.Ok();
});
app.MapDelete("/DeletarMusica/{nome}", (string nome) =>
{
    var dal = new DAL<Musica>(new ScreenSoundContext());
    var musica = dal.RecuperarPor(m => m.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    if (musica == null)
    {
        return Results.NotFound();
    }
    dal.Deletar(musica);
    return Results.Ok();
});
app.Run();
