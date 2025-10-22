using System.Collections;
using UnityEngine;

public class ControladorTrampolin : MonoBehaviour
{
    // Usamos un "enum" para definir los estados posibles de nuestro trampol�n
    private enum EstadoTrampolin { Quieto, ViajandoAPausa, EnPausa, ViajandoAFinal, Regresando, Desapareciendo }
    private EstadoTrampolin estadoActual;

    [Header("Objetos y Referencias")]
    public GameObject trampolinEstatico; // El segundo trampol�n que aparecer�

    [Header("Configuraci�n del Rebote")]
    public float fuerzaRebote = 20f;

    [Header("Configuraci�n del Movimiento")]
    public Transform puntoA;
    public Transform puntoDePausa;
    public Transform puntoDeRegreso;
    public Transform puntoB;
    public float velocidadMovimiento = 3f;
    public float tiempoAparicion = 1f; // Cu�nto tiempo antes de desaparecer aparece el trampol�n 2

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

        // Movemos el trampol�n hacia su destino actual
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino, velocidadMovimiento * Time.deltaTime);

        // Comprobamos si hemos llegado a nuestro destino para cambiar de estado
        if (Vector3.Distance(transform.position, puntoDestino) < 0.1f)
        {
            if (estadoActual == EstadoTrampolin.ViajandoAPausa)
            {
                estadoActual = EstadoTrampolin.EnPausa;
                Debug.Log("Llegu� a la pausa. Esperando siguiente rebote.");
            }
            else if (estadoActual == EstadoTrampolin.ViajandoAFinal)
            {
                estadoActual = EstadoTrampolin.Regresando;
                puntoDestino = puntoDeRegreso.position; // Nuevo destino
                Debug.Log("Llegu� al final. Regresando a la mitad.");
            }
            else if (estadoActual == EstadoTrampolin.Regresando)
            {
                estadoActual = EstadoTrampolin.Desapareciendo;
                Debug.Log("Llegu� al punto de regreso. Desapareciendo.");
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

                // Ahora, cambiamos de estado seg�n en cu�l estemos
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

    // Corutina para manejar la desaparici�n y aparici�n final
    IEnumerator SecuenciaFinal()
    {
        // Esperamos un momento para que el jugador vea que est� a punto de desaparecer
        yield return new WaitForSeconds(tiempoAparicion);

        // Activamos el segundo trampol�n
        if (trampolinEstatico != null)
        {
            trampolinEstatico.SetActive(true);
            Debug.Log("Trampol�n est�tico apareci�.");
        }

        // Hacemos desaparecer el primer trampol�n
        gameObject.SetActive(false);
    }
}