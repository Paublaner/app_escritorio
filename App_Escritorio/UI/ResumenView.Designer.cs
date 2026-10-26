namespace App_Escritorio.UI
{
    partial class ResumenView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblMes = new Label();
            tablaTarjetas = new TableLayoutPanel();
            panelSaldo = new Tarjeta();
            iconoSaldo = new IconoCirculo();
            lblSaldoPie = new Label();
            lblSaldoValor = new Label();
            lblSaldoTitulo = new Label();
            panelIngresos = new Tarjeta();
            iconoIngresos = new IconoCirculo();
            lblIngresosPie = new Label();
            lblIngresosValor = new Label();
            lblIngresosTitulo = new Label();
            panelGastos = new Tarjeta();
            iconoGastos = new IconoCirculo();
            lblGastosPie = new Label();
            lblGastosValor = new Label();
            lblGastosTitulo = new Label();
            tablaInferior = new TableLayoutPanel();
            panelGrafico = new Tarjeta();
            graficoGastos = new GraficoCircular();
            lblGastosCategoria = new Label();
            panelPresupuestos = new Tarjeta();
            gridPresupuestos = new DataGridView();
            lblPresupuestos = new Label();
            panelMovimientos = new Tarjeta();
            gridMovimientos = new DataGridView();
            lblMovimientos = new Label();
            tablaTarjetas.SuspendLayout();
            panelSaldo.SuspendLayout();
            panelIngresos.SuspendLayout();
            panelGastos.SuspendLayout();
            tablaInferior.SuspendLayout();
            panelGrafico.SuspendLayout();
            panelPresupuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).BeginInit();
            panelMovimientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(139, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Resumen";
            //
            // lblMes
            //
            lblMes.AutoSize = true;
            lblMes.Location = new Point(3, 42);
            lblMes.Name = "lblMes";
            lblMes.Size = new Size(0, 19);
            lblMes.TabIndex = 1;
            //
            // tablaTarjetas
            //
            tablaTarjetas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tablaTarjetas.ColumnCount = 3;
            tablaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tablaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tablaTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tablaTarjetas.Controls.Add(panelSaldo, 0, 0);
            tablaTarjetas.Controls.Add(panelIngresos, 1, 0);
            tablaTarjetas.Controls.Add(panelGastos, 2, 0);
            tablaTarjetas.Location = new Point(0, 78);
            tablaTarjetas.Name = "tablaTarjetas";
            tablaTarjetas.RowCount = 1;
            tablaTarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaTarjetas.Size = new Size(950, 136);
            tablaTarjetas.TabIndex = 2;
            //
            // panelSaldo
            //
            panelSaldo.Controls.Add(iconoSaldo);
            panelSaldo.Controls.Add(lblSaldoPie);
            panelSaldo.Controls.Add(lblSaldoValor);
            panelSaldo.Controls.Add(lblSaldoTitulo);
            panelSaldo.Degradado = true;
            panelSaldo.Dock = DockStyle.Fill;
            panelSaldo.Location = new Point(0, 0);
            panelSaldo.Margin = new Padding(0, 0, 8, 0);
            panelSaldo.Name = "panelSaldo";
            panelSaldo.Size = new Size(308, 136);
            panelSaldo.TabIndex = 0;
            //
            // iconoSaldo
            //
            iconoSaldo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconoSaldo.Icono = "";
            iconoSaldo.Location = new Point(246, 18);
            iconoSaldo.Name = "iconoSaldo";
            iconoSaldo.Rol = Rol.Destacado;
            iconoSaldo.Size = new Size(42, 42);
            iconoSaldo.TabIndex = 3;
            //
            // lblSaldoPie
            //
            lblSaldoPie.AutoSize = true;
            lblSaldoPie.Font = new Font("Segoe UI", 9F);
            lblSaldoPie.Location = new Point(22, 94);
            lblSaldoPie.Name = "lblSaldoPie";
            lblSaldoPie.Size = new Size(0, 15);
            lblSaldoPie.TabIndex = 2;
            //
            // lblSaldoValor
            //
            lblSaldoValor.AutoSize = true;
            lblSaldoValor.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblSaldoValor.Location = new Point(18, 50);
            lblSaldoValor.Name = "lblSaldoValor";
            lblSaldoValor.Size = new Size(0, 38);
            lblSaldoValor.TabIndex = 1;
            //
            // lblSaldoTitulo
            //
            lblSaldoTitulo.AutoSize = true;
            lblSaldoTitulo.Font = new Font("Segoe UI Semibold", 10F);
            lblSaldoTitulo.Location = new Point(22, 22);
            lblSaldoTitulo.Name = "lblSaldoTitulo";
            lblSaldoTitulo.Size = new Size(76, 19);
            lblSaldoTitulo.TabIndex = 0;
            lblSaldoTitulo.Text = "Saldo total";
            //
            // panelIngresos
            //
            panelIngresos.Controls.Add(iconoIngresos);
            panelIngresos.Controls.Add(lblIngresosPie);
            panelIngresos.Controls.Add(lblIngresosValor);
            panelIngresos.Controls.Add(lblIngresosTitulo);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Location = new Point(320, 0);
            panelIngresos.Margin = new Padding(4, 0, 4, 0);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(308, 136);
            panelIngresos.TabIndex = 1;
            //
            // iconoIngresos
            //
            iconoIngresos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconoIngresos.Icono = "";
            iconoIngresos.Location = new Point(246, 18);
            iconoIngresos.Name = "iconoIngresos";
            iconoIngresos.Rol = Rol.Ingreso;
            iconoIngresos.Size = new Size(42, 42);
            iconoIngresos.TabIndex = 3;
            //
            // lblIngresosPie
            //
            lblIngresosPie.AutoSize = true;
            lblIngresosPie.Font = new Font("Segoe UI", 9F);
            lblIngresosPie.Location = new Point(22, 94);
            lblIngresosPie.Name = "lblIngresosPie";
            lblIngresosPie.Size = new Size(0, 15);
            lblIngresosPie.TabIndex = 2;
            //
            // lblIngresosValor
            //
            lblIngresosValor.AutoSize = true;
            lblIngresosValor.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblIngresosValor.Location = new Point(18, 50);
            lblIngresosValor.Name = "lblIngresosValor";
            lblIngresosValor.Size = new Size(0, 38);
            lblIngresosValor.TabIndex = 1;
            //
            // lblIngresosTitulo
            //
            lblIngresosTitulo.AutoSize = true;
            lblIngresosTitulo.Font = new Font("Segoe UI Semibold", 10F);
            lblIngresosTitulo.Location = new Point(22, 22);
            lblIngresosTitulo.Name = "lblIngresosTitulo";
            lblIngresosTitulo.Size = new Size(118, 19);
            lblIngresosTitulo.TabIndex = 0;
            lblIngresosTitulo.Text = "Ingresos del mes";
            //
            // panelGastos
            //
            panelGastos.Controls.Add(iconoGastos);
            panelGastos.Controls.Add(lblGastosPie);
            panelGastos.Controls.Add(lblGastosValor);
            panelGastos.Controls.Add(lblGastosTitulo);
            panelGastos.Dock = DockStyle.Fill;
            panelGastos.Location = new Point(640, 0);
            panelGastos.Margin = new Padding(8, 0, 0, 0);
            panelGastos.Name = "panelGastos";
            panelGastos.Size = new Size(310, 136);
            panelGastos.TabIndex = 2;
            //
            // iconoGastos
            //
            iconoGastos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            iconoGastos.Icono = "";
            iconoGastos.Location = new Point(248, 18);
            iconoGastos.Name = "iconoGastos";
            iconoGastos.Rol = Rol.Gasto;
            iconoGastos.Size = new Size(42, 42);
            iconoGastos.TabIndex = 3;
            //
            // lblGastosPie
            //
            lblGastosPie.AutoSize = true;
            lblGastosPie.Font = new Font("Segoe UI", 9F);
            lblGastosPie.Location = new Point(22, 94);
            lblGastosPie.Name = "lblGastosPie";
            lblGastosPie.Size = new Size(0, 15);
            lblGastosPie.TabIndex = 2;
            //
            // lblGastosValor
            //
            lblGastosValor.AutoSize = true;
            lblGastosValor.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblGastosValor.Location = new Point(18, 50);
            lblGastosValor.Name = "lblGastosValor";
            lblGastosValor.Size = new Size(0, 38);
            lblGastosValor.TabIndex = 1;
            //
            // lblGastosTitulo
            //
            lblGastosTitulo.AutoSize = true;
            lblGastosTitulo.Font = new Font("Segoe UI Semibold", 10F);
            lblGastosTitulo.Location = new Point(22, 22);
            lblGastosTitulo.Name = "lblGastosTitulo";
            lblGastosTitulo.Size = new Size(107, 19);
            lblGastosTitulo.TabIndex = 0;
            lblGastosTitulo.Text = "Gastos del mes";
            //
            // tablaInferior
            //
            tablaInferior.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaInferior.ColumnCount = 3;
            tablaInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tablaInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tablaInferior.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tablaInferior.Controls.Add(panelGrafico, 0, 0);
            tablaInferior.Controls.Add(panelPresupuestos, 1, 0);
            tablaInferior.Controls.Add(panelMovimientos, 2, 0);
            tablaInferior.Location = new Point(0, 228);
            tablaInferior.Name = "tablaInferior";
            tablaInferior.RowCount = 1;
            tablaInferior.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaInferior.Size = new Size(950, 412);
            tablaInferior.TabIndex = 3;
            //
            // panelGrafico
            //
            panelGrafico.Controls.Add(graficoGastos);
            panelGrafico.Controls.Add(lblGastosCategoria);
            panelGrafico.Dock = DockStyle.Fill;
            panelGrafico.Location = new Point(0, 0);
            panelGrafico.Margin = new Padding(0, 0, 8, 0);
            panelGrafico.Name = "panelGrafico";
            panelGrafico.Padding = new Padding(20, 18, 20, 24);
            panelGrafico.Size = new Size(308, 412);
            panelGrafico.TabIndex = 0;
            //
            // graficoGastos
            //
            graficoGastos.Dock = DockStyle.Fill;
            graficoGastos.Location = new Point(20, 54);
            graficoGastos.Name = "graficoGastos";
            graficoGastos.Size = new Size(268, 334);
            graficoGastos.TabIndex = 1;
            graficoGastos.TextoVacio = "Sin gastos este mes";
            //
            // lblGastosCategoria
            //
            lblGastosCategoria.Dock = DockStyle.Top;
            lblGastosCategoria.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblGastosCategoria.Location = new Point(20, 18);
            lblGastosCategoria.Name = "lblGastosCategoria";
            lblGastosCategoria.Size = new Size(268, 36);
            lblGastosCategoria.TabIndex = 0;
            lblGastosCategoria.Text = "Gastos por categoría";
            //
            // panelPresupuestos
            //
            panelPresupuestos.Controls.Add(gridPresupuestos);
            panelPresupuestos.Controls.Add(lblPresupuestos);
            panelPresupuestos.Dock = DockStyle.Fill;
            panelPresupuestos.Location = new Point(320, 0);
            panelPresupuestos.Margin = new Padding(4, 0, 4, 0);
            panelPresupuestos.Name = "panelPresupuestos";
            panelPresupuestos.Padding = new Padding(12, 18, 12, 24);
            panelPresupuestos.Size = new Size(308, 412);
            panelPresupuestos.TabIndex = 1;
            //
            // gridPresupuestos
            //
            gridPresupuestos.Dock = DockStyle.Fill;
            gridPresupuestos.Location = new Point(12, 54);
            gridPresupuestos.Name = "gridPresupuestos";
            gridPresupuestos.Size = new Size(284, 334);
            gridPresupuestos.TabIndex = 1;
            //
            // lblPresupuestos
            //
            lblPresupuestos.Dock = DockStyle.Top;
            lblPresupuestos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPresupuestos.Location = new Point(12, 18);
            lblPresupuestos.Name = "lblPresupuestos";
            lblPresupuestos.Padding = new Padding(8, 0, 0, 0);
            lblPresupuestos.Size = new Size(284, 36);
            lblPresupuestos.TabIndex = 0;
            lblPresupuestos.Text = "Presupuestos del mes";
            //
            // panelMovimientos
            //
            panelMovimientos.Controls.Add(gridMovimientos);
            panelMovimientos.Controls.Add(lblMovimientos);
            panelMovimientos.Dock = DockStyle.Fill;
            panelMovimientos.Location = new Point(640, 0);
            panelMovimientos.Margin = new Padding(8, 0, 0, 0);
            panelMovimientos.Name = "panelMovimientos";
            panelMovimientos.Padding = new Padding(12, 18, 12, 24);
            panelMovimientos.Size = new Size(310, 412);
            panelMovimientos.TabIndex = 2;
            //
            // gridMovimientos
            //
            gridMovimientos.Dock = DockStyle.Fill;
            gridMovimientos.Location = new Point(12, 54);
            gridMovimientos.Name = "gridMovimientos";
            gridMovimientos.Size = new Size(286, 334);
            gridMovimientos.TabIndex = 1;
            //
            // lblMovimientos
            //
            lblMovimientos.Dock = DockStyle.Top;
            lblMovimientos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMovimientos.Location = new Point(12, 18);
            lblMovimientos.Name = "lblMovimientos";
            lblMovimientos.Padding = new Padding(8, 0, 0, 0);
            lblMovimientos.Size = new Size(286, 36);
            lblMovimientos.TabIndex = 0;
            lblMovimientos.Text = "Movimientos del mes";
            //
            // ResumenView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 246, 251);
            Controls.Add(lblTitulo);
            Controls.Add(lblMes);
            Controls.Add(tablaTarjetas);
            Controls.Add(tablaInferior);
            Font = new Font("Segoe UI", 10F);
            Name = "ResumenView";
            Size = new Size(950, 640);
            tablaTarjetas.ResumeLayout(false);
            panelSaldo.ResumeLayout(false);
            panelSaldo.PerformLayout();
            panelIngresos.ResumeLayout(false);
            panelIngresos.PerformLayout();
            panelGastos.ResumeLayout(false);
            panelGastos.PerformLayout();
            tablaInferior.ResumeLayout(false);
            panelGrafico.ResumeLayout(false);
            panelPresupuestos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).EndInit();
            panelMovimientos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblMes;
        private TableLayoutPanel tablaTarjetas;
        private Tarjeta panelSaldo;
        private IconoCirculo iconoSaldo;
        private Label lblSaldoTitulo;
        private Label lblSaldoValor;
        private Label lblSaldoPie;
        private Tarjeta panelIngresos;
        private IconoCirculo iconoIngresos;
        private Label lblIngresosTitulo;
        private Label lblIngresosValor;
        private Label lblIngresosPie;
        private Tarjeta panelGastos;
        private IconoCirculo iconoGastos;
        private Label lblGastosTitulo;
        private Label lblGastosValor;
        private Label lblGastosPie;
        private TableLayoutPanel tablaInferior;
        private Tarjeta panelGrafico;
        private Label lblGastosCategoria;
        private GraficoCircular graficoGastos;
        private Tarjeta panelPresupuestos;
        private Label lblPresupuestos;
        private DataGridView gridPresupuestos;
        private Tarjeta panelMovimientos;
        private Label lblMovimientos;
        private DataGridView gridMovimientos;
    }
}
