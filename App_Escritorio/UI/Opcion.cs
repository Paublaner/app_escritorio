namespace App_Escritorio.UI;

// Elemento de un ComboBox: el valor que se usa en código y el texto que ve el usuario
public record Opcion<T>(T Valor, string Texto);
