using UnityEngine;
using UnityEngine.UI;

public class NivelJugador : MonoBehaviour
{
    public int nivelActual = 1; // Nivel inicial del jugador
    public int experienciaActual = 0; // Experiencia actual
    public int experienciaParaProximoNivel = 5; // Experiencia necesaria para el pr�ximo nivel
    public Text textoNivel; // Texto UI para mostrar el nivel del jugador
    public Text textoExperiencia; // Texto UI para mostrar la experiencia del jugador

    void Start()
    {
        ActualizarUINivel(); // Inicializar la UI del nivel
    }

    // M�todo para a�adir experiencia al personaje
    public void A�adirExperiencia(int cantidadExperiencia)
    {
        experienciaActual += cantidadExperiencia;
        Debug.Log("Experiencia a�adida: " + cantidadExperiencia);

        // Revisar si se ha alcanzado suficiente experiencia para subir de nivel
        if (experienciaActual >= experienciaParaProximoNivel)
        {
            SubirDeNivel();
        }

        ActualizarUINivel(); // Actualizar la UI despu�s de agregar experiencia
    }

    // M�todo para subir de nivel al personaje
    void SubirDeNivel()
    {
        nivelActual++;
        experienciaActual -= experienciaParaProximoNivel; // Restar la experiencia necesaria para el nivel anterior
        experienciaParaProximoNivel = Mathf.CeilToInt(experienciaParaProximoNivel * 1.5f); // Incrementar la experiencia necesaria para el siguiente nivel
        Debug.Log("Has subido de nivel, Nivel actual: " + nivelActual);
    }

    // Actualizar la UI para reflejar el nivel y la experiencia
    void ActualizarUINivel()
    {
        if (textoNivel != null)
        {
            textoNivel.text = "Nivel: " + nivelActual;
        }

        if (textoExperiencia != null)
        {
            textoExperiencia.text = "Experiencia: " + experienciaActual + "/" + experienciaParaProximoNivel;
        }
    }
}

