namespace App_Escritorio.UI
{
    partial class FormPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLateral = new Panel();
            btnTema = new Button();
            btnAsistente = new Button();
            btnPresupuestos = new Button();
            btnCategorias = new Button();
            btnMovimientos = new Button();
            btnCuentas = new Button();
            btnResumen = new Button();
            lblLogo = new Label();
            panelContenido = new Panel();
            panelLateral.SuspendLayout();
            SuspendLayout();
            //
            // panelLateral
            //
            panelLateral.BackColor = Color.FromArgb(30, 41, 59);
            panelLateral.Controls.Add(btnTema);
            panelLateral.Controls.Add(btnAsistente);
            panelLateral.Controls.Add(btnPresupuestos);
            panelLateral.Controls.Add(btnCategorias);
            panelLateral.Controls.Add(btnMovimientos);
            panelLateral.Controls.Add(btnCuentas);
            panelLateral.Controls.Add(btnResumen);
            panelLateral.Controls.Add(lblLogo);
            panelLateral.Dock = DockStyle.Left;
            panelLateral.Location = new Point(0, 0);
            panelLateral.Name = "panelLateral";
            panelLateral.Size = new Size(200, 760);
            panelLateral.TabIndex = 0;
            //
            // btnTema
            //
            btnTema.Cursor = Cursors.Hand;
            btnTema.Dock = DockStyle.Bottom;
            btnTema.FlatAppearance.BorderSize = 0;
            btnTema.FlatStyle = FlatStyle.Flat;
            btnTema.ForeColor = Color.White;
            btnTema.Location = new Point(0, 712);
            btnTema.Name = "btnTema";
            btnTema.Padding = new Padding(20, 0, 0, 0);
            btnTema.Size = new Size(200, 48);
            btnTema.TabIndex = 6;
            btnTema.Text = "Modo oscuro";
            btnTema.TextAlign = ContentAlignment.MiddleLeft;
            btnTema.UseVisualStyleBackColor = false;
            //
            // btnAsistente
            //
            btnAsistente.Cursor = Cursors.Hand;
            btnAsistente.FlatAppearance.BorderSize = 0;
            btnAsistente.FlatStyle = FlatStyle.Flat;
            btnAsistente.ForeColor = Color.White;
            btnAsistente.Location = new Point(0, 310);
            btnAsistente.Name = "btnAsistente";
            btnAsistente.Padding = new Padding(20, 0, 0, 0);
            btnAsistente.Size = new Size(200, 44);
            btnAsistente.TabIndex = 7;
            btnAsistente.Text = "Asistente";
            btnAsistente.TextAlign = ContentAlignment.MiddleLeft;
            btnAsistente.UseVisualStyleBackColor = false;
            //
            // btnPresupuestos
            //
            btnPresupuestos.Cursor = Cursors.Hand;
            btnPresupuestos.FlatAppearance.BorderSize = 0;
            btnPresupuestos.FlatStyle = FlatStyle.Flat;
            btnPresupuestos.ForeColor = Color.White;
            btnPresupuestos.Location = new Point(0, 266);
            btnPresupuestos.Name = "btnPresupuestos";
            btnPresupuestos.Padding = new Padding(20, 0, 0, 0);
            btnPresupuestos.Size = new Size(200, 44);
            btnPresupuestos.TabIndex = 5;
            btnPresupuestos.Text = "Presupuestos";
            btnPresupuestos.TextAlign = ContentAlignment.MiddleLeft;
            btnPresupuestos.UseVisualStyleBackColor = false;
            //
            // btnCategorias
            //
            btnCategorias.Cursor = Cursors.Hand;
            btnCategorias.FlatAppearance.BorderSize = 0;
            btnCategorias.FlatStyle = FlatStyle.Flat;
            btnCategorias.ForeColor = Color.White;
            btnCategorias.Location = new Point(0, 222);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Padding = new Padding(20, 0, 0, 0);
            btnCategorias.Size = new Size(200, 44);
            btnCategorias.TabIndex = 4;
            btnCategorias.Text = "Categorías";
            btnCategorias.TextAlign = ContentAlignment.MiddleLeft;
            btnCategorias.UseVisualStyleBackColor = false;
            //
            // btnMovimientos
            //
            btnMovimientos.Cursor = Cursors.Hand;
            btnMovimientos.FlatAppearance.BorderSize = 0;
            btnMovimientos.FlatStyle = FlatStyle.Flat;
            btnMovimientos.ForeColor = Color.White;
            btnMovimientos.Location = new Point(0, 178);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Padding = new Padding(20, 0, 0, 0);
            btnMovimientos.Size = new Size(200, 44);
            btnMovimientos.TabIndex = 3;
            btnMovimientos.Text = "Movimientos";
            btnMovimientos.TextAlign = ContentAlignment.MiddleLeft;
            btnMovimientos.UseVisualStyleBackColor = false;
            //
            // btnCuentas
            //
            btnCuentas.Cursor = Cursors.Hand;
            btnCuentas.FlatAppearance.BorderSize = 0;
            btnCuentas.FlatStyle = FlatStyle.Flat;
            btnCuentas.ForeColor = Color.White;
            btnCuentas.Location = new Point(0, 134);
            btnCuentas.Name = "btnCuentas";
            btnCuentas.Padding = new Padding(20, 0, 0, 0);
            btnCuentas.Size = new Size(200, 44);
            btnCuentas.TabIndex = 2;
            btnCuentas.Text = "Cuentas";
            btnCuentas.TextAlign = ContentAlignment.MiddleLeft;
            btnCuentas.UseVisualStyleBackColor = false;
            //
            // btnResumen
            //
            btnResumen.Cursor = Cursors.Hand;
            btnResumen.FlatAppearance.BorderSize = 0;
            btnResumen.FlatStyle = FlatStyle.Flat;
            btnResumen.ForeColor = Color.White;
            btnResumen.Location = new Point(0, 90);
            btnResumen.Name = "btnResumen";
            btnResumen.Padding = new Padding(20, 0, 0, 0);
            btnResumen.Size = new Size(200, 44);
            btnResumen.TabIndex = 1;
            btnResumen.Text = "Resumen";
            btnResumen.TextAlign = ContentAlignment.MiddleLeft;
            btnResumen.UseVisualStyleBackColor = false;
            //
            // lblLogo
            //
            lblLogo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(20, 24);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(160, 40);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "Finanzas";
            lblLogo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            panelContenido.BackColor = Color.FromArgb(248, 250, 252);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(200, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(24);
            panelContenido.Size = new Size(1060, 760);
            panelContenido.TabIndex = 1;
            //
            // FormPrincipal
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1260, 760);
            Controls.Add(panelContenido);
            Controls.Add(panelLateral);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1230, 720);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Finanzas";
            panelLateral.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLateral;
        private Label lblLogo;
        private Button btnResumen;
        private Button btnCuentas;
        private Button btnMovimientos;
        private Button btnCategorias;
        private Button btnPresupuestos;
        private Button btnAsistente;
        private Panel panelContenido;
        private Button btnTema;
    }
}
