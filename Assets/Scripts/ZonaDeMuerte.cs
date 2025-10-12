using UnityEngine;
using UnityEngine.SceneManagement; // �No olvides esto!

public class ZonaDeMuerte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el que entra en la zona es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha ca�do. Reiniciando nivel.");
            // Recargamos la escena que est� actualmente activa
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}