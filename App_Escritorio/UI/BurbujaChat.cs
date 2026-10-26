using System.Drawing.Drawing2D;

namespace App_Escritorio.UI;

// Un mensaje del chat: fondo redondeado, a la derecha y con el degradado de acento si lo escribe el usuario
public class BurbujaChat : Label
{
    private const int Radio = 16;

    public bool EsUsuario { get; }

    public bool EsError { get; }

    public BurbujaChat(string texto, bool esUsuario, bool esError = false)
    {
        EsUsuario = esUsuario;
        EsError = esError;

        Text = texto;
        AutoSize = true;
        UseMnemonic = false;
        Padding = new Padding(16, 11, 16, 11);
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

        using (var forma = Dibujo.Redondeado(rect, radio))
        {
            if (EsUsuario)
            {
                using var degradado = Dibujo.Degradado(rect);
                g.FillPath(degradado, forma);
            }
            else
            {
                using var fondo = new SolidBrush(EsError ? Estilo.GastoSuave : Estilo.Hover);
                g.FillPath(fondo, forma);
            }
        }

        var zonaTexto = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical);
        TextRenderer.DrawText(g, Text, Font, zonaTexto, ForeColor, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
    }
}
