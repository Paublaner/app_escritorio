using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Escritorio.Domain.Ports.Driven;

public interface IAsistenteIA
{
    Task<Result<string>> PreguntarAsync(string pregunta, string hilo_id);

}
