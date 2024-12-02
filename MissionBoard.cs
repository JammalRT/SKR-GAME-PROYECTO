using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionBoard : MonoBehaviour
{
    public GameObject missionMenu; // Canvas del menú de misiones
    public TMP_Text missionDetails; // Texto para mostrar detalles de la misión
    public Button acceptButton; // Botón para aceptar la misión
    public GameObject interactButton; // Botón de interacción
    public MissionIndicator npcIndicator; // Referencia al indicador del NPC

    private bool missionAccepted = false; // Estado de la misión

    void Start()
    {
        missionMenu.SetActive(false); // Ocultar menú al inicio
        interactButton.SetActive(false); // Ocultar botón de interacción
        if (npcIndicator != null)
        {
            npcIndicator.HideIndicator(); // Asegurarse de que el indicador esté oculto al inicio
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true); // Mostrar botón al acercarse
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(false); // Ocultar botón al alejarse
            missionMenu.SetActive(false); // Cerrar menú si está abierto
        }
    }

    public void OpenMissionMenu()
    {
        if (!missionAccepted)
        {
            missionMenu.SetActive(true);
            missionDetails.text = "Habla con el NPC para obtener información.";
            acceptButton.interactable = true;
        }
    }

    public void AcceptMission()
    {
        missionAccepted = true;
        missionMenu.SetActive(false); // Cerrar menú
        Debug.Log("Misión aceptada: Habla con el NPC.");

        // Activar el indicador del NPC
        if (npcIndicator != null)
        {
            npcIndicator.ShowIndicator();
        }
    }
}
