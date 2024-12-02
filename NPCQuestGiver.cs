using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCQuestGiver : MonoBehaviour
{
    public GameObject questSymbol; // El símbolo de la misión encima del NPC
    public TextMeshProUGUI questText; // El texto que explica la misión
    public Button interactButton; // Botón para interactuar
    public GameObject interactionButton; // El botón para interactuar
    public Transform portalA; // La posición del portal A
    public Transform player; // Referencia al jugador
    public TextMeshProUGUI distanceText; // Texto que muestra la distancia restante
    public Button acceptQuestButton; // Botón para aceptar la misión
    public MissionIndicator missionIndicator; // Indicador de misión en la pantalla
    public Image missionIndicatorIcon; // Icono del indicador del NPC

    private bool isQuestActive = false; // Estado de la misión (activa o no)
    private bool isQuestCompleted = false; // Estado de la misión (completada o no)

    void Start()
    {
        interactionButton.SetActive(false); // Ocultar el botón de interacción al inicio
        questText.gameObject.SetActive(false); // Ocultar el texto de la misión al inicio
        distanceText.gameObject.SetActive(false); // Ocultar el texto de seguimiento al inicio
        questSymbol.SetActive(true); // Mostrar el símbolo de misión disponible
        if (missionIndicator != null) missionIndicator.ShowIndicator(); // Asegurar que el indicador esté visible al inicio
    }
    public void EnableQuest()
    {
        Debug.Log("Misión activada desde el tablero.");
        questSymbol.SetActive(true); // Mostrar el símbolo de misión del NPC
    }
    void Update()
    {
        // Mostrar el botón de interacción si el jugador está cerca del NPC
        if (Vector3.Distance(player.position, transform.position) < 3f)
        {
            interactionButton.SetActive(true);
        }
        else
        {
            interactionButton.SetActive(false);
        }

        // Si la misión está activa, actualizar el texto de seguimiento
        if (isQuestActive && !isQuestCompleted)
        {
            float distanceToPortal = Vector3.Distance(player.position, portalA.position);
            distanceText.text = "Distancia al portal: " + distanceToPortal.ToString("F1") + " metros";

            // Verificar si el jugador ha llegado al portal A
            if (distanceToPortal < 2f)
            {
                CompleteQuest();
            }
        }
    }

    public void OnInteract()
    {
        if (!isQuestActive && !isQuestCompleted)
        {
            // Mostrar el texto de la misión
            questText.gameObject.SetActive(true);
            acceptQuestButton.gameObject.SetActive(true); // Mostrar el botón de aceptar misión
        }
        else if (isQuestCompleted)
        {
            // Mostrar el mensaje de felicitación
            questText.gameObject.SetActive(true);
            questText.text = "¡Buen trabajo! Has completado la misión. ¡Gracias por tu ayuda!"; // Texto de felicitación
            acceptQuestButton.gameObject.SetActive(false); // Ocultar el botón de aceptar misión
            interactButton.gameObject.SetActive(false); // Ocultar el botón de interactuar

            // Ocultar el indicador verde
            if (missionIndicator != null)
            {
                missionIndicator.HideIndicator();
            }

            // Iniciar corrutina para ocultar el texto después de 5 segundos
            StartCoroutine(HideCompletionText());
        }
    }

    public void AcceptQuest()
    {
        Debug.Log("Misión aceptada."); // Depuración
        isQuestActive = true;
        questText.gameObject.SetActive(false); // Ocultar el texto de la misión
        acceptQuestButton.gameObject.SetActive(false); // Ocultar el botón de aceptar misión
        distanceText.gameObject.SetActive(true); // Mostrar el texto de seguimiento

        // Ocultar el indicador de misión
        if (missionIndicator != null)
        {
            missionIndicator.HideIndicator();
        }
    }
    

    private void CompleteQuest()
    {
        isQuestCompleted = true;
        isQuestActive = false;

        // Cambiar el color y texto
        distanceText.color = Color.green; // Cambiar el color del texto a verde
        distanceText.text = "¡Misión completada!"; // Actualizar el texto de seguimiento
        Debug.Log("Misión completada."); // Depuración adicional

        // Cambiar el color del icono del indicador a verde y mostrarlo
        if (missionIndicatorIcon != null)
        {
            missionIndicatorIcon.color = Color.green; // Cambiar el color del icono
        }
        if (missionIndicator != null)
        {
            missionIndicator.ShowIndicator();
        }
    }

    private IEnumerator HideCompletionText()
    {
        yield return new WaitForSeconds(5); // Esperar 5 segundos
        questText.gameObject.SetActive(false); // Ocultar el texto de felicitación
        Debug.Log("Texto de felicitación ocultado."); // Confirmación adicional
    }
}
