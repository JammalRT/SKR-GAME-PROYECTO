using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeAdvanceController : MonoBehaviour
{
    public Scrollbar hourScrollbar; // Scrollbar para seleccionar cuantas horas avanzar
    public Button confirmButton; // Boton para confirmar el avance de tiempo
    public TextMeshProUGUI hourText; // Texto que muestra la cantidad de horas a avanzar
    public TimeController timeController; // Referencia al script que controla el tiempo del juego
    public GameObject player; // Referencia al jugador para desactivar su movimiento
    public float timeScaleMultiplier = 100f; // Multiplicador para acelerar el tiempo

    public bool isAdvancingTime = false; // Indica si el tiempo esta avanzando rapidamente
    private int hoursToAdvance; // Cantidad de horas a avanzar
    private float targetTimeOfDay; // Hora objetivo a alcanzar
    private float originalTimeScale; // Escala de tiempo original

    void Start()
    {
        // Asignar la funcion OnConfirmButtonClicked al evento onClick del boton de confirmacion
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        originalTimeScale = Time.timeScale; // Guardar la escala de tiempo original
    }

    void Update()
    {
        if (isAdvancingTime)
        {
            // Acelerar el tiempo ajustando la escala de tiempo global
            Time.timeScale = timeScaleMultiplier;

            // Incrementar `timeOfDay` gradualmente
            timeController.timeOfDay += Time.deltaTime / (originalTimeScale / timeScaleMultiplier);

            // Verificar si hemos alcanzado la hora objetivo
            if (timeController.timeOfDay >= targetTimeOfDay)
            {
                StopTimeAdvance();
            }
        }
        else
        {
            // Mostrar la cantidad de horas seleccionadas en la UI
            hoursToAdvance = Mathf.RoundToInt(hourScrollbar.value * 24);
            hourText.text = "Avanzar " + hoursToAdvance.ToString() + " horas";
        }
    }

    void OnConfirmButtonClicked()
    {
        if (hoursToAdvance > 0)
        {
            // Desactivar el movimiento del jugador
            player.GetComponent<PlayerMovement>().enabled = false;

            // Calcular la hora objetivo
            targetTimeOfDay = timeController.timeOfDay + hoursToAdvance * 60;
            if (targetTimeOfDay >= 1440f) targetTimeOfDay -= 1440f;

            // Iniciar el avance del tiempo
            isAdvancingTime = true;

            // Cambiar el texto del boton (opcional)
            confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = "Avanzando...";
        }
    }

    void StopTimeAdvance()
    {
        // Detener el avance del tiempo
        isAdvancingTime = false;

        // Restaurar la escala de tiempo original
        Time.timeScale = originalTimeScale;

        // Asegurar que `timeOfDay` sea igual a `targetTimeOfDay`
        timeController.timeOfDay = targetTimeOfDay;

        // Rehabilitar el movimiento del jugador
        player.GetComponent<PlayerMovement>().enabled = true;

        // Cambiar el texto del boton de vuelta a "Confirmar"
        confirmButton.GetComponentInChildren<TextMeshProUGUI>().text = "Confirmar";
    }
}
