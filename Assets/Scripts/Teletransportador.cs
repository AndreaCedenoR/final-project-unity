using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    [Header("Destino del Teleport")]
    public Transform puntoDeDestino; // Aquí arrastraremos el objeto de destino

    // Esta función se activa automáticamente cuando algo entra en el Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Primero, comprobamos si lo que entró es el jugador
        if (other.CompareTag("Player"))
        {
            // Verificamos que hemos asignado un destino para evitar errores
            if (puntoDeDestino != null)
            {
                // ¡La magia! Movemos al jugador a la posición del punto de destino
                other.transform.position = puntoDeDestino.position;
                Debug.Log("¡Jugador teletransportado!");
            }
            else
            {
                // Un mensaje de error útil si se nos olvida asignar el destino
                Debug.LogError("¡No has asignado un punto de destino en el teletransportador!");
            }
        }
    }
}