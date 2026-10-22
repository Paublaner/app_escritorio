namespace App_Escritorio.UI;

// Windows no deja cambiar el fondo del DateTimePicker: en modo oscuro se dibuja a mano.
// Solo cambia el dibujo; los clics, el teclado y el calendario siguen siendo los de Windows.
public class SelectorFecha : DateTimePicker
{
    public void ActualizarTema()
    {
        bool dibujadoAMano = GetStyle(ControlStyles.UserPaint);

        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, Tema.Oscuro);

        // WinForms no le pasa la fuente a Windows si el control se creó dibujado a mano:
        // al volver al dibujo de Windows se recrea para que la reciba
        if (dibujadoAMano && !Tema.Oscuro && IsHandleCreated)
        {
            RecreateHandle();
        }

        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        var borde = new Rectangle(0, 0, Width - 1, Height - 1);

        g.Clear(Estilo.Superficie);

        using (var lapiz = new Pen(Focused ? Estilo.Acento : Estilo.Borde))
        {
            g.DrawRectangle(lapiz, borde);
        }

        // Con ShowUpDown las flechas son una ventana hija que pinta Windows; si no, se dibuja el desplegable
        int anchoBoton = ShowUpDown ? 0 : Height;

        var zonaTexto = new Rectangle(4, 0, Width - anchoBoton - 6, Height);
        TextRenderer.DrawText(g, Text, Font, zonaTexto, Estilo.Texto,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);

        if (!ShowUpDown)
        {
            int cx = Width - anchoBoton / 2 - 1;
            int cy = Height / 2;
            int lado = Math.Max(3, Height / 7);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using var pincel = new SolidBrush(Estilo.TextoSecundario);
            g.FillPolygon(pincel, new[]
            {
                new Point(cx - lado, cy - lado / 2),
                new Point(cx + lado, cy - lado / 2),
                new Point(cx, cy + lado / 2 + 1)
            });
        }
    }

    protected override void OnValueChanged(EventArgs eventargs)
    {
        base.OnValueChanged(eventargs);
        Invalidate();
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        Invalidate();
    }
}
