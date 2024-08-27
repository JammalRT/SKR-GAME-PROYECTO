using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destinationTransform; // La posicion de destino (Cilindro B)
    public float delayTime = 2.0f; // Tiempo de espera antes del teletransporte

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador (puedes usar etiquetas o capas para mas control)
        if (other.CompareTag("Player"))
        {
            // Inicia la rutina de teletransporte
            StartCoroutine(TeleportAfterDelay(other.gameObject));
        }
    }

    IEnumerator TeleportAfterDelay(GameObject teleportee)
    {
        // Espera el tiempo definido antes de teletransportar
        yield return new WaitForSeconds(delayTime);

        // Teletransporta el objeto a la posicion y rotacion del destino
        teleportee.transform.position = destinationTransform.position;
        teleportee.transform.rotation = destinationTransform.rotation;
    }
}