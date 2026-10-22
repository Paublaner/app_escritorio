namespace App_Escritorio.UI;

public static class Combos
{
    // Rellena el combo manteniendo la opción seleccionada si sigue existiendo
    public static void Rellenar<T>(ComboBox combo, List<Opcion<T>> opciones)
    {
        var anterior = combo.SelectedItem as Opcion<T>;

        combo.DisplayMember = nameof(Opcion<T>.Texto);
        combo.ValueMember = nameof(Opcion<T>.Valor);
        combo.DataSource = opciones;

        if (anterior is not null)
        {
            int indice = opciones.FindIndex(o => EqualityComparer<T>.Default.Equals(o.Valor, anterior.Valor));

            if (indice >= 0)
            {
                combo.SelectedIndex = indice;
            }
        }
    }
}
