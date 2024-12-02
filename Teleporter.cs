using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa el espacio de nombres para TextMeshPro

public class Teleporter : MonoBehaviour
{
    public Transform destinationTransform;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TMP_Text loadingText; // Cambiar a TMP_Text
    public PlayerMovement playerMovement;
    public float loadingDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithLoading(other.gameObject));
        }
    }

    private IEnumerator TeleportWithLoading(GameObject player)
    {
        // Mostrar pantalla de carga
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // Desactivar movimiento del jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        float elapsedTime = 0f;

        // Barra de carga y porcentaje
        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadingDuration);
            if (loadingBar != null)
            {
                loadingBar.value = progress; // Actualiza el slider
            }
            if (loadingText != null)
            {
                loadingText.text = Mathf.RoundToInt(progress * 100f) + "%"; // Actualiza el texto
            }
            yield return null;
        }

        // Teletransportar al jugador
        player.transform.position = destinationTransform.position;
        player.transform.rotation = destinationTransform.rotation;

        // Ocultar pantalla de carga
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }

        // Reactivar movimiento del jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }
}
