using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace App_Escritorio.UI;

// Papel de un control que el tema necesita conocer para darle color
public enum Rol
{
    Primario,
    Secundario,
    Peligro,
    Ingreso,
    Gasto,
    // Texto blanco sobre la tarjeta con degradado
    Destacado
}

// Paleta de colores: cada propiedad devuelve el color del tema activo (claro u oscuro)
public static class Estilo
{
    public static Color Lateral => Tema.Oscuro ? Color.FromArgb(14, 19, 36) : Color.White;
    public static Color Fondo => Tema.Oscuro ? Color.FromArgb(9, 13, 28) : Color.FromArgb(244, 246, 251);
    public static Color Superficie => Tema.Oscuro ? Color.FromArgb(21, 27, 46) : Color.White;
    public static Color Hover => Tema.Oscuro ? Color.FromArgb(31, 39, 64) : Color.FromArgb(241, 244, 250);
    public static Color Seleccion => Tema.Oscuro ? Color.FromArgb(40, 44, 92) : Color.FromArgb(238, 240, 255);
    public static Color Borde => Tema.Oscuro ? Color.FromArgb(38, 46, 72) : Color.FromArgb(228, 232, 241);
    public static Color Texto => Tema.Oscuro ? Color.FromArgb(232, 236, 245) : Color.FromArgb(15, 23, 42);
    public static Color TextoSecundario => Tema.Oscuro ? Color.FromArgb(140, 152, 180) : Color.FromArgb(100, 112, 139);
    public static Color Acento => Tema.Oscuro ? Color.FromArgb(129, 140, 248) : Color.FromArgb(99, 102, 241);
    public static Color AcentoSuave => Tema.Oscuro ? Color.FromArgb(37, 40, 84) : Color.FromArgb(238, 240, 255);
    public static Color Ingreso => Tema.Oscuro ? Color.FromArgb(52, 211, 153) : Color.FromArgb(5, 150, 105);
    public static Color IngresoSuave => Tema.Oscuro ? Color.FromArgb(16, 48, 46) : Color.FromArgb(220, 252, 236);
    public static Color Gasto => Tema.Oscuro ? Color.FromArgb(251, 113, 133) : Color.FromArgb(225, 29, 72);
    public static Color GastoSuave => Tema.Oscuro ? Color.FromArgb(56, 24, 44) : Color.FromArgb(255, 228, 232);
    public static Color Aviso => Tema.Oscuro ? Color.FromArgb(251, 191, 36) : Color.FromArgb(217, 119, 6);

    // Extremos del degradado (índigo a violeta): el texto encima es siempre blanco, así que no cambian con el tema
    public static Color AcentoInicio => Color.FromArgb(99, 102, 241);
    public static Color AcentoFin => Color.FromArgb(168, 85, 247);

    private const int RadioBoton = 10;

    // Se reutiliza: CellFormatting salta en cada repintado y crear una fuente cada vez gasta recursos de Windows
    private static readonly Font FuenteImporte = new("Segoe UI Semibold", 10F);

    // Icono opcional de cada botón, dibujado a la izquierda del texto
    private static readonly ConditionalWeakTable<Button, string> IconosBoton = new();

    // Marca etiquetas con un rol (texto secundario, ingreso, gasto) para que el tema les dé su color
    public static void Marcar(Rol rol, params Label[] etiquetas)
    {
        foreach (var etiqueta in etiquetas)
        {
            etiqueta.Tag = rol;
            etiqueta.ForeColor = ColorEtiqueta(etiqueta);
        }
    }

    public static Color ColorEtiqueta(Label etiqueta) => etiqueta.Tag switch
    {
        Rol.Secundario => TextoSecundario,
        Rol.Ingreso => Ingreso,
        Rol.Gasto => Gasto,
        Rol.Destacado => Color.White,
        _ => Texto
    };

    public static void Grid(DataGridView grid)
    {
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.RowHeadersVisible = false;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 40;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 8.5F);
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
        grid.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        grid.RowTemplate.Height = 44;

        grid.DataBindingComplete += CabecerasEnMayusculas;
        grid.CellPainting += PintarCelda;
        grid.CellMouseEnter += (_, e) => Resaltar(grid, e.RowIndex, true);
        grid.CellMouseLeave += (_, e) => Resaltar(grid, e.RowIndex, false);

