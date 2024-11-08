using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioJugador : MonoBehaviour
{
    public List<string> inventario = new List<string>(); // Inventario de menas del jugador
    public int lingotes = 0; // Cantidad de lingotes que posee el jugador
    public Text textoInventario; // Texto UI para mostrar la cantidad de menas
    public Text textoLingotes; // Texto UI para mostrar la cantidad de lingotes
    public NivelJugador nivelJugador; // Referencia al componente PlayerLevel

    void Start()
    {
        ActualizarUIInventario(); // Inicializar el texto del inventario
        ActualizarUILingotes(); // Inicializar el texto de los lingotes
    }

    // Añadir mena al inventario
    public void AgregarMena(string nombreMena)
    {
        inventario.Add(nombreMena);
        Debug.Log("Mena añadida al inventario: " + nombreMena);
        ActualizarUIInventario(); // Llamar a la actualización de la UI después de añadir la mena
    }

    // Añadir lingotes al inventario
    public void AgregarLingotes(int cantidadLingotes)
    {
        lingotes += cantidadLingotes;
        ActualizarUILingotes();
    }

    // Añadir experiencia al jugador
    public void AgregarExperiencia(int experiencia)
    {
        if (nivelJugador != null)
        {
            nivelJugador.AñadirExperiencia(experiencia);
        }
    }

    // Actualizar la UI del inventario de menas
    public void ActualizarUIInventario()
    {
        if (textoInventario != null)
        {
            textoInventario.text = "Menas obtenidas: " + inventario.Count;
        }
    }

    // Actualizar la UI de los lingotes
    public void ActualizarUILingotes()
    {
        if (textoLingotes != null)
        {
            textoLingotes.text = "Lingotes: " + lingotes;
        }
    }
}


