using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace App_Escritorio.UI;

// Panel con esquinas redondeadas y sombra suave. Con Degradado se pinta con el color de acento
// (la tarjeta destacada del resumen) y sus etiquetas deben ir marcadas como Rol.Destacado
public class Tarjeta : Panel
{
    [DefaultValue(16)]
    public int Radio { get; set; } = 16;

    [DefaultValue(false)]
    public bool Degradado { get; set; }

    public Tarjeta()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer
            | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

        Padding = new Padding(20);
        ActualizarTema();
    }

    public void ActualizarTema()
    {
        // Es el color que heredan los hijos (etiquetas, tablas...), así que coincide con el de la tarjeta
        BackColor = Degradado ? Estilo.AcentoInicio : Estilo.Superficie;
        Invalidate(true);
    }

    // La zona de la tarjeta sin el hueco que se deja alrededor para la sombra
    private RectangleF Cuerpo
    {
        get
        {
            float escala = DeviceDpi / 96f;
            return new RectangleF(3 * escala, 2 * escala, Width - 6 * escala - 1, Height - 7 * escala - 1);
        }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent?.BackColor ?? Estilo.Fondo);

        float radio = Radio * DeviceDpi / 96f;
        var cuerpo = Cuerpo;

        if (cuerpo.Width <= 0 || cuerpo.Height <= 0)
        {
            return;
        }

        Dibujo.Sombra(g, cuerpo, radio, 5);

        using var forma = Dibujo.Redondeado(cuerpo, radio);

        if (Degradado)
        {
            using var degradado = Dibujo.Degradado(cuerpo);
            g.FillPath(degradado, forma);
            DibujarBrillo(g, cuerpo, forma);
            return;
        }

        using var fondo = new SolidBrush(Estilo.Superficie);
        using var borde = new Pen(Estilo.Borde);
        g.FillPath(fondo, forma);
        g.DrawPath(borde, forma);
    }

    // Dos círculos blancos casi transparentes en la esquina, para que el degradado no quede plano
    private static void DibujarBrillo(Graphics g, RectangleF cuerpo, GraphicsPath forma)
    {
        var anterior = g.Clip;
        g.SetClip(forma, CombineMode.Intersect);

        float d = cuerpo.Height * 1.4f;

        using (var pincel = new SolidBrush(Color.FromArgb(28, Color.White)))
        {
            g.FillEllipse(pincel, cuerpo.Right - d * 0.55f, cuerpo.Top - d * 0.45f, d, d);
        }

        using (var pincel = new SolidBrush(Color.FromArgb(18, Color.White)))
        {
            g.FillEllipse(pincel, cuerpo.Right - d * 0.25f, cuerpo.Top + d * 0.15f, d * 0.7f, d * 0.7f);
        }

        g.Clip = anterior;
    }
}
