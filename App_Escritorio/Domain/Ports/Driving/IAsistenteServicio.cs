using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Domain.Ports.Driving;

public interface IAsistenteServicio
{
    Task<Result<string>> PreguntarAsync(string pregunta, string hilo_id);
}
