using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI timeText; // Referencia al componente de texto para mostrar la hora
    public Light sun; // Referencia al objeto de luz que representa el sol
    public float secondsPerMinute = 1f; // Duracion de un minuto del juego en segundos reales
    public float sunriseHour = 6f; // Hora del amanecer (6 AM)
    public float sunsetHour = 20f; // Hora del atardecer (8 PM)
    public float timeOfDay = 6f * 60f; // Inicializar a las 6:00 AM

    void Update()
    {
        // Incrementar el tiempo del dia
        if (!FindObjectOfType<TimeAdvanceController>().isAdvancingTime)
        {
            timeOfDay += Time.deltaTime / secondsPerMinute * 60f;
        }

        // Asegurar que `timeOfDay` se mantenga en el rango de 0 a 1440 minutos (24 horas)
        if (timeOfDay >= 1440f) timeOfDay = 1440f;

        // Calcular la hora actual
        int hours = (int)(timeOfDay / 60f);
        int minutes = (int)(timeOfDay % 60f);

        // Formatear la hora para mostrarla en pantalla
        string timeString = string.Format("{0:00}:{1:00}", hours, minutes);

        // Mostrar la hora y el estado del dia
        timeText.text = timeString + "\n" + GetTimeOfDayMessage(hours, minutes);

        // Actualizar la rotacion del sol basada en la hora actual
        UpdateSunRotation(hours, minutes);
    }

    // Metodo para determinar el mensaje segun la hora
    string GetTimeOfDayMessage(int hours, int minutes)
    {
        if (hours == 6 && minutes < 30)
            return "Amanecer";

        if (hours == 20 && minutes < 30)
            return "Anochecer";

        if (hours >= 6 && hours < 20)
            return "Dia";

        return "Noche";
    }

    // Metodo para actualizar la rotacion del sol
    void UpdateSunRotation(int hours, int minutes)
    {
        // Solo rotar el sol entre las 6:00 AM y las 8:00 PM
        if (hours >= sunriseHour && hours < sunsetHour)
        {
            float t = (hours - sunriseHour) + (minutes / 60f);
            float sunAngle = Mathf.Lerp(0f, 180f, t / (sunsetHour - sunriseHour));
            sun.transform.rotation = Quaternion.Euler(sunAngle, 0, 0);
        }
        else
        {
            // Apaga la luz del sol durante la noche
            sun.transform.rotation = Quaternion.Euler(180f, 0, 0);
        }
    }
}