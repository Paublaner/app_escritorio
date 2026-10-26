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
            panelContenido = new Panel();
            panelLateral.SuspendLayout();
            SuspendLayout();
            //
            // panelLateral
            //
            panelLateral.BackColor = Color.White;
            panelLateral.Controls.Add(btnTema);
            panelLateral.Controls.Add(btnAsistente);
            panelLateral.Controls.Add(btnPresupuestos);
            panelLateral.Controls.Add(btnCategorias);
            panelLateral.Controls.Add(btnMovimientos);
            panelLateral.Controls.Add(btnCuentas);
            panelLateral.Controls.Add(btnResumen);
            panelLateral.Dock = DockStyle.Left;
            panelLateral.Location = new Point(0, 0);
            panelLateral.Name = "panelLateral";
            panelLateral.Size = new Size(240, 800);
            panelLateral.TabIndex = 0;
            panelLateral.Paint += panelLateral_Paint;
            //
            // btnTema
            //
            btnTema.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnTema.Cursor = Cursors.Hand;
            btnTema.FlatAppearance.BorderSize = 0;
            btnTema.FlatStyle = FlatStyle.Flat;
            btnTema.Location = new Point(12, 740);
            btnTema.Name = "btnTema";
            btnTema.Size = new Size(216, 44);
            btnTema.TabIndex = 6;
            btnTema.Text = "Modo oscuro";
            btnTema.UseVisualStyleBackColor = false;
            //
            // btnAsistente
            //
            btnAsistente.Cursor = Cursors.Hand;
            btnAsistente.FlatAppearance.BorderSize = 0;
            btnAsistente.FlatStyle = FlatStyle.Flat;
            btnAsistente.Location = new Point(12, 360);
            btnAsistente.Name = "btnAsistente";
            btnAsistente.Size = new Size(216, 44);
            btnAsistente.TabIndex = 5;
            btnAsistente.Text = "Asistente";
            btnAsistente.UseVisualStyleBackColor = false;
            //
            // btnPresupuestos
            //
            btnPresupuestos.Cursor = Cursors.Hand;
            btnPresupuestos.FlatAppearance.BorderSize = 0;
            btnPresupuestos.FlatStyle = FlatStyle.Flat;
            btnPresupuestos.Location = new Point(12, 312);
            btnPresupuestos.Name = "btnPresupuestos";
            btnPresupuestos.Size = new Size(216, 44);
            btnPresupuestos.TabIndex = 4;
            btnPresupuestos.Text = "Presupuestos";
            btnPresupuestos.UseVisualStyleBackColor = false;
            //
            // btnCategorias
            //
            btnCategorias.Cursor = Cursors.Hand;
            btnCategorias.FlatAppearance.BorderSize = 0;
            btnCategorias.FlatStyle = FlatStyle.Flat;
            btnCategorias.Location = new Point(12, 264);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(216, 44);
            btnCategorias.TabIndex = 3;
            btnCategorias.Text = "Categorías";
            btnCategorias.UseVisualStyleBackColor = false;
            //
            // btnMovimientos
            //
            btnMovimientos.Cursor = Cursors.Hand;
            btnMovimientos.FlatAppearance.BorderSize = 0;
            btnMovimientos.FlatStyle = FlatStyle.Flat;
            btnMovimientos.Location = new Point(12, 216);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new Size(216, 44);
            btnMovimientos.TabIndex = 2;
            btnMovimientos.Text = "Movimientos";
            btnMovimientos.UseVisualStyleBackColor = false;
            //
            // btnCuentas
            //
            btnCuentas.Cursor = Cursors.Hand;
            btnCuentas.FlatAppearance.BorderSize = 0;
            btnCuentas.FlatStyle = FlatStyle.Flat;
            btnCuentas.Location = new Point(12, 168);
            btnCuentas.Name = "btnCuentas";
            btnCuentas.Size = new Size(216, 44);
            btnCuentas.TabIndex = 1;
            btnCuentas.Text = "Cuentas";
            btnCuentas.UseVisualStyleBackColor = false;
            //
            // btnResumen
            //
            btnResumen.Cursor = Cursors.Hand;
            btnResumen.FlatAppearance.BorderSize = 0;
            btnResumen.FlatStyle = FlatStyle.Flat;
            btnResumen.Location = new Point(12, 120);
            btnResumen.Name = "btnResumen";
            btnResumen.Size = new Size(216, 44);
            btnResumen.TabIndex = 0;
            btnResumen.Text = "Resumen";
            btnResumen.UseVisualStyleBackColor = false;
            //
            // panelContenido
            //
            panelContenido.BackColor = Color.FromArgb(244, 246, 251);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(240, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Padding = new Padding(32, 28, 32, 24);
            panelContenido.Size = new Size(1060, 800);
            panelContenido.TabIndex = 1;
            //
            // FormPrincipal
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1300, 800);
            Controls.Add(panelContenido);
            Controls.Add(panelLateral);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1280, 760);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Finanzas";
            panelLateral.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLateral;
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
