using System.Collections;
using UnityEngine;

public class ControladorTrampolin : MonoBehaviour
{
    // Usamos un "enum" para definir los estados posibles de nuestro trampolín
    private enum EstadoTrampolin { Quieto, ViajandoAPausa, EnPausa, ViajandoAFinal, Regresando, Desapareciendo }
    private EstadoTrampolin estadoActual;

    [Header("Objetos y Referencias")]
    public GameObject trampolinEstatico; // El segundo trampolín que aparecerá

    [Header("Configuración del Rebote")]
    public float fuerzaRebote = 20f;

    [Header("Configuración del Movimiento")]
    public Transform puntoA;
    public Transform puntoDePausa;
    public Transform puntoDeRegreso;
    public Transform puntoB;
    public float velocidadMovimiento = 3f;
    public float tiempoAparicion = 1f; // Cuánto tiempo antes de desaparecer aparece el trampolín 2

    private Vector3 puntoDestino;

    void Start()
    {
        estadoActual = EstadoTrampolin.Quieto; // Empezamos en el estado "Quieto"
        transform.position = puntoA.position;
    }

    void Update()
    {
        // Si no estamos en un estado de movimiento, no hacemos nada en Update.
        if (estadoActual != EstadoTrampolin.ViajandoAPausa &&
            estadoActual != EstadoTrampolin.ViajandoAFinal &&
            estadoActual != EstadoTrampolin.Regresando)
        {
            return;
        }

        // Movemos el trampolín hacia su destino actual
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino, velocidadMovimiento * Time.deltaTime);

        // Comprobamos si hemos llegado a nuestro destino para cambiar de estado
        if (Vector3.Distance(transform.position, puntoDestino) < 0.1f)
        {
            if (estadoActual == EstadoTrampolin.ViajandoAPausa)
            {
                estadoActual = EstadoTrampolin.EnPausa;
                Debug.Log("Llegué a la pausa. Esperando siguiente rebote.");
            }
            else if (estadoActual == EstadoTrampolin.ViajandoAFinal)
            {
                estadoActual = EstadoTrampolin.Regresando;
                puntoDestino = puntoDeRegreso.position; // Nuevo destino
                Debug.Log("Llegué al final. Regresando a la mitad.");
            }
            else if (estadoActual == EstadoTrampolin.Regresando)
            {
                estadoActual = EstadoTrampolin.Desapareciendo;
                Debug.Log("Llegué al punto de regreso. Desapareciendo.");
                StartCoroutine(SecuenciaFinal());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            if (contactPoint.normal.y < -0.5f) // Si el jugador nos pisa desde arriba
            {
                // Rebotar siempre es igual
                Rigidbody2D rbJugador = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rbJugador != null)
                {
                    rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, fuerzaRebote);
                }

                // Ahora, cambiamos de estado según en cuál estemos
                if (estadoActual == EstadoTrampolin.Quieto)
                {
                    estadoActual = EstadoTrampolin.ViajandoAPausa;
                    puntoDestino = puntoDePausa.position;
                    Debug.Log("Primer toque. Viajando a la pausa.");
                }
                else if (estadoActual == EstadoTrampolin.EnPausa)
                {
                    estadoActual = EstadoTrampolin.ViajandoAFinal;
                    puntoDestino = puntoB.position;
                    Debug.Log("Segundo toque. Viajando al final.");
                }
            }
        }
    }

    // Corutina para manejar la desaparición y aparición final
    IEnumerator SecuenciaFinal()
    {
        // Esperamos un momento para que el jugador vea que está a punto de desaparecer
        yield return new WaitForSeconds(tiempoAparicion);

        // Activamos el segundo trampolín
        if (trampolinEstatico != null)
        {
            trampolinEstatico.SetActive(true);
            Debug.Log("Trampolín estático apareció.");
        }

        // Hacemos desaparecer el primer trampolín
        gameObject.SetActive(false);
    }
}