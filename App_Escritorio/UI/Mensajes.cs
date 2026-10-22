using FluentResults;

namespace App_Escritorio.UI;

public static class Mensajes
{
    // Devuelve true si el resultado es correcto; si no, muestra los errores al usuario
    public static bool Comprobar(IResultBase resultado, IWin32Window owner)
    {
        if (resultado.IsSuccess)
        {
            return true;
        }

        Aviso(owner, string.Join(Environment.NewLine, resultado.Errors.Select(e => e.Message)));
        return false;
    }

    public static void Aviso(IWin32Window owner, string mensaje)
    {
        MessageBox.Show(owner, mensaje, "No se ha podido completar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static bool Confirmar(IWin32Window owner, string mensaje)
    {
        return MessageBox.Show(owner, mensaje, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}
