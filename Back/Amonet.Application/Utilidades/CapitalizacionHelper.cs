using System.Globalization;
using System.Text;

namespace Amonet.Application.Utilidades;

public static class CapitalizacionHelper
{
    /// <summary>
    /// Capitaliza un texto según las reglas de la RAE:
    /// - Primera letra en mayúscula
    /// - Resto en minúsculas
    /// - Respeta espacios y signos de puntuación
    /// </summary>
    public static string CapitalizarSegunRAE(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return texto;

        var textoInfo = new CultureInfo("es-ES", false).TextInfo;
        var palabras = texto.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        
        var resultado = new StringBuilder();
        
        for (int i = 0; i < palabras.Length; i++)
        {
            if (i > 0)
                resultado.Append(' ');
            
            var palabra = palabras[i].Trim();
            if (palabra.Length > 0)
            {
                // Capitalizar primera letra, resto en minúscula
                resultado.Append(char.ToUpper(palabra[0], CultureInfo.GetCultureInfo("es-ES")));
                if (palabra.Length > 1)
                {
                    resultado.Append(palabra.Substring(1).ToLower(CultureInfo.GetCultureInfo("es-ES")));
                }
            }
        }
        
        return resultado.ToString();
    }
}

