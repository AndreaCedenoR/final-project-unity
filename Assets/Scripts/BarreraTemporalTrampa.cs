using System.Collections;
using UnityEngine;

public class BarreraTemporalTrampa : MonoBehaviour
{
    [Header("Objetos de la Trampa")]
    public Transform barrera; // La barrera que se mover�

    [Header("Posiciones")]
    public Vector3 posicionInicial; // La posici�n donde la barrera est� escondida
    public Vector3 posicionActiva;  // La posici�n donde la barrera bloquea el salto

    [Header("Tiempos y Velocidad")]
    public float velocidadSubida = 10f; // Qu� tan r�pido sube la barrera
    public float tiempoDeEspera = 2f;  // Cu�ntos segundos se queda arriba antes de bajar
    public float velocidadBajada = 2f;  // Qu� tan r�pido baja

    private bool trampaActivada = false;

    // Se activa cuando el jugador entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el que entra es el jugador Y la trampa no ha sido activada antes
        if (other.CompareTag("Player") && !trampaActivada)
        {
            trampaActivada = true; // Marcamos la trampa para que no se active de nuevo
            Debug.Log("�Trampa activada!");
            StartCoroutine(ActivarSecuenciaTrampa());
        }
    }

    // Usamos una Corutina para manejar la secuencia con tiempos de espera
    IEnumerator ActivarSecuenciaTrampa()
    {
        // --- FASE 1: SUBIR LA BARRERA ---
        // Mientras la barrera no haya llegado a su posici�n activa...
        while (Vector3.Distance(barrera.position, posicionActiva) > 0.01f)
        {
            // Mueve la barrera hacia arriba
            barrera.position = Vector3.MoveTowards(barrera.position, posicionActiva, velocidadSubida * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }
        barrera.position = posicionActiva; // Aseguramos que llegue a la posici�n exacta

        // --- FASE 2: ESPERAR ---
        Debug.Log("Barrera arriba, esperando " + tiempoDeEspera + " segundos.");
        yield return new WaitForSeconds(tiempoDeEspera); // Pausa la ejecuci�n por el tiempo especificado

        // --- FASE 3: BAJAR LA BARRERA ---
        Debug.Log("Bajando la barrera.");
        // Mientras la barrera no haya vuelto a su posici�n inicial...
        while (Vector3.Distance(barrera.position, posicionInicial) > 0.01f)
        {
            // Mueve la barrera hacia abajo
            barrera.position = Vector3.MoveTowards(barrera.position, posicionInicial, velocidadBajada * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }
        barrera.position = posicionInicial; // Aseguramos que llegue a la posici�n exacta

        Debug.Log("La barrera ha bajado y no volver� a subir.");
    }
}