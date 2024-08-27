using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public int rotationScale = 10; // Escala de rotacion, determina la velocidad de rotacion del ciclo dia/noche

    void Update()
    {
        // Rota el objeto alrededor del eje X a una velocidad determinada por rotationScale
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
    }
}