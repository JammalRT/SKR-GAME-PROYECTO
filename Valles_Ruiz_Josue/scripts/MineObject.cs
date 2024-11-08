using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoMinado : MonoBehaviour
{
    public Slider sliderMinado; // Referencia al slider del Canvas
    public float tiempoMinado = 3f; // Tiempo necesario para minar en segundos.
    private bool estaMinando = false;
    private float progresoMinado = 0f;

    // Inventario global para almacenar las menas minadas
    public static List<string> inventario = new List<string>();

    // Variable para hacer referencia al texto UI
    public Text textoInventario;

    // Referencia al componente PlayerLevel del jugador
    public NivelJugador nivelJugador;
    public int expGanada = 1; // Experiencia ganada por cada mena minada

    void Start()
    {
        ActualizarUIInventario(); // Inicializar el texto del inventario
    }

    void Update()
    {
        // Detecta cuando nos acercamos y clickeamos sobre la mena
        if (Input.GetMouseButtonDown(0) && !estaMinando)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                IniciarMinado();
            }
        }

        // Progreso de minado
        if (estaMinando)
        {
            progresoMinado += Time.deltaTime / tiempoMinado;
            sliderMinado.value = progresoMinado;

            if (progresoMinado >= 1f)
            {
                CompletarMinado();
            }
        }
    }

    // Función para minar una mena al estar cerca de ella
    void IniciarMinado()
    {
        estaMinando = true;
        sliderMinado.gameObject.SetActive(true);
        progresoMinado = 0f;
        sliderMinado.value = progresoMinado;
    }

    void AgregarAlInventario()
    {
        // Añadir la mena al inventario con un identificador único
        inventario.Add(gameObject.GetInstanceID().ToString());
        Debug.Log("Item añadido al inventario: " + gameObject.GetInstanceID());
        ActualizarUIInventario();
    }

    void CompletarMinado()
    {
        estaMinando = false;
        sliderMinado.gameObject.SetActive(false);
        AgregarAlInventario();
        AgregarExperienciaAlJugador(); // Añadir experiencia al jugador
        // Destruir la mina después de haberla minado
        Destroy(gameObject);
    }

    public void ActualizarUIInventario()
    {
        if (textoInventario != null)
        {
            textoInventario.text = "Menas obtenidas: " + inventario.Count;
        }
    }

    void AgregarExperienciaAlJugador()
    {
        if (nivelJugador != null)
        {
            nivelJugador.AñadirExperiencia(expGanada);
        }
    }
}

