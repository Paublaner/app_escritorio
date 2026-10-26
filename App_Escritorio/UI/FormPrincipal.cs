using System.Drawing.Drawing2D;

namespace App_Escritorio.UI
{
    public partial class FormPrincipal : Form
    {
        private readonly Dictionary<Button, UserControl> _vistas;
        private readonly Dictionary<Button, string> _iconos;
        private Button _activo;

        public FormPrincipal(ResumenView resumen, CuentasView cuentas, MovimientosView movimientos, CategoriasView categorias, PresupuestosView presupuestos, ChatView chat)
        {
            InitializeComponent();

            _vistas = new Dictionary<Button, UserControl>
            {
                [btnResumen] = resumen,
                [btnCuentas] = cuentas,
                [btnMovimientos] = movimientos,
                [btnCategorias] = categorias,
                [btnPresupuestos] = presupuestos,
                [btnAsistente] = chat
            };

            _iconos = new Dictionary<Button, string>
            {
                [btnResumen] = Dibujo.IconoResumen,
                [btnCuentas] = Dibujo.IconoCuentas,
                [btnMovimientos] = Dibujo.IconoMovimientos,
                [btnCategorias] = Dibujo.IconoCategorias,
                [btnPresupuestos] = Dibujo.IconoPresupuestos,
                [btnAsistente] = Dibujo.IconoAsistente
            };

            foreach (var (boton, vista) in _vistas)
            {
                vista.Dock = DockStyle.Fill;
                boton.Click += Navegar_Click;
            }

            foreach (var boton in _vistas.Keys.Append(btnTema))
            {
                boton.Paint += PintarBotonMenu;
                boton.MouseEnter += (_, _) => boton.Invalidate();
                boton.MouseLeave += (_, _) => boton.Invalidate();
            }

            _activo = btnResumen;

            btnTema.Click += (_, _) => Tema.Alternar();
            Tema.Cambiado += AplicarTema;
            FormClosed += (_, _) => Tema.Cambiado -= AplicarTema;

            AplicarTema();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Tema.BarraTitulo(this);
        }

        private void AplicarTema()
        {
            panelLateral.BackColor = Estilo.Lateral;
            btnTema.Text = Tema.Oscuro ? "Modo claro" : "Modo oscuro";
            panelLateral.Invalidate(true);

            // Las vistas que no se están mostrando también, para que estén listas al navegar
            Tema.Aplicar(panelContenido);

            foreach (var vista in _vistas.Values)
            {
                Tema.Aplicar(vista);
            }

            Tema.BarraTitulo(this);
        }

        // Logo arriba, separador sobre el botón del tema y línea a la derecha que separa el menú del contenido
        private void panelLateral_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float escala = DeviceDpi / 96f;
            int x = (int)(24 * escala);
            int lado = (int)(40 * escala);
            var logo = new Rectangle(x, (int)(32 * escala), lado, lado);

            using (var forma = Dibujo.Redondeado(logo, 12 * escala))
            using (var degradado = Dibujo.Degradado(logo))
            {
                g.FillPath(degradado, forma);
            }

            using (var fuenteLogo = new Font("Segoe UI", 15F, FontStyle.Bold))
            {
                TextRenderer.DrawText(g, "€", fuenteLogo, logo, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            int xTexto = logo.Right + (int)(12 * escala);

            using (var fuenteNombre = new Font("Segoe UI", 14F, FontStyle.Bold))
            {
                TextRenderer.DrawText(g, "Finanzas", fuenteNombre, new Point(xTexto, logo.Top - (int)(3 * escala)), Estilo.Texto);
            }

            using (var fuenteLema = new Font("Segoe UI", 8.5F))
            {
                TextRenderer.DrawText(g, "Tu dinero, bajo control", fuenteLema, new Point(xTexto + (int)(2 * escala), logo.Top + (int)(23 * escala)), Estilo.TextoSecundario);
            }

            using (var fuenteSeccion = new Font("Segoe UI Semibold", 8F))
            {
                TextRenderer.DrawText(g, "MENÚ", fuenteSeccion, new Point(x, btnResumen.Top - (int)(24 * escala)), Estilo.TextoSecundario);
            }

            using var lapiz = new Pen(Estilo.Borde);
            int ySeparador = btnTema.Top - (int)(12 * escala);
            g.DrawLine(lapiz, x, ySeparador, panelLateral.Width - x, ySeparador);
            g.DrawLine(lapiz, panelLateral.Width - 1, 0, panelLateral.Width - 1, panelLateral.Height);
        }

        // Cada opción del menú: icono y texto, con una pastilla de degradado si es la vista activa
        private void PintarBotonMenu(object? sender, PaintEventArgs e)
        {
            if (sender is not Button boton)
            {
                return;
            }

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Estilo.Lateral);

            float escala = DeviceDpi / 96f;
            bool activo = boton == _activo;
            bool encima = boton.ClientRectangle.Contains(boton.PointToClient(Cursor.Position));
            var rect = new RectangleF(0, 0, boton.Width - 1, boton.Height - 1);

            using (var forma = Dibujo.Redondeado(rect, 10 * escala))
            {
                if (activo)
                {
                    using var degradado = Dibujo.Degradado(rect);
                    g.FillPath(degradado, forma);
                }
                else if (encima)
                {
                    using var pincel = new SolidBrush(Estilo.Hover);
                    g.FillPath(pincel, forma);
                }
            }

            var color = activo ? Color.White : encima ? Estilo.Texto : Estilo.TextoSecundario;
            string icono = boton == btnTema ? (Tema.Oscuro ? Dibujo.IconoSol : Dibujo.IconoLuna) : _iconos[boton];

            var zonaIcono = new Rectangle((int)(14 * escala), 0, (int)(24 * escala), boton.Height);
            Dibujo.Icono(g, icono, 12F, zonaIcono, color);

            using var fuente = new Font(activo ? "Segoe UI Semibold" : "Segoe UI", 10F);
            var zonaTexto = new Rectangle(zonaIcono.Right + (int)(12 * escala), 0, boton.Width, boton.Height);
            TextRenderer.DrawText(g, boton.Text, fuente, zonaTexto, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await MostrarAsync(btnResumen);
        }

        private async void Navegar_Click(object? sender, EventArgs e)
        {
            if (sender is Button boton)
            {
                await MostrarAsync(boton);
            }
        }

        private async Task MostrarAsync(Button boton)
        {
            var anterior = _activo;
            _activo = boton;
            anterior.Invalidate();
            boton.Invalidate();

            var vista = _vistas[boton];

            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(vista);

            // Quita el foco del botón lateral (si no, se dibuja el recuadro de foco) y lo pasa al primer campo de la vista
            ActiveControl = vista;

            // Cada vista recarga sus datos al mostrarse, así ve lo creado en las demás
            await ((IVista)vista).CargarAsync();
        }
    }
}
