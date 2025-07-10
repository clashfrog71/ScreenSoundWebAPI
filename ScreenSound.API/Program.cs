using ScreenSound.Modelos;
using System.Text.Json.Serialization;
using ScreenSound.Banco;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

ArtistaEndpoint.Artista(app);
MusicaEndpoint.Musica(app);
app.Run();