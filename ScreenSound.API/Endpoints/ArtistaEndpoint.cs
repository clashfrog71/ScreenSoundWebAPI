using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.NovaPasta;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Runtime.CompilerServices;

namespace ScreenSound.API.Endpoints;

public static class ArtistaEndpoint
{
    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
    {
        return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista artista)
    {
        return new ArtistaResponse(artista.Nome, artista.Bio, artista.Id, artista.FotoPerfil);
    }

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
        app.MapPost("/AdicionarArtista", ([FromBody] ArtistaRequest artistaRequest) =>
        {
            var dal = new DAL<Artista>(new ScreenSoundContext());
            var artista = new Artista(artistaRequest.nome,artistaRequest.bio);
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
