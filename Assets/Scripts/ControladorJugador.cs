using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    // Variables que podemos ajustar desde el Inspector de Unity
    public float velocidadMovimiento = 7f;
    public float fuerzaSalto = 5f;

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
        // Obtenemos la entrada del teclado (-1 para izquierda, 1 para derecha)
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // Aplicamos la velocidad al Rigidbody para mover al personaje
        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidadMovimiento, rb.linearVelocity.y);

        // --- SALTO ---
        // Comprobamos si se presion� la barra espaciadora Y si el jugador est� en el suelo
        if (Input.GetButtonDown("Jump") && estaEnElSuelo)
        {
            // Aplicamos una fuerza vertical para que salte
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }

    // Esta funci�n se llama cuando el collider del jugador EMPIEZA a tocar otro collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobamos si el objeto con el que chocamos tiene la etiqueta "Piso"
        if (collision.gameObject.CompareTag("Piso"))
        {
            estaEnElSuelo = true;
        }
    }

    // Esta funci�n se llama cuando el collider del jugador DEJA de tocar otro collider
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Comprobamos si dejamos de tocar el objeto con la etiqueta "Piso"
        if (collision.gameObject.CompareTag("Piso"))
        {
            estaEnElSuelo = false;
        }
    }
}
