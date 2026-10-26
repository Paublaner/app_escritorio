using System.Drawing.Drawing2D;

namespace App_Escritorio.UI;

// Un mensaje del chat: fondo redondeado con el color del tema, a la derecha si lo escribe el usuario
public class BurbujaChat : Label
{
    private const int Radio = 14;

    public bool EsUsuario { get; }

    public bool EsError { get; }

    public BurbujaChat(string texto, bool esUsuario, bool esError = false)
    {
        EsUsuario = esUsuario;
        EsError = esError;

        Text = texto;
        AutoSize = true;
        UseMnemonic = false;
        Padding = new Padding(14, 10, 14, 10);
        Margin = Padding.Empty;
        Font = new Font("Segoe UI", 10.5F);

        // El fondo lo pinta OnPaint; el Label solo tiene que dejar ver el del panel
        SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);

        ActualizarTema();
    }

    public void ActualizarTema()
    {
        BackColor = Color.Transparent;
        ForeColor = EsUsuario ? Color.White : EsError ? Estilo.Gasto : Estilo.Texto;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        float radio = Radio * DeviceDpi / 96f;
        var rect = new RectangleF(0, 0, Width - 1, Height - 1);

        using (var forma = Redondeado(rect, radio))
        using (var fondo = new SolidBrush(EsUsuario ? Estilo.Acento : Estilo.Superficie))
        {
            g.FillPath(fondo, forma);

            if (!EsUsuario)
            {
                using var borde = new Pen(EsError ? Estilo.Gasto : Estilo.Borde);
                g.DrawPath(borde, forma);
            }
        }

        var zonaTexto = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical);
        TextRenderer.DrawText(g, Text, Font, zonaTexto, ForeColor, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
    }

    private static GraphicsPath Redondeado(RectangleF r, float radio)
    {
        float d = radio * 2;
        var path = new GraphicsPath();

        path.AddArc(r.Left, r.Top, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        return path;
    }
}
