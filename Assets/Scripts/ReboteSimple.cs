using UnityEngine;

public class ReboteSimple : MonoBehaviour
{
    [Header("Configuración del Rebote")]
    public float fuerzaRebote = 20f; // Puedes ajustar esto para que rebote más o menos que el otro

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si el objeto que nos ha tocado es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Comprobamos si el jugador nos toca desde arriba
            ContactPoint2D contactPoint = collision.GetContact(0);
            if (contactPoint.normal.y < -0.5f)
            {
                // Obtenemos el Rigidbody del jugador para aplicarle la fuerza
                Rigidbody2D rbJugador = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rbJugador != null)
                {
                    // Le damos al jugador una gran velocidad vertical
                    rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, fuerzaRebote);
                    Debug.Log("¡Rebote en trampolín estático!");
                }
            }
        }
    }
}