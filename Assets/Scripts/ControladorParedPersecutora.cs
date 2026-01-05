using UnityEngine;

public class ControladorParedPersecutora : MonoBehaviour
{
    // La posición final en X a la que queremos llegar
    public float posicionXFinal = 274.30f;
    public float velocidad = 3f;

    private bool seEstaMoviendo = false;
    private Vector3 puntoDestino;

    // Esta función se llamará cuando empiece a moverse
    public void IniciarMovimiento()
    {
        // Calculamos el punto de destino final manteniendo la Y y Z actuales
        puntoDestino = new Vector3(posicionXFinal, transform.position.y, transform.position.z);
        seEstaMoviendo = true;
        Debug.Log("Pared persecutora iniciada. Moviéndose hacia " + puntoDestino);
    }

    void Update()
    {
        // Si la variable de control es falsa, no hacemos nada
        if (!seEstaMoviendo)
        {
            return;
        }

        // Movemos la pared hacia su destino
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino, velocidad * Time.deltaTime);

        // Si ya llegamos, detenemos el movimiento
        if (transform.position == puntoDestino)
        {
            seEstaMoviendo = false;
            Debug.Log("La pared ha llegado a su destino.");
        }
    }
}