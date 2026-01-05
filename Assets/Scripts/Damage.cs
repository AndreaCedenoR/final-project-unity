using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HazardDamage : MonoBehaviour
{
    public AudioClip deathSound;
    public float delayBeforeReload = 3f; // Nueva variable para controlar el tiempo de espera.

    private bool playerIsDead = false; // Un seguro para evitar activar la muerte múltiples veces.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerIsDead)
        {
            StartCoroutine(DeathSequence(collision.gameObject));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerIsDead)
        {
            // Iniciamos la secuencia de muerte.
            StartCoroutine(DeathSequence(other.gameObject));
        }
    }

    private IEnumerator DeathSequence(GameObject player)
    {
        playerIsDead = true;

        // Desactivamos el control del jugador para que no se pueda mover
        player.GetComponent<ControladorJugador>().enabled = false;

        // --- CÓDIGO CORREGIDO Y MÁS SEGURO ---

        // Primero, comprobamos si el AudioManager existe en la escena
        if (AudioManager.instance != null)
        {
            // Si existe, ahora comprobamos si tenemos un sonido de muerte asignado
            if (deathSound != null)
            {
                // Y solo si ambas cosas son verdad, reproducimos el sonido
                AudioManager.instance.PlaySound(deathSound);
            }
        }
        else
        {
            // Esto es opcional, pero te ayuda a saber por qué no sonó nada cuando pruebas un nivel solo
            Debug.LogWarning("AudioManager no encontrado. No se reproducirá sonido de muerte.");
        }

        // El resto del código continúa ejecutándose sin importar si hubo sonido o no
        yield return new WaitForSeconds(delayBeforeReload);

        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }
}