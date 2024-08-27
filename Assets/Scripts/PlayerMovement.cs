using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.5f; // Velocidad de movimiento del jugador
    public float jumpForce = 5.5f; // Fuerza de salto del jugador
    public Transform camTransform; // Referencia a la camara para alinear el movimiento

    private Rigidbody rb; // Referencia al componente Rigidbody del jugador
    private bool isGrounded; // Bandera para verificar si el jugador esta en el suelo

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Inicializar el Rigidbody
        camTransform = Camera.main.transform; // Asignar la camara principal
    }

    void Update()
    {
        // Movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        if (movement.magnitude > 0)
        {
            Vector3 moveDirection = camTransform.forward * movement.z + camTransform.right * movement.x;
            moveDirection.y = 0;
            rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Verifica si esta en el suelo
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica si dejo el suelo
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
