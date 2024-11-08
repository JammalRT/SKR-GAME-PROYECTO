using UnityEngine;
using UnityEngine.UI;

public class ClickToShowSlider : MonoBehaviour
{
    public Slider timeSlider; // Arrastra el Slider desde el Inspector
    public float countdownTime = 5f; // Duración de la cuenta regresiva en segundos
    public float activationDistance = 5f; // Distancia para activar el Slider
    public Transform player; // Referencia al jugador u objeto que se mueve

    private float currentTime;
    private bool isCountingDown = false;

    void Start()
    {
        if (timeSlider != null)
        {
            timeSlider.maxValue = countdownTime;
            timeSlider.value = 0;
            timeSlider.gameObject.SetActive(false); // Ocultar el Slider al inicio
        }
    }

    void Update()
    {
        // Verificar la distancia entre el jugador y el objeto
        if (Vector3.Distance(player.position, transform.position) <= activationDistance)
        {
            timeSlider.gameObject.SetActive(true); // Mostrar el Slider si el jugador está cerca
        }
        else
        {
            timeSlider.gameObject.SetActive(false); // Ocultar el Slider si el jugador se aleja
        }

        // Revisar si se está ejecutando la cuenta regresiva
        if (isCountingDown)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                timeSlider.value = countdownTime - currentTime;
            }
            else
            {
                // Cuando se acaba el tiempo, detener la cuenta regresiva
                isCountingDown = false;
                timeSlider.value = countdownTime;
            }
        }
    }

    void OnMouseDown()
    {
        // Comenzar la cuenta regresiva al hacer clic en el objeto
        if (!isCountingDown)
        {
            isCountingDown = true;
            currentTime = countdownTime;
            timeSlider.value = 0;
        }
    }
}
