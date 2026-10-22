namespace App_Escritorio.UI;

public static class DialogoTexto
{
    // Pide un texto al usuario; devuelve null si cancela
    public static string? Pedir(IWin32Window owner, string titulo, string etiqueta, string valorInicial)
    {
        using var form = new Form
        {
            Text = titulo,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            StartPosition = FormStartPosition.CenterParent,
            MinimizeBox = false,
            MaximizeBox = false,
            ShowInTaskbar = false,
            ClientSize = new Size(360, 130),
            Font = new Font("Segoe UI", 10F)
        };

        var lblEtiqueta = new Label { Text = etiqueta, Location = new Point(16, 16), AutoSize = true };
        var txtValor = new TextBox { Text = valorInicial, Location = new Point(16, 42), Width = 328 };
        var btnAceptar = new Button { Text = "Aceptar", DialogResult = DialogResult.OK, Location = new Point(164, 86), Size = new Size(88, 32) };
        var btnCancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Location = new Point(256, 86), Size = new Size(88, 32) };

        form.Controls.AddRange([lblEtiqueta, txtValor, btnAceptar, btnCancelar]);
        form.AcceptButton = btnAceptar;
        form.CancelButton = btnCancelar;

        return form.ShowDialog(owner) == DialogResult.OK ? txtValor.Text : null;
    }
}
