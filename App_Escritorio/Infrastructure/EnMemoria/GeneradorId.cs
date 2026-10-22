namespace App_Escritorio.Infrastructure.EnMemoria;

// TEMPORAL: los adaptadores en memoria se sustituirán por los de EF Core + PostgreSQL.
// El Id tiene setter privado (lo pondrá la base de datos), así que aquí se asigna por reflexión.
internal static class GeneradorId
{
    public static void Asignar(object entidad, int id)
    {
        entidad.GetType().GetProperty("Id")!.SetValue(entidad, id);
    }
}
