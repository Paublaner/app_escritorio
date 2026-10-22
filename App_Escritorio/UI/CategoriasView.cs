using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driving;

namespace App_Escritorio.UI
{
    public partial class CategoriasView : UserControl, IVista
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public CategoriasView(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;

            InitializeComponent();

            Estilo.Grid(gridCategorias);
            Estilo.BotonPrimario(btnCrear);
            Estilo.BotonPeligro(btnEliminar);

            var tipos = Enum.GetValues<Categoria.TipoMovimiento>()
                .Select(t => new Opcion<Categoria.TipoMovimiento>(t, Textos.Tipo(t)))
                .ToList();

            cmbTipo.DisplayMember = nameof(Opcion<Categoria.TipoMovimiento>.Texto);
            cmbTipo.ValueMember = nameof(Opcion<Categoria.TipoMovimiento>.Valor);
            cmbTipo.DataSource = tipos;

            // En el filtro, null significa "todas"
            cmbFiltro.DisplayMember = nameof(Opcion<Categoria.TipoMovimiento?>.Texto);
            cmbFiltro.DataSource = new List<Opcion<Categoria.TipoMovimiento?>> { new(null, "Todas") }
                .Concat(tipos.Select(t => new Opcion<Categoria.TipoMovimiento?>(t.Valor, t.Texto)))
                .ToList();
        }

        public async Task CargarAsync()
        {
            var filtro = (cmbFiltro.SelectedItem as Opcion<Categoria.TipoMovimiento?>)?.Valor;

            var categorias = filtro is { } tipo
                ? await _categoriaServicio.ObtenerPorTipoAsync(tipo)
                : await _categoriaServicio.ObtenerTodasAsync();

            if (!Mensajes.Comprobar(categorias, this))
            {
                return;
            }

            gridCategorias.DataSource = categorias.Value
                .OrderBy(c => c.Tipo)
                .ThenBy(c => c.Nombre)
                .Select(c => new FilaCategoria { Id = c.Id, Nombre = c.Nombre, Tipo = Textos.Tipo(c.Tipo) })
                .ToList();
        }

        private async void btnCrear_Click(object? sender, EventArgs e)
        {
            var resultado = await _categoriaServicio.CrearAsync(txtNombre.Text, (Categoria.TipoMovimiento)cmbTipo.SelectedValue!);

            if (!Mensajes.Comprobar(resultado, this))
            {
                return;
            }

            txtNombre.Clear();
            await CargarAsync();
        }

        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (gridCategorias.CurrentRow?.DataBoundItem is not FilaCategoria fila
                || !Mensajes.Confirmar(this, $"¿Eliminar la categoría \"{fila.Nombre}\"?"))
            {
                return;
            }

            var resultado = await _categoriaServicio.EliminarAsync(fila.Id);

            if (Mensajes.Comprobar(resultado, this))
            {
                await CargarAsync();
            }
        }

        private async void cmbFiltro_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            await CargarAsync();
        }
    }
}
