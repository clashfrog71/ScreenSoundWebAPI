using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Runtime.CompilerServices;

namespace ScreenSound.API.Endpoints;

public static class ArtistaEndpoint
{
    public static void Artista(this WebApplication app)
    {
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
            artista.Nome = artistaExistente.Nome;
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
    }
}
