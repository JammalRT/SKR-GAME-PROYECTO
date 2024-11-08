using UnityEngine;

public class SistemaCambio : MonoBehaviour
{
    public int lingotesPorIntercambio = 1; // N�mero de lingotes recibidos por cada intercambio
    public int menasRequeridas = 3; // N�mero de menas requeridas para un intercambio
    public InventarioJugador inventarioJugador; // Referencia al inventario del jugador
    public NivelJugador nivelJugador; // Referencia para otorgar experiencia
    public int recompensaExperiencia = 5; // Experiencia obtenida por intercambio exitoso
    private bool jugadorEnRango = false; // Verifica si el jugador est� en rango

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("Jugador en rango de la tienda.");
            jugadorEnRango = true;
        }
    }

    void OnTriggerExit(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("Jugador sali� de la tienda.");
            jugadorEnRango = false;
        }
    }

    void Update()
    {
        // Detecta la tecla de intercambio cuando el jugador est� cerca
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            IntentarIntercambio();
        }
    }

    void IntentarIntercambio()
    {
        if (inventarioJugador != null)
        {
            Debug.Log("Cantidad actual de menas en el inventario: " + inventarioJugador.inventario.Count);
            if (inventarioJugador.inventario.Count >= menasRequeridas)
            {
                for (int i = 0; i < menasRequeridas; i++)
                {
                    inventarioJugador.inventario.RemoveAt(0); // Remueve las menas necesarias
                }
                inventarioJugador.AgregarLingotes(lingotesPorIntercambio); // A�adir lingotes
                Debug.Log("Intercambio exitoso: Has obtenido " + lingotesPorIntercambio + " lingote(s).");
                inventarioJugador.ActualizarUIInventario();
                inventarioJugador.ActualizarUILingotes();

                if (nivelJugador != null)
                {
                    nivelJugador.A�adirExperiencia(recompensaExperiencia);
                }
            }
            else
            {
                Debug.Log("No tienes suficientes menas para realizar el intercambio.");
            }
        }
        else
        {
            Debug.Log("No se encontr� el inventario del jugador.");
        }
    }
}
