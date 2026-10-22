using App_Escritorio.Domain.Model;

namespace App_Escritorio.UI;

public static class Textos
{
    public static string Tipo(Cuenta.TipoCuenta tipo) => tipo switch
    {
        Cuenta.TipoCuenta.Banco => "Banco",
        Cuenta.TipoCuenta.Efectivo => "Efectivo",
        Cuenta.TipoCuenta.Ahorro => "Ahorro",
        Cuenta.TipoCuenta.Credito => "Crédito",
        _ => tipo.ToString()
    };

    public static string Tipo(Categoria.TipoMovimiento tipo) => tipo switch
    {
        Categoria.TipoMovimiento.Ingreso => "Ingreso",
        Categoria.TipoMovimiento.Gasto => "Gasto",
        _ => tipo.ToString()
    };

    public static string Mes(DateTime fecha)
    {
        string texto = fecha.ToString("MMMM yyyy");
        return char.ToUpper(texto[0]) + texto[1..];
    }
}
