using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace App_Escritorio.UI;

// Icono de Windows sobre un cuadrado redondeado del color de su rol (ingreso, gasto, acento...)
public class IconoCirculo : Control
{
    [DefaultValue("")]
    public string Icono { get; set; } = string.Empty;

    [DefaultValue(Rol.Primario)]
    public Rol Rol { get; set; } = Rol.Primario;

    public IconoCirculo()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
            | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

        // Transparente: se ve la tarjeta de debajo, también cuando es la del degradado
        BackColor = Color.Transparent;
        Size = new Size(44, 44);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var (fondo, color) = Rol switch
        {
            Rol.Ingreso => (Estilo.IngresoSuave, Estilo.Ingreso),
            Rol.Gasto => (Estilo.GastoSuave, Estilo.Gasto),
            Rol.Destacado => (Color.FromArgb(48, Color.White), Color.White),
            _ => (Estilo.AcentoSuave, Estilo.Acento)
        };

        var rect = new RectangleF(0, 0, Width - 1, Height - 1);

        using (var forma = Dibujo.Redondeado(rect, Math.Min(Width, Height) * 0.3f))
        using (var pincel = new SolidBrush(fondo))
        {
            g.FillPath(pincel, forma);
        }

        // El tamaño del icono va en puntos: se convierte desde el alto en píxeles
        float tamano = Height * 0.42f * 72f / DeviceDpi;
        Dibujo.Icono(g, Icono, tamano, ClientRectangle, color);
    }
}
