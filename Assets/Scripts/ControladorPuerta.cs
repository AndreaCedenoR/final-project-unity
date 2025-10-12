using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class ControladorPuerta : MonoBehaviour
{
    // En el Inspector pondremos el nombre de la escena a la que queremos ir
    public string nombreDelSiguienteNivel;

    // Esta función se activa cuando otro objeto con Collider2D entra en nuestro Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si el objeto que entró es el jugador
        // Para que esto funcione, ¡tu jugador debe tener la etiqueta "Player"!
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Nivel completado! Cargando " + nombreDelSiguienteNivel);
            // Cargamos la escena que especificamos en el Inspector
            SceneManager.LoadScene(nombreDelSiguienteNivel);
        }
    }
}