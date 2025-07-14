using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;
namespace ScreenSound.API.Endpoints;
public static class MusicaEndpoint
{
    public static void Musica(this WebApplication app)
    {
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
        app.MapPut("/AtualizarMusica", ([FromBody] MusicaRequest musicaRequest) =>
        {
            var dal = new DAL<Musica>(new ScreenSoundContext());
            var musicaExistente = dal.RecuperarPor(m => m.Id == musicaRequest.Id);
            if (musicaExistente == null)
            {
                return Results.NotFound();
            }
            musicaExistente.Nome = musicaRequest.Nome;
            musicaExistente.AnoLancamento = musicaRequest.AnoLancamento;
            dal.Atualizar(musicaExistente);
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
    }
}