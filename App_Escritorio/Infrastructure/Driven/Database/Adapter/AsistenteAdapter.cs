using App_Escritorio.Domain.Ports.Driven;
using FluentResults;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;


namespace App_Escritorio.Infrastructure.Driven.Database.Adapter;


public class AsistenteAdapter (HttpClient http,ILogger<AsistenteAdapter> logger) : IAsistenteIA
{
    private record RespuestaChat(string Respuesta);
    private readonly HttpClient asistente = http;

    private readonly ILogger<AsistenteAdapter> _logger = logger;

    public async Task<Result<string>> PreguntarAsync(string pregunta, string hilo_id)
    {
        try
        {
            var response = await asistente.PostAsJsonAsync("chat", new { pregunta, hilo_id });

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"El asistente respondió con un error ({(int)response.StatusCode}).");
                return Result.Fail($"El asistente respondió con un error ({(int)response.StatusCode}).");
            }

            var cuerpo = await response.Content.ReadFromJsonAsync<RespuestaChat>();

            if (cuerpo == null)
            {
                _logger.LogError("La respuesta del asistente es nula.");
                return Result.Fail("La respuesta del asistente es nula.");
            }

            return Result.Ok(cuerpo.Respuesta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción al comunicarse con el asistente.");
            return Result.Fail($"Excepción al comunicarse con el asistente: {ex.Message}");
        }

    }
}
