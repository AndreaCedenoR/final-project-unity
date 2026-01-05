using UnityEngine;

public class ControladorActivadorPared : MonoBehaviour
{
    // Una referencia a la pared que queremos activar
    public ControladorParedPersecutora pared;

    private bool yaFueActivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el que entra es el jugador y no hemos activado la pared antes
        if (other.CompareTag("Player") && !yaFueActivado)
        {
            yaFueActivado = true;

            // Si tenemos una referencia a la pared...
            if (pared != null)
            {
                // ...le damos la orden de empezar a moverse
                pared.IniciarMovimiento();
            }
        }
    }
}