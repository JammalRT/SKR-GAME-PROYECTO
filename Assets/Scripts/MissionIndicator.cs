using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionIndicator : MonoBehaviour
{
    public Transform targetNPC; // El NPC que da la mision
    public RectTransform indicatorIcon; // El icono de la mision en la pantalla
    public Camera mainCamera; // La camara principal
    public float edgeBuffer = 50f; // Distancia del borde de la pantalla

    void Update()
    {
        // Obtener la posicion del NPC en la pantalla
        Vector3 screenPos = mainCamera.WorldToScreenPoint(targetNPC.position);

        // Si el NPC esta detras de la camara, ajustar el indicador para que apunte en la direccion correcta
        if (screenPos.z < 0)
        {
            screenPos *= -1;
        }

        // Mantener el indicador dentro de los bordes de la pantalla
        screenPos.x = Mathf.Clamp(screenPos.x, edgeBuffer, Screen.width - edgeBuffer);
        screenPos.y = Mathf.Clamp(screenPos.y, edgeBuffer, Screen.height - edgeBuffer);

        // Actualizar la posicion del icono
        indicatorIcon.position = screenPos;
    }
}