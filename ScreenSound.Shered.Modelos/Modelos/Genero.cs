using ScreenSound.Modelos;

namespace ScreenSound.API.Response;

public class Genero
{
    public int Id { get; set; }
    public string? genero { get; set; } = string.Empty;
    public string? descricao { get; set; } = string.Empty;
    public ICollection<Artista>? artistas { get; set; } = new List<Artista>();
    public override string ToString()
    {
        return @$"Id: {Id}
        Genero: {genero}
        Descricao: {descricao}";
    }
}
