using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objetivo que la camara seguira
    public Vector3 offset = new Vector3(0, 5, -10); // Offset para ajustar la posicion de la camara respecto al objetivo
    public float rotationSpeed = 100.0f; // Velocidad de rotacion de la camara basada en la entrada del mouse

    private void LateUpdate()
    {
        if (target != null)
        {
            // Rotacion basada en la entrada del mouse
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            // Aplica la rotacion
            offset = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * offset;

            // Reposiciona la camara
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
