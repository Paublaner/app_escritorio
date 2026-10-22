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
            panelSaldo = new Panel();
            lblSaldoValor = new Label();
            lblSaldoTitulo = new Label();
            panelIngresos = new Panel();
            lblIngresosValor = new Label();
            lblIngresosTitulo = new Label();
            panelGastos = new Panel();
            lblGastosValor = new Label();
            lblGastosTitulo = new Label();
            tablaInferior = new TableLayoutPanel();
            lblPresupuestos = new Label();
            lblMovimientos = new Label();
            lblGastosCategoria = new Label();
            graficoGastos = new GraficoCircular();
            gridPresupuestos = new DataGridView();
            gridMovimientos = new DataGridView();
            tablaTarjetas.SuspendLayout();
            panelSaldo.SuspendLayout();
            panelIngresos.SuspendLayout();
            panelGastos.SuspendLayout();
            tablaInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(125, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Resumen";
            //
            // lblMes
            //
            lblMes.AutoSize = true;
            lblMes.ForeColor = Color.FromArgb(100, 116, 139);
            lblMes.Location = new Point(2, 36);
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
            tablaTarjetas.Location = new Point(0, 72);
            tablaTarjetas.Name = "tablaTarjetas";
            tablaTarjetas.RowCount = 1;
            tablaTarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaTarjetas.Size = new Size(950, 110);
            tablaTarjetas.TabIndex = 2;
            //
            // panelSaldo
            //
            panelSaldo.BackColor = Color.White;
            panelSaldo.BorderStyle = BorderStyle.FixedSingle;
            panelSaldo.Controls.Add(lblSaldoValor);
            panelSaldo.Controls.Add(lblSaldoTitulo);
            panelSaldo.Dock = DockStyle.Fill;
            panelSaldo.Location = new Point(0, 0);
            panelSaldo.Margin = new Padding(0, 0, 12, 0);
            panelSaldo.Name = "panelSaldo";
            panelSaldo.Size = new Size(304, 110);
            panelSaldo.TabIndex = 0;
            //
            // lblSaldoValor
            //
            lblSaldoValor.AutoSize = true;
            lblSaldoValor.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblSaldoValor.Location = new Point(16, 44);
            lblSaldoValor.Name = "lblSaldoValor";
            lblSaldoValor.Size = new Size(0, 37);
            lblSaldoValor.TabIndex = 1;
            //
            // lblSaldoTitulo
            //
            lblSaldoTitulo.AutoSize = true;
            lblSaldoTitulo.ForeColor = Color.FromArgb(100, 116, 139);
            lblSaldoTitulo.Location = new Point(16, 16);
            lblSaldoTitulo.Name = "lblSaldoTitulo";
            lblSaldoTitulo.Size = new Size(77, 19);
            lblSaldoTitulo.TabIndex = 0;
            lblSaldoTitulo.Text = "Saldo total";
            //
            // panelIngresos
            //
            panelIngresos.BackColor = Color.White;
            panelIngresos.BorderStyle = BorderStyle.FixedSingle;
            panelIngresos.Controls.Add(lblIngresosValor);
            panelIngresos.Controls.Add(lblIngresosTitulo);
            panelIngresos.Dock = DockStyle.Fill;
            panelIngresos.Location = new Point(322, 0);
            panelIngresos.Margin = new Padding(6, 0, 6, 0);
            panelIngresos.Name = "panelIngresos";
            panelIngresos.Size = new Size(304, 110);
            panelIngresos.TabIndex = 1;
            //
            // lblIngresosValor
            //
            lblIngresosValor.AutoSize = true;
            lblIngresosValor.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblIngresosValor.ForeColor = Color.FromArgb(22, 163, 74);
            lblIngresosValor.Location = new Point(16, 44);
            lblIngresosValor.Name = "lblIngresosValor";
            lblIngresosValor.Size = new Size(0, 37);
            lblIngresosValor.TabIndex = 1;
            //
            // lblIngresosTitulo
            //
            lblIngresosTitulo.AutoSize = true;
            lblIngresosTitulo.ForeColor = Color.FromArgb(100, 116, 139);
            lblIngresosTitulo.Location = new Point(16, 16);
            lblIngresosTitulo.Name = "lblIngresosTitulo";
            lblIngresosTitulo.Size = new Size(122, 19);
            lblIngresosTitulo.TabIndex = 0;
            lblIngresosTitulo.Text = "Ingresos del mes";
            //
            // panelGastos
            //
            panelGastos.BackColor = Color.White;
            panelGastos.BorderStyle = BorderStyle.FixedSingle;
            panelGastos.Controls.Add(lblGastosValor);
            panelGastos.Controls.Add(lblGastosTitulo);
            panelGastos.Dock = DockStyle.Fill;
            panelGastos.Location = new Point(645, 0);
            panelGastos.Margin = new Padding(12, 0, 0, 0);
            panelGastos.Name = "panelGastos";
            panelGastos.Size = new Size(305, 110);
            panelGastos.TabIndex = 2;
            //
            // lblGastosValor
            //
            lblGastosValor.AutoSize = true;
            lblGastosValor.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblGastosValor.ForeColor = Color.FromArgb(220, 38, 38);
            lblGastosValor.Location = new Point(16, 44);
            lblGastosValor.Name = "lblGastosValor";
            lblGastosValor.Size = new Size(0, 37);
            lblGastosValor.TabIndex = 1;
            //
            // lblGastosTitulo
            //
            lblGastosTitulo.AutoSize = true;
            lblGastosTitulo.ForeColor = Color.FromArgb(100, 116, 139);
            lblGastosTitulo.Location = new Point(16, 16);
            lblGastosTitulo.Name = "lblGastosTitulo";
            lblGastosTitulo.Size = new Size(110, 19);
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
            tablaInferior.Controls.Add(lblGastosCategoria, 0, 0);
            tablaInferior.Controls.Add(lblPresupuestos, 1, 0);
            tablaInferior.Controls.Add(lblMovimientos, 2, 0);
            tablaInferior.Controls.Add(graficoGastos, 0, 1);
            tablaInferior.Controls.Add(gridPresupuestos, 1, 1);
            tablaInferior.Controls.Add(gridMovimientos, 2, 1);
            tablaInferior.Location = new Point(0, 204);
            tablaInferior.Name = "tablaInferior";
            tablaInferior.RowCount = 2;
            tablaInferior.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tablaInferior.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablaInferior.Size = new Size(950, 436);
            tablaInferior.TabIndex = 3;
            //
            // lblGastosCategoria
            //
            lblGastosCategoria.AutoSize = true;
            lblGastosCategoria.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblGastosCategoria.Location = new Point(0, 0);
            lblGastosCategoria.Margin = new Padding(0);
            lblGastosCategoria.Name = "lblGastosCategoria";
            lblGastosCategoria.Size = new Size(177, 21);
            lblGastosCategoria.TabIndex = 4;
            lblGastosCategoria.Text = "Gastos por categoría";
            //
            // graficoGastos
            //
            graficoGastos.Dock = DockStyle.Fill;
            graficoGastos.Location = new Point(0, 36);
            graficoGastos.Margin = new Padding(0, 0, 8, 0);
            graficoGastos.Name = "graficoGastos";
            graficoGastos.Size = new Size(308, 400);
            graficoGastos.TabIndex = 5;
            graficoGastos.TextoVacio = "Sin gastos este mes";
            //
            // lblPresupuestos
            //
            lblPresupuestos.AutoSize = true;
            lblPresupuestos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPresupuestos.Location = new Point(324, 0);
            lblPresupuestos.Margin = new Padding(8, 0, 0, 0);
            lblPresupuestos.Name = "lblPresupuestos";
            lblPresupuestos.Size = new Size(169, 21);
            lblPresupuestos.TabIndex = 0;
            lblPresupuestos.Text = "Presupuestos del mes";
            //
            // lblMovimientos
            //
            lblMovimientos.AutoSize = true;
            lblMovimientos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMovimientos.Location = new Point(641, 0);
            lblMovimientos.Margin = new Padding(8, 0, 0, 0);
            lblMovimientos.Name = "lblMovimientos";
            lblMovimientos.Size = new Size(168, 21);
            lblMovimientos.TabIndex = 1;
            lblMovimientos.Text = "Movimientos del mes";
            //
            // gridPresupuestos
            //
            gridPresupuestos.Dock = DockStyle.Fill;
            gridPresupuestos.Location = new Point(324, 36);
            gridPresupuestos.Margin = new Padding(8, 0, 8, 0);
            gridPresupuestos.Name = "gridPresupuestos";
            gridPresupuestos.Size = new Size(301, 400);
            gridPresupuestos.TabIndex = 2;
            //
            // gridMovimientos
            //
            gridMovimientos.Dock = DockStyle.Fill;
            gridMovimientos.Location = new Point(641, 36);
            gridMovimientos.Margin = new Padding(8, 0, 0, 0);
            gridMovimientos.Name = "gridMovimientos";
            gridMovimientos.Size = new Size(309, 400);
            gridMovimientos.TabIndex = 3;
            //
            // ResumenView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
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
            tablaInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblMes;
        private TableLayoutPanel tablaTarjetas;
        private Panel panelSaldo;
        private Label lblSaldoTitulo;
        private Label lblSaldoValor;
        private Panel panelIngresos;
        private Label lblIngresosTitulo;
        private Label lblIngresosValor;
        private Panel panelGastos;
        private Label lblGastosTitulo;
        private Label lblGastosValor;
        private TableLayoutPanel tablaInferior;
        private Label lblPresupuestos;
        private Label lblMovimientos;
        private DataGridView gridPresupuestos;
        private DataGridView gridMovimientos;
        private Label lblGastosCategoria;
        private GraficoCircular graficoGastos;
    }
}
