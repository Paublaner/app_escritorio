namespace App_Escritorio.UI
{
    public partial class FormPrincipal : Form
    {
        private readonly Dictionary<Button, UserControl> _vistas;

        public FormPrincipal(ResumenView resumen, CuentasView cuentas, MovimientosView movimientos, CategoriasView categorias, PresupuestosView presupuestos)
        {
            InitializeComponent();

            _vistas = new Dictionary<Button, UserControl>
            {
                [btnResumen] = resumen,
                [btnCuentas] = cuentas,
                [btnMovimientos] = movimientos,
                [btnCategorias] = categorias,
                [btnPresupuestos] = presupuestos
            };

            foreach (var (boton, vista) in _vistas)
            {
                vista.Dock = DockStyle.Fill;
                boton.Click += Navegar_Click;
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
            foreach (var b in _vistas.Keys)
            {
                b.BackColor = b == boton ? Estilo.LateralActivo : Estilo.Lateral;
            }

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
