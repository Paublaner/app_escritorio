namespace App_Escritorio.UI;

// Papel de un control que el tema necesita conocer para darle color
public enum Rol
{
    Primario,
    Secundario,
    Peligro,
    Ingreso,
    Gasto
}

// Paleta de colores: cada propiedad devuelve el color del tema activo (claro u oscuro)
public static class Estilo
{
    public static Color Lateral => Tema.Oscuro ? Color.FromArgb(2, 6, 23) : Color.FromArgb(30, 41, 59);
    public static Color LateralActivo => Tema.Oscuro ? Color.FromArgb(30, 41, 59) : Color.FromArgb(51, 65, 85);
    public static Color Fondo => Tema.Oscuro ? Color.FromArgb(15, 23, 42) : Color.FromArgb(248, 250, 252);
    public static Color Superficie => Tema.Oscuro ? Color.FromArgb(30, 41, 59) : Color.White;
    public static Color Cabecera => Tema.Oscuro ? Color.FromArgb(22, 32, 51) : Color.FromArgb(241, 245, 249);
    public static Color Seleccion => Tema.Oscuro ? Color.FromArgb(30, 58, 138) : Color.FromArgb(219, 234, 254);
    public static Color Borde => Tema.Oscuro ? Color.FromArgb(51, 65, 85) : Color.FromArgb(226, 232, 240);
    public static Color Texto => Tema.Oscuro ? Color.FromArgb(226, 232, 240) : Color.FromArgb(15, 23, 42);
    public static Color TextoSecundario => Tema.Oscuro ? Color.FromArgb(148, 163, 184) : Color.FromArgb(100, 116, 139);
    public static Color Acento => Tema.Oscuro ? Color.FromArgb(59, 130, 246) : Color.FromArgb(37, 99, 235);
    public static Color Ingreso => Tema.Oscuro ? Color.FromArgb(74, 222, 128) : Color.FromArgb(22, 163, 74);
    public static Color Gasto => Tema.Oscuro ? Color.FromArgb(248, 113, 113) : Color.FromArgb(220, 38, 38);
    public static Color Aviso => Tema.Oscuro ? Color.FromArgb(251, 191, 36) : Color.FromArgb(217, 119, 6);

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
        grid.ColumnHeadersHeight = 36;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        grid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        grid.RowTemplate.Height = 34;

        ColoresGrid(grid);
    }

    public static void ColoresGrid(DataGridView grid)
    {
        grid.BackgroundColor = Superficie;
        grid.GridColor = Borde;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Cabecera;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = TextoSecundario;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Cabecera;
        grid.DefaultCellStyle.BackColor = Superficie;
        grid.DefaultCellStyle.ForeColor = Texto;
        grid.DefaultCellStyle.SelectionBackColor = Seleccion;
        grid.DefaultCellStyle.SelectionForeColor = Texto;
    }

    public static void BotonPrimario(Button boton) => Boton(boton, Rol.Primario);

    public static void BotonSecundario(Button boton) => Boton(boton, Rol.Secundario);

    public static void BotonPeligro(Button boton) => Boton(boton, Rol.Peligro);

    private static void Boton(Button boton, Rol rol)
    {
        boton.Tag = rol;
        boton.FlatStyle = FlatStyle.Flat;
        boton.Cursor = Cursors.Hand;
        boton.UseVisualStyleBackColor = false;

        ColoresBoton(boton);
    }

    // Los botones sin rol (por ejemplo, los de los diálogos) se pintan como secundarios
    public static void ColoresBoton(Button boton)
    {
        if (boton.Tag is not Rol)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.UseVisualStyleBackColor = false;
        }

        bool primario = boton.Tag is Rol.Primario;

        boton.BackColor = primario ? Acento : Superficie;
        boton.ForeColor = boton.Tag switch
        {
            Rol.Primario => Color.White,
            Rol.Peligro => Gasto,
            _ => Texto
        };
        boton.FlatAppearance.BorderColor = primario ? Acento : Borde;
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
