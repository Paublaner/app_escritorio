namespace App_Escritorio.UI;

public static class Estilo
{
    public static readonly Color Lateral = Color.FromArgb(30, 41, 59);
    public static readonly Color LateralActivo = Color.FromArgb(51, 65, 85);
    public static readonly Color Fondo = Color.FromArgb(248, 250, 252);
    public static readonly Color Borde = Color.FromArgb(226, 232, 240);
    public static readonly Color TextoSecundario = Color.FromArgb(100, 116, 139);
    public static readonly Color Acento = Color.FromArgb(37, 99, 235);
    public static readonly Color Ingreso = Color.FromArgb(22, 163, 74);
    public static readonly Color Gasto = Color.FromArgb(220, 38, 38);
    public static readonly Color Aviso = Color.FromArgb(217, 119, 6);

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
        grid.BackgroundColor = Color.White;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Borde;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 36;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = TextoSecundario;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 245, 249);
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        grid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        grid.RowTemplate.Height = 34;
    }

    public static void BotonPrimario(Button boton) => Boton(boton, Acento, Color.White);

    public static void BotonSecundario(Button boton) => Boton(boton, Color.White, Color.FromArgb(30, 41, 59));

    public static void BotonPeligro(Button boton) => Boton(boton, Color.White, Gasto);

    private static void Boton(Button boton, Color fondo, Color texto)
    {
        boton.FlatStyle = FlatStyle.Flat;
        boton.BackColor = fondo;
        boton.ForeColor = texto;
        boton.FlatAppearance.BorderColor = fondo == Color.White ? Borde : fondo;
        boton.Cursor = Cursors.Hand;
        boton.UseVisualStyleBackColor = false;
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
