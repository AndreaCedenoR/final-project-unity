using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    // Variables que podemos ajustar desde el Inspector de Unity
    public float velocidadMovimiento = 7f;
    public float fuerzaSalto = 5f;
    public bool controlesInvertidos = false;
    // Componentes del jugador
    private Rigidbody2D rb;

    // Variables para controlar el estado
    private bool estaEnElSuelo = false;

    // Start se llama antes del primer frame
    void Start()
    {
        // Obtenemos el componente Rigidbody2D para poder usarlo
        rb = GetComponent<Rigidbody2D>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        // --- MOVIMIENTO HORIZONTAL ---
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // --- LÓGICA DE CONTROLES INVERTIDOS (ACTUALIZADA) ---
        if (controlesInvertidos)
        {
            // Si los controles están invertidos, multiplicamos la entrada por -1
            // USAMOS linearVelocity en lugar de velocity
            rb.linearVelocity = new Vector2(movimientoHorizontal * -1 * velocidadMovimiento, rb.linearVelocity.y);
        }
        else
        {
            // Si no, todo funciona como siempre
            // USAMOS linearVelocity en lugar de velocity
            rb.linearVelocity = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.linearVelocity.y);
        }

        // --- SALTO (ACTUALIZADO) ---
        // Comprobamos si se presionó la barra espaciadora Y si el jugador está en el suelo
        if (Input.GetButtonDown("Jump") && estaEnElSuelo)
        {
            // Aplicamos una fuerza vertical para que salte
            // USAMOS linearVelocity en lugar de velocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }

    // Esta funci�n se llama cuando el collider del jugador EMPIEZA a tocar otro collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si el objeto con el que chocamos tiene la etiqueta "Piso" O "Restauradora"
        if (collision.gameObject.CompareTag("Piso") || collision.gameObject.CompareTag("Restauradora"))
        {
            estaEnElSuelo = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Hacemos lo mismo al salir de la colisión
        if (collision.gameObject.CompareTag("Piso") || collision.gameObject.CompareTag("Restauradora"))
        {
            estaEnElSuelo = false;
        }
    }
}