        ColoresGrid(grid);
    }

    public static void ColoresGrid(DataGridView grid)
    {
        grid.BackgroundColor = Superficie;
        grid.GridColor = Borde;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Superficie;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = TextoSecundario;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Superficie;
        grid.DefaultCellStyle.BackColor = Superficie;
        grid.DefaultCellStyle.ForeColor = Texto;
        grid.DefaultCellStyle.SelectionBackColor = Seleccion;
        grid.DefaultCellStyle.SelectionForeColor = Texto;

        // El resaltado de la fila bajo el ratón se queda con el color del tema anterior
        foreach (DataGridViewRow fila in grid.Rows)
        {
            fila.DefaultCellStyle.BackColor = Color.Empty;
        }
    }

    private static void Resaltar(DataGridView grid, int fila, bool encima)
    {
        if (fila >= 0 && fila < grid.Rows.Count)
        {
            grid.Rows[fila].DefaultCellStyle.BackColor = encima ? Hover : Color.Empty;
        }
    }

    private static void CabecerasEnMayusculas(object? sender, DataGridViewBindingCompleteEventArgs e)
    {
        if (sender is DataGridView grid)
        {
            foreach (DataGridViewColumn columna in grid.Columns)
            {
                columna.HeaderText = columna.HeaderText.ToUpperInvariant();
            }
        }
    }

    // Dibujos propios de algunas celdas: la línea bajo la cabecera, el tipo como etiqueta de color
    // y el porcentaje de presupuesto usado como barra de progreso
    private static void PintarCelda(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (sender is not DataGridView grid || e.Graphics is null || e.ColumnIndex < 0)
        {
            return;
        }

        if (e.RowIndex == -1)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            using var lapiz = new Pen(Borde);
            e.Graphics.DrawLine(lapiz, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
            e.Handled = true;
            return;
        }

        string propiedad = grid.Columns[e.ColumnIndex].DataPropertyName;

        if (propiedad == nameof(FilaCuenta.Tipo) && e.Value is string tipo)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.Border);
            PintarEtiqueta(e.Graphics, e.CellBounds, tipo, grid.Font, grid.DeviceDpi);
            e.Handled = true;
        }
        else if (propiedad == nameof(FilaPresupuesto.Uso) && e.Value is decimal uso)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.Border);
            PintarProgreso(e.Graphics, e.CellBounds, uso, grid.Font, grid.DeviceDpi);
            e.Handled = true;
        }
    }

    private static void PintarEtiqueta(Graphics g, Rectangle celda, string texto, Font fuente, int dpi)
    {
        var (fondo, color) = texto switch
        {
            "Ingreso" => (IngresoSuave, Ingreso),
            "Gasto" => (GastoSuave, Gasto),
            _ => (AcentoSuave, Acento)
        };

        float escala = dpi / 96f;

        using var negrita = new Font("Segoe UI Semibold", fuente.Size * 0.9f);
        var medida = TextRenderer.MeasureText(texto, negrita);
        int alto = (int)(24 * escala);
        var pastilla = new Rectangle(celda.Left + (int)(8 * escala), celda.Top + (celda.Height - alto) / 2,
            medida.Width + (int)(12 * escala), alto);

        g.SmoothingMode = SmoothingMode.AntiAlias;

        using (var forma = Dibujo.Redondeado(pastilla, alto / 2f))
        using (var pincel = new SolidBrush(fondo))
        {
            g.FillPath(pincel, forma);
        }

        TextRenderer.DrawText(g, texto, negrita, pastilla, color,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }

    private static void PintarProgreso(Graphics g, Rectangle celda, decimal uso, Font fuente, int dpi)
    {
        float escala = dpi / 96f;
        var color = uso >= 1 ? Gasto : uso >= 0.8m ? Aviso : Acento;

        string texto = uso.ToString("P0");
        int anchoTexto = (int)(52 * escala);
        int margen = (int)(8 * escala);
        int alto = (int)(8 * escala);

        var barra = new Rectangle(celda.Left + margen, celda.Top + (celda.Height - alto) / 2,
            Math.Max(celda.Width - anchoTexto - 2 * margen, 10), alto);

        g.SmoothingMode = SmoothingMode.AntiAlias;

        using (var forma = Dibujo.Redondeado(barra, alto / 2f))
        using (var pincel = new SolidBrush(Borde))
        {
            g.FillPath(pincel, forma);
        }

        float relleno = (float)Math.Min(uso, 1) * barra.Width;

        if (relleno >= alto)
        {
            var lleno = new RectangleF(barra.Left, barra.Top, relleno, barra.Height);

            using var forma = Dibujo.Redondeado(lleno, alto / 2f);
            using var pincel = new SolidBrush(color);
            g.FillPath(pincel, forma);
        }

        var zonaTexto = new Rectangle(barra.Right, celda.Top, celda.Right - barra.Right - margen, celda.Height);
        TextRenderer.DrawText(g, texto, fuente, zonaTexto, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
    }

    public static void BotonPrimario(Button boton) => Boton(boton, Rol.Primario);

    public static void BotonSecundario(Button boton) => Boton(boton, Rol.Secundario);

    public static void BotonPeligro(Button boton) => Boton(boton, Rol.Peligro);

    public static void Icono(Button boton, string icono)
    {
        IconosBoton.AddOrUpdate(boton, icono);
        boton.Invalidate();
    }

    private static void Boton(Button boton, Rol rol)
    {
        boton.Tag = rol;
        ColoresBoton(boton);
    }

    // Los botones se dibujan enteros a mano (redondeados, con degradado y con efecto al pasar el ratón).
    // Los que no tienen rol (por ejemplo, los de los diálogos) se pintan como secundarios
    public static void ColoresBoton(Button boton)
    {
        boton.FlatStyle = FlatStyle.Flat;
        boton.FlatAppearance.BorderSize = 0;
        boton.UseVisualStyleBackColor = false;
        boton.Cursor = Cursors.Hand;

        // Quitar antes de añadir: el tema se aplica varias veces y no hay que suscribirse dos veces
        boton.Paint -= PintarBoton;
        boton.Paint += PintarBoton;
        boton.MouseEnter -= Repintar;
        boton.MouseEnter += Repintar;
        boton.MouseLeave -= Repintar;
        boton.MouseLeave += Repintar;
        boton.MouseDown -= Repintar;
        boton.MouseDown += Repintar;
        boton.MouseUp -= Repintar;
        boton.MouseUp += Repintar;
        boton.EnabledChanged -= Repintar;
        boton.EnabledChanged += Repintar;

        boton.Invalidate();
    }

    private static void Repintar(object? sender, EventArgs e) => (sender as Control)?.Invalidate();

    private static void PintarBoton(object? sender, PaintEventArgs e)
    {
        if (sender is not Button boton)
        {
            return;
        }

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(boton.Parent?.BackColor ?? Fondo);

        bool encima = boton.Enabled && boton.ClientRectangle.Contains(boton.PointToClient(Cursor.Position));
        bool pulsado = encima && (Control.MouseButtons & MouseButtons.Left) != 0;
        var rol = boton.Tag as Rol? ?? Rol.Secundario;

        var rect = new RectangleF(0.5f, 0.5f, boton.Width - 1.5f, boton.Height - 1.5f);

        if (pulsado)
        {
            rect.Inflate(-1, -1);
        }

        float radio = RadioBoton * boton.DeviceDpi / 96f;
        Color colorTexto;

        using (var forma = Dibujo.Redondeado(rect, radio))
        {
            if (rol == Rol.Primario && boton.Enabled)
            {
                using var degradado = Dibujo.Degradado(rect, encima);
                g.FillPath(degradado, forma);
                colorTexto = Color.White;
            }
            else
            {
                var fondo = rol switch
                {
                    Rol.Peligro when encima => GastoSuave,
                    _ when encima => Hover,
                    _ => Superficie
                };

                using var pincel = new SolidBrush(fondo);
                using var borde = new Pen(rol == Rol.Peligro && encima ? Gasto : Borde);
                g.FillPath(pincel, forma);
                g.DrawPath(borde, forma);

                colorTexto = !boton.Enabled ? TextoSecundario : rol == Rol.Peligro ? Gasto : Texto;
            }
        }

        using var fuente = new Font("Segoe UI Semibold", boton.Font.Size);
        var zona = Rectangle.Round(rect);

        if (!IconosBoton.TryGetValue(boton, out var icono))
        {
            TextRenderer.DrawText(g, boton.Text, fuente, zona, colorTexto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            return;
        }

        // Icono y texto centrados juntos
        int separacion = string.IsNullOrEmpty(boton.Text) ? 0 : (int)(8 * boton.DeviceDpi / 96f);
        int anchoIcono = (int)(fuente.Size * 1.6f * boton.DeviceDpi / 72f);
        int anchoTexto = string.IsNullOrEmpty(boton.Text) ? 0 : TextRenderer.MeasureText(boton.Text, fuente).Width;
        int x = zona.Left + (zona.Width - anchoIcono - separacion - anchoTexto) / 2;

        Dibujo.Icono(g, icono, fuente.Size, new Rectangle(x, zona.Top, anchoIcono, zona.Height), colorTexto);
        TextRenderer.DrawText(g, boton.Text, fuente, new Rectangle(x + anchoIcono + separacion, zona.Top, anchoTexto, zona.Height),
            colorTexto, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
    }

    public static void Formato(DataGridView grid, string columna, string formato, bool alinearDerecha)
    {
        var col = grid.Columns[columna];

        if (col is null)
        {
            return;
        }

        col.DefaultCellStyle.Format = formato;

        if (alinearDerecha)
        {
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }

    public static void Ocultar(DataGridView grid, params string[] columnas)
    {
        foreach (var columna in columnas)
        {
            if (grid.Columns[columna] is { } col)
            {
                col.Visible = false;
            }
        }
    }

    // Importe en verde si es ingreso y en rojo si es gasto
    public static void ColorearMovimiento(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView grid || e.RowIndex < 0 || e.CellStyle is null)
        {
            return;
        }

        if (grid.Columns[e.ColumnIndex].DataPropertyName == nameof(FilaMovimiento.Importe)
            && grid.Rows[e.RowIndex].DataBoundItem is FilaMovimiento fila)
        {
            e.CellStyle.ForeColor = fila.EsGasto ? Gasto : Ingreso;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            e.CellStyle.Font = FuenteImporte;
        }
    }

    // Presupuesto pasado en rojo y cerca del límite (80 %) en naranja
    public static void ColorearPresupuesto(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is not DataGridView grid || e.RowIndex < 0 || e.CellStyle is null)
        {
            return;
        }

        if (grid.Rows[e.RowIndex].DataBoundItem is FilaPresupuesto fila)
        {
            if (fila.Uso >= 1)
            {
                e.CellStyle.ForeColor = Gasto;
            }
            else if (fila.Uso >= 0.8m)
            {
                e.CellStyle.ForeColor = Aviso;
            }

            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }
    }
}
