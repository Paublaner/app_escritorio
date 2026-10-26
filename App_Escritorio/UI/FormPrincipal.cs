namespace App_Escritorio.UI
{
    public partial class FormPrincipal : Form
    {
        private readonly Dictionary<Button, UserControl> _vistas;
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

            foreach (var (boton, vista) in _vistas)
            {
                vista.Dock = DockStyle.Fill;
                boton.Click += Navegar_Click;
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
            PintarMenu();

            btnTema.BackColor = Estilo.Lateral;
            btnTema.Text = Tema.Oscuro ? "Modo claro" : "Modo oscuro";

            // Las vistas que no se están mostrando también, para que estén listas al navegar
            Tema.Aplicar(panelContenido);

            foreach (var vista in _vistas.Values)
            {
                Tema.Aplicar(vista);
            }

            Tema.BarraTitulo(this);
        }

        private void PintarMenu()
        {
            foreach (var b in _vistas.Keys)
            {
                b.BackColor = b == _activo ? Estilo.LateralActivo : Estilo.Lateral;
            }
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
            _activo = boton;
            PintarMenu();

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
