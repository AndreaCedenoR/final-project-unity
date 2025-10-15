using UnityEngine;

public class ControladorPlataformaRestauradora : MonoBehaviour
{
    private bool haSidoActivada = false; // Para que solo funcione una vez
    private SpriteRenderer spriteRenderer; // Para cambiar el color como feedback visual
    public Color colorDesactivado = Color.gray; // Color que tomará después de usarse

    void Start()
    {
        // Obtenemos el componente SpriteRenderer para poder cambiar su color
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // OnCollisionEnter2D se activa cuando un objeto con Rigidbody2D choca con este
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto que nos toca es el jugador Y no hemos sido activados antes
        if (collision.gameObject.CompareTag("Player") && !haSidoActivada)
        {
            // Marcamos que ya fuimos activados para que no se repita
            haSidoActivada = true;

            // Buscamos el script del jugador
            ControladorJugador jugador = collision.gameObject.GetComponent<ControladorJugador>();

            // Si encontramos el script...
            if (jugador != null)
            {
                // ¡Devolvemos los controles a la normalidad!
                jugador.controlesInvertidos = false;

                // Damos feedback visual cambiando el color de la plataforma
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = colorDesactivado;
                }

                Debug.Log("¡Controles restaurados a la normalidad!");
            }
        }
    }
}