using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadMineria : MonoBehaviour
{
    public string Nombre;
    public int NivelMineria = 1;
    public int ExperienciaMineria = 0;
    public int ExperienciaParaSubir = 100;

    void Start()
    {
        Nombre = "Abraham";
    }

    void Update()
    {
        // Puedes añadir lógica aquí si deseas ejecutar algo cada frame
    }

    public void PicarMineral()
    {
        Debug.Log($"{Nombre} ha picado un mineral.");
        int experienciaGanada = 20; // Experiencia ganada al picar un mineral
        ExperienciaMineria += experienciaGanada;
        Debug.Log($"Ganaste {experienciaGanada} puntos de experiencia en minería.");

        // Verificar si el personaje ha ganado suficiente experiencia para subir de nivel
        if (ExperienciaMineria >= ExperienciaParaSubir)
        {
            SubirNivelMineria();
        }
        else
        {
            Debug.Log($"Experiencia actual: {ExperienciaMineria}/{ExperienciaParaSubir}");
        }
    }

    private void SubirNivelMineria()
    {
        NivelMineria++;
        ExperienciaMineria -= ExperienciaParaSubir; // Restamos la experiencia usada para subir de nivel
        ExperienciaParaSubir += 50; // Aumentamos la experiencia necesaria para el próximo nivel
        Debug.Log($"{Nombre} ha subido al nivel {NivelMineria}");
        Debug.Log($"EXP: {ExperienciaMineria}/{ExperienciaParaSubir}");
    }
}
