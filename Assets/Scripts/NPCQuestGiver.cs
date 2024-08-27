using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCQuestGiver : MonoBehaviour
{
    public GameObject questSymbol; // El simbolo de la mision encima del NPC
    public TextMeshProUGUI questText; // El texto que explica la mision
    public Button interactButton; // Boton para interactuar y mostrar el mensaje de felicitacion
    public GameObject interactionButton; // El boton para interactuar
    public Transform portalA; // La posicion del portal A
    public Transform player; // Referencia al jugador
    public TextMeshProUGUI distanceText; // Texto que muestra la distancia restante
    public Button acceptQuestButton; // Boton para aceptar la mision
    public GameObject missionIndicator; // Indicador de mision en la pantalla

    private bool isQuestActive = false; // Estado de la mision (activa o no)
    private bool isQuestCompleted = false; // Estado de la mision (completada o no)

    void Start()
    {
        interactionButton.SetActive(false); // Ocultar el boton de interaccion al inicio
        questText.gameObject.SetActive(false); // Ocultar el texto de la mision al inicio
        distanceText.gameObject.SetActive(false); // Ocultar el texto de seguimiento al inicio
        questSymbol.SetActive(true); // Mostrar el simbolo de mision disponible
    }

    void Update()
    {
        // Mostrar el boton de interaccion si el jugador esta cerca del NPC
        if (Vector3.Distance(player.position, transform.position) < 3f && !isQuestCompleted)
        {
            interactionButton.SetActive(true);
        }
        else
        {
            interactionButton.SetActive(false);
        }

        // Si la mision esta activa, actualizar el texto de seguimiento
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
            questText.gameObject.SetActive(true); // Mostrar el texto de la mision
            acceptQuestButton.gameObject.SetActive(true); // Mostrar el boton de aceptar mision
        }
        else if (isQuestCompleted)
        {
            questText.gameObject.SetActive(true);
            questText.text = "Buen trabajo! Has completado la mision."; // Texto de felicitacion
            acceptQuestButton.gameObject.SetActive(false); // Ocultar el boton de aceptar mision
            interactButton.gameObject.SetActive(false); // Ocultar el boton de interactuar

            missionIndicator.SetActive(false); // Ocultar el indicador de mision completada
        }
    }
    
    public void AcceptQuest()
    {
        isQuestActive = true;
        questText.gameObject.SetActive(false); // Ocultar el texto de la mision
        acceptQuestButton.gameObject.SetActive(false); // Ocultar el boton de aceptar mision
        distanceText.gameObject.SetActive(true); // Mostrar el texto de seguimiento
        
        missionIndicator.SetActive(false); // Desactivar el indicador de mision
    }

    private void CompleteQuest()
    {
        isQuestCompleted = true;
        isQuestActive = false;
        distanceText.color = Color.green; // Cambiar el color del texto a verde
        questSymbol.GetComponent<Image>().color = Color.green; // Cambiar el color del simbolo a verde
        distanceText.text = "Mision completada"; // Actualizar el texto de seguimiento

        missionIndicator.GetComponent<Image>().color = Color.green; // Cambiar el color del indicador a verde
        missionIndicator.SetActive(true); // Reactivar el indicador en la pantalla

        interactButton.gameObject.SetActive(true); // Mostrar el boton de interaccion debajo del texto de mision completada
        interactButton.transform.SetParent(distanceText.transform, false); // Asegurar que el boton este debajo del texto
    }

}