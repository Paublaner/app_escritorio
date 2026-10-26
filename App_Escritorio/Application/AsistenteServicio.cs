using App_Escritorio.Domain.Ports.Driven;
using App_Escritorio.Domain.Ports.Driving;
using FluentResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Application
{
    public class AsistenteServicio (IAsistenteIA asistenteIA, ILogger<AsistenteServicio> logger) : IAsistenteServicio
    {
        private readonly IAsistenteIA _asistenteIA = asistenteIA;
        private readonly ILogger<AsistenteServicio> _logger = logger;

        public async Task<Result<string>> PreguntarAsync(string pregunta, string hilo_id)
        {
            if (string.IsNullOrWhiteSpace(pregunta))
            {
                _logger.LogWarning("La pregunta no puede estar vacía.");
                return Result.Fail<string>("La pregunta no puede estar vacía.");
            }

            try
            {
                return await _asistenteIA.PreguntarAsync(pregunta, hilo_id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la pregunta.");
                return Result.Fail<string>("Error al procesar la pregunta.");
            }
        }
    }
}
