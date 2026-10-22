using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driving;

namespace App_Escritorio.UI
{
    public partial class PresupuestosView : UserControl, IVista
    {
        private readonly IPresupuestoServicio _presupuestoServicio;
        private readonly ICategoriaServicio _categoriaServicio;

        private List<Categoria> _categorias = [];

        public PresupuestosView(IPresupuestoServicio presupuestoServicio, ICategoriaServicio categoriaServicio)
        {
            _presupuestoServicio = presupuestoServicio;
            _categoriaServicio = categoriaServicio;

            InitializeComponent();

            Estilo.Grid(gridPresupuestos);
            gridPresupuestos.CellFormatting += Estilo.ColorearPresupuesto;
            Estilo.BotonPrimario(btnDefinir);
        }

        public async Task CargarAsync()
        {
            var categorias = await _categoriaServicio.ObtenerPorTipoAsync(Categoria.TipoMovimiento.Gasto);

            if (!Mensajes.Comprobar(categorias, this))
            {
                return;
            }

            _categorias = categorias.Value;

            Combos.Rellenar(cmbCategoria, _categorias
                .OrderBy(c => c.Nombre)
                .Select(c => new Opcion<int>(c.Id, c.Nombre))
                .ToList());

            await CargarPresupuestosAsync();
        }

        private async Task CargarPresupuestosAsync()
        {
            var filas = await Consultas.FilasPresupuestosAsync(_presupuestoServicio, _categorias, dtpMes.Value.Year, dtpMes.Value.Month);

            if (!Mensajes.Comprobar(filas, this))
            {
                return;
            }

            gridPresupuestos.DataSource = filas.Value;
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Limite), "C2", true);
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Gastado), "C2", true);
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Restante), "C2", true);
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Uso), "P0", true);

            lblTotal.Text = $"Presupuestado: {filas.Value.Sum(f => f.Limite):C2}     Gastado: {filas.Value.Sum(f => f.Gastado):C2}";
        }

        private async void btnDefinir_Click(object? sender, EventArgs e)
        {
            if (cmbCategoria.SelectedItem is not Opcion<int> categoria)
            {
                Mensajes.Aviso(this, "Crea primero una categoría de gasto");
                return;
            }

            var resultado = await _presupuestoServicio.DefinirAsync(categoria.Valor, numLimite.Value, dtpMes.Value.Year, dtpMes.Value.Month);

            if (!Mensajes.Comprobar(resultado, this))
            {
                return;
            }

            numLimite.Value = 0;
            await CargarPresupuestosAsync();
        }

        // Al seleccionar un presupuesto se cargan sus datos para poder cambiar el límite
        private void gridPresupuestos_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridPresupuestos.CurrentRow?.DataBoundItem is FilaPresupuesto fila && gridPresupuestos.Focused)
            {
                cmbCategoria.SelectedValue = fila.CategoriaId;
                numLimite.Value = fila.Limite;
            }
        }

        private async void dtpMes_ValueChanged(object? sender, EventArgs e)
        {
            await CargarPresupuestosAsync();
        }
    }
}
