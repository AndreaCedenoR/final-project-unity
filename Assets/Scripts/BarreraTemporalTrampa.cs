using System.Collections;
using UnityEngine;

public class BarreraTemporalTrampa : MonoBehaviour
{
    [Header("Objetos de la Trampa")]
    public Transform barrera; // La barrera que se moverá

    [Header("Posiciones")]
    public Vector3 posicionInicial; // La posición donde la barrera está escondida
    public Vector3 posicionActiva;  // La posición donde la barrera bloquea el salto

    [Header("Tiempos y Velocidad")]
    public float velocidadSubida = 10f; // Qué tan rápido sube la barrera
    public float tiempoDeEspera = 2f;  // Cuántos segundos se queda arriba antes de bajar
    public float velocidadBajada = 2f;  // Qué tan rápido baja

    private bool trampaActivada = false;

    // Se activa cuando el jugador entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el que entra es el jugador Y la trampa no ha sido activada antes
        if (other.CompareTag("Player") && !trampaActivada)
        {
            trampaActivada = true; // Marcamos la trampa para que no se active de nuevo
            Debug.Log("¡Trampa activada!");
            StartCoroutine(ActivarSecuenciaTrampa());
        }
    }

    // Usamos una Corutina para manejar la secuencia con tiempos de espera
    IEnumerator ActivarSecuenciaTrampa()
    {
        // --- FASE 1: SUBIR LA BARRERA ---
        // Mientras la barrera no haya llegado a su posición activa...
        while (Vector3.Distance(barrera.position, posicionActiva) > 0.01f)
        {
            // Mueve la barrera hacia arriba
            barrera.position = Vector3.MoveTowards(barrera.position, posicionActiva, velocidadSubida * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }
        barrera.position = posicionActiva; // Aseguramos que llegue a la posición exacta

        // --- FASE 2: ESPERAR ---
        Debug.Log("Barrera arriba, esperando " + tiempoDeEspera + " segundos.");
        yield return new WaitForSeconds(tiempoDeEspera); // Pausa la ejecución por el tiempo especificado

        // --- FASE 3: BAJAR LA BARRERA ---
        Debug.Log("Bajando la barrera.");
        // Mientras la barrera no haya vuelto a su posición inicial...
        while (Vector3.Distance(barrera.position, posicionInicial) > 0.01f)
        {
            // Mueve la barrera hacia abajo
            barrera.position = Vector3.MoveTowards(barrera.position, posicionInicial, velocidadBajada * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }
        barrera.position = posicionInicial; // Aseguramos que llegue a la posición exacta

        Debug.Log("La barrera ha bajado y no volverá a subir.");
    }
}