using System.ComponentModel;

namespace App_Escritorio.UI;

// Filas que se muestran en los DataGridView: los modelos guardan ids, aquí van ya los nombres

public class FilaCuenta
{
    [Browsable(false)]
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Tipo { get; init; } = string.Empty;
    public decimal Saldo { get; init; }
}

public class FilaCategoria
{
    [Browsable(false)]
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Tipo { get; init; } = string.Empty;
}

public class FilaMovimiento
{
    [Browsable(false)]
    public int Id { get; init; }
    [Browsable(false)]
    public bool EsGasto { get; init; }
    public DateTime Fecha { get; init; }
    public string Cuenta { get; init; } = string.Empty;
    [DisplayName("Categoría")]
    public string Categoria { get; init; } = string.Empty;
    // Con signo: los gastos se muestran en negativo
    public decimal Importe { get; init; }
    [DisplayName("Descripción")]
    public string Descripcion { get; init; } = string.Empty;
}

public class FilaPresupuesto
{
    [Browsable(false)]
    public int CategoriaId { get; init; }
    [DisplayName("Categoría")]
    public string Categoria { get; init; } = string.Empty;
    [DisplayName("Límite")]
    public decimal Limite { get; init; }
    public decimal Gastado { get; init; }
    public decimal Restante { get; init; }
    [DisplayName("% usado")]
    public decimal Uso { get; init; }
}
