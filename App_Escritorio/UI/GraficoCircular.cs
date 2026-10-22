namespace App_Escritorio.UI;

// Gráfico de anillo dibujado con GDI+ (WinForms en .NET no trae controles de gráficos)
public class GraficoCircular : Control
{
    private static readonly Color[] Colores =
    {
        Color.FromArgb(37, 99, 235),
        Color.FromArgb(245, 158, 11),
        Color.FromArgb(20, 184, 166),
        Color.FromArgb(139, 92, 246),
        Color.FromArgb(236, 72, 153),
        Color.FromArgb(148, 163, 184)
    };

    private const int AltoFilaLeyenda = 26;

    private List<(string Nombre, decimal Valor)> _datos = new();

    [System.ComponentModel.DefaultValue("Sin datos")]
    public string TextoVacio { get; set; } = "Sin datos";

    public GraficoCircular()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.White;
    }

    public void MostrarDatos(IEnumerable<(string Nombre, decimal Valor)> datos)
    {
        _datos = datos.Where(d => d.Valor > 0).ToList();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        float escala = DeviceDpi / 96f;
        int margen = (int)(16 * escala);
        int altoFila = (int)(AltoFilaLeyenda * escala);
        int altoLeyenda = Math.Max(_datos.Count, 1) * altoFila;
        int diametro = Math.Min(Width - 2 * margen, Height - altoLeyenda - 3 * margen);

        if (diametro < 40)
        {
            return;
        }

        float grosor = diametro * 0.2f;
        var circulo = new RectangleF((Width - diametro) / 2f, margen, diametro, diametro);

        // El lápiz se dibuja centrado en la línea, así que el anillo va medio grosor hacia dentro
        var anillo = RectangleF.Inflate(circulo, -grosor / 2, -grosor / 2);

        decimal total = _datos.Sum(d => d.Valor);

        if (total == 0)
        {
            using var lapizVacio = new Pen(Estilo.Borde, grosor);
            g.DrawEllipse(lapizVacio, anillo);
            DibujarCentro(g, circulo, TextoVacio, null);
            return;
        }

        float inicio = -90;

        for (int i = 0; i < _datos.Count; i++)
        {
            float barrido = (float)(_datos[i].Valor / total) * 360f;

            // Pequeña separación blanca entre porciones si hay más de una
            float hueco = _datos.Count > 1 && barrido > 3 ? 1.5f : 0;

            using var lapiz = new Pen(Colores[i % Colores.Length], grosor);
            g.DrawArc(lapiz, anillo, inicio + hueco / 2, barrido - hueco);

            inicio += barrido;
        }

        DibujarCentro(g, circulo, "Total", total.ToString("C2"));
        DibujarLeyenda(g, (int)circulo.Bottom + margen, margen, altoFila, total);
    }

    private void DibujarCentro(Graphics g, RectangleF circulo, string titulo, string? valor)
    {
        var centro = Rectangle.Round(circulo);

        if (valor is null)
        {
            TextRenderer.DrawText(g, titulo, Font, centro, Estilo.TextoSecundario,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            return;
        }

        using var fuenteValor = new Font(Font.FontFamily, Font.Size * 1.3f, FontStyle.Bold);
        int altoTitulo = TextRenderer.MeasureText(titulo, Font).Height;
        int altoValor = TextRenderer.MeasureText(valor, fuenteValor).Height;
        int y = centro.Top + (centro.Height - altoTitulo - altoValor) / 2;

        TextRenderer.DrawText(g, titulo, Font, new Rectangle(centro.Left, y, centro.Width, altoTitulo),
            Estilo.TextoSecundario, TextFormatFlags.HorizontalCenter);
        TextRenderer.DrawText(g, valor, fuenteValor, new Rectangle(centro.Left, y + altoTitulo, centro.Width, altoValor),
            ForeColor, TextFormatFlags.HorizontalCenter);
    }

    private void DibujarLeyenda(Graphics g, int y, int margen, int altoFila, decimal total)
    {
        int lado = altoFila / 2;

        for (int i = 0; i < _datos.Count; i++)
        {
            var fila = new Rectangle(margen, y + i * altoFila, Width - 2 * margen, altoFila);

            using var pincel = new SolidBrush(Colores[i % Colores.Length]);
            g.FillEllipse(pincel, fila.Left, fila.Top + (altoFila - lado) / 2, lado, lado);

            string importe = $"{_datos[i].Valor:C2}  ({_datos[i].Valor / total:P0})";
            int anchoImporte = TextRenderer.MeasureText(importe, Font).Width;

            var zonaNombre = new Rectangle(fila.Left + lado + 8, fila.Top, fila.Width - lado - 8 - anchoImporte, fila.Height);
            TextRenderer.DrawText(g, _datos[i].Nombre, Font, zonaNombre, ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(g, importe, Font, fila, Estilo.TextoSecundario,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
