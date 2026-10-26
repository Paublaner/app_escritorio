using System.Drawing.Drawing2D;

namespace App_Escritorio.UI;

// Utilidades de GDI+ compartidas por los controles que se dibujan a mano
public static class Dibujo
{
    // Iconos de Windows: Segoe Fluent Icons en Windows 11 y Segoe MDL2 Assets en Windows 10 (mismos códigos)
    public const string IconoResumen = "";
    public const string IconoCuentas = "";
    public const string IconoMovimientos = "";
    public const string IconoCategorias = "";
    public const string IconoPresupuestos = "";
    public const string IconoAsistente = "";
    public const string IconoLuna = "";
    public const string IconoSol = "";
    public const string IconoSaldo = "";
    public const string IconoIngreso = "";
    public const string IconoGasto = "";
    public const string IconoEnviar = "";
    public const string IconoNuevo = "";
    public const string IconoEliminar = "";
    public const string IconoEditar = "";
    public const string IconoGuardar = "";

    private static readonly string FamiliaIconos = ElegirFamiliaIconos();

    public static Font FuenteIconos(float tamano) => new(FamiliaIconos, tamano);

    public static GraphicsPath Redondeado(RectangleF r, float radio)
    {
        var path = new GraphicsPath();
        float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height));

        if (d <= 0)
        {
            path.AddRectangle(r);
            return path;
        }

        path.AddArc(r.Left, r.Top, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        return path;
    }

    // Degradado diagonal del color de acento, el de los botones principales y la tarjeta destacada
    public static LinearGradientBrush Degradado(RectangleF r, bool encima = false)
    {
        var inicio = encima ? ControlPaint.Light(Estilo.AcentoInicio, 0.15f) : Estilo.AcentoInicio;
        var fin = encima ? ControlPaint.Light(Estilo.AcentoFin, 0.15f) : Estilo.AcentoFin;

        // Un rectángulo vacío hace fallar al pincel
        var zona = new RectangleF(r.X, r.Y, Math.Max(r.Width, 1), Math.Max(r.Height, 1));
        return new LinearGradientBrush(zona, inicio, fin, LinearGradientMode.ForwardDiagonal);
    }

    // Sombra suave: varias capas casi transparentes que se van ensanchando
    public static void Sombra(Graphics g, RectangleF r, float radio, int capas)
    {
        int alfa = Tema.Oscuro ? 28 : 10;

        for (int i = capas; i >= 1; i--)
        {
            var capa = RectangleF.Inflate(r, i * 0.6f, i * 0.6f);
            capa.Offset(0, i * 0.5f);

            using var forma = Redondeado(capa, radio + i);
            using var pincel = new SolidBrush(Color.FromArgb(alfa, 15, 23, 42));
            g.FillPath(pincel, forma);
        }
    }

    public static void Icono(Graphics g, string icono, float tamano, Rectangle zona, Color color)
    {
        using var fuente = FuenteIconos(tamano);
        TextRenderer.DrawText(g, icono, fuente, zona, color,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
    }

    private static string ElegirFamiliaIconos()
    {
        foreach (var nombre in new[] { "Segoe Fluent Icons", "Segoe MDL2 Assets" })
        {
            // Si la fuente no está instalada, Windows da otra en su lugar sin avisar
            using var prueba = new Font(nombre, 10);

            if (prueba.Name == nombre)
            {
                return nombre;
            }
        }

        return "Segoe UI Symbol";
    }
}
