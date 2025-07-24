namespace ScreenSound.API.Response;

public class GeneroResponse
{
    public int Id { get; set; }
    public string? genero { get; set; } = string.Empty;
    public string? descricao { get; set; } = string.Empty;
    public ICollection<ArtistaResponse> artistaResponses { get; set; } = new List<ArtistaResponse>();
}
