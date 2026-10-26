using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace App_Escritorio.UI;

// Modo claro u oscuro: recuerda la elección del usuario y recolorea los controles
public static class Tema
{
    private static readonly string Archivo = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Finanzas", "tema.txt");

    public static bool Oscuro { get; private set; } = Cargar();

    public static event Action? Cambiado;

    public static void Alternar()
    {
        Oscuro = !Oscuro;
        Guardar();
        Cambiado?.Invoke();
    }

    // Recorre el control y todos sus hijos dándoles los colores del tema activo
    public static void Aplicar(Control control)
    {
        switch (control)
        {
            case DataGridView grid:
                Estilo.ColoresGrid(grid);
                break;
            case Button boton:
                Estilo.ColoresBoton(boton);
                break;
            // Antes que Label: la burbuja es un Label que pinta su propio fondo
            case BurbujaChat burbuja:
                burbuja.ActualizarTema();
                break;
            case Label etiqueta:
                etiqueta.ForeColor = Estilo.ColorEtiqueta(etiqueta);
                break;
            case ComboBox or TextBox or NumericUpDown:
                control.BackColor = Estilo.Superficie;
                control.ForeColor = Estilo.Texto;
                TemaNativo(control);
                break;
            case SelectorFecha fecha:
                fecha.ActualizarTema();
                TemaNativo(control);
                break;
            case ScrollBar:
                TemaNativo(control);
                break;
            case GraficoCircular grafico:
                grafico.BackColor = Estilo.Superficie;
                grafico.ForeColor = Estilo.Texto;
                grafico.Invalidate();
                break;
            // Las tarjetas del resumen son paneles con borde
            case Panel { BorderStyle: BorderStyle.FixedSingle }:
                control.BackColor = Estilo.Superficie;
                control.ForeColor = Estilo.Texto;
                break;
            default:
                control.BackColor = Estilo.Fondo;
                control.ForeColor = Estilo.Texto;
                break;
        }

        foreach (Control hijo in control.Controls)
        {
            Aplicar(hijo);
        }
    }

    // Barra de título del color del tema. El modo oscuro de la barra existe desde Windows 10 20H1,
    // pero no se repinta al cambiarlo; el color propio (Windows 11) sí se aplica al momento
    public static void BarraTitulo(Form form)
    {
        if (!form.IsHandleCreated)
        {
            return;
        }

        int oscuro = Oscuro ? 1 : 0;
        int fondo = ColorTranslator.ToWin32(Estilo.Fondo);
        int texto = ColorTranslator.ToWin32(Estilo.Texto);

        DwmSetWindowAttribute(form.Handle, DwmUsarModoOscuro, ref oscuro, sizeof(int));
        DwmSetWindowAttribute(form.Handle, DwmColorBarra, ref fondo, sizeof(int));
        DwmSetWindowAttribute(form.Handle, DwmColorTextoBarra, ref texto, sizeof(int));
    }

    // Combos, barras de desplazamiento, etc. los dibuja Windows: se les pide su versión oscura
    private static void TemaNativo(Control control)
    {
        control.HandleCreated -= AlCrearHandle;
        control.HandleCreated += AlCrearHandle;

        if (control.IsHandleCreated)
        {
            AlCrearHandle(control, EventArgs.Empty);
        }
    }

    private static void AlCrearHandle(object? sender, EventArgs e)
    {
        if (sender is not Control control)
        {
            return;
        }

        // DarkMode_CFD es el tema de los cuadros de diálogo de archivos: combos y cajas de texto oscuros
        string? tema = !Oscuro ? null : control is ComboBox or TextBox ? "DarkMode_CFD" : "DarkMode_Explorer";
        SetWindowTheme(control.Handle, tema, null);

        // Las flechas de un DateTimePicker con ShowUpDown son una ventana hija de Windows
        IntPtr flechas = FindWindowEx(control.Handle, IntPtr.Zero, "msctls_updown32", null);

        if (flechas != IntPtr.Zero)
        {
            SetWindowTheme(flechas, Oscuro ? "DarkMode_Explorer" : null, null);
        }

        control.Invalidate(true);
    }

    private static bool Cargar()
    {
        try
        {
            if (File.Exists(Archivo))
            {
                return File.ReadAllText(Archivo).Trim() == "oscuro";
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Si no se puede leer, se sigue el tema de Windows
        }

        // Sin preferencia guardada, el mismo tema que las aplicaciones de Windows
        using var clave = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        return clave?.GetValue("AppsUseLightTheme") is int claro && claro == 0;
    }

    private static void Guardar()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Archivo)!);
            File.WriteAllText(Archivo, Oscuro ? "oscuro" : "claro");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // No poder recordar la preferencia no impide usar la aplicación
        }
    }

    private const int DwmUsarModoOscuro = 20;
    private const int DwmColorBarra = 35;
    private const int DwmColorTextoBarra = 36;

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamano);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hwnd, string? subApp, string? subIdList);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr FindWindowEx(IntPtr padre, IntPtr despuesDe, string clase, string? titulo);
}
