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

        player.GetComponent<ControladorJugador>().enabled = false;

        if (deathSound != null)
        {
            AudioManager.instance.PlaySound(deathSound);
        }

        yield return new WaitForSeconds(delayBeforeReload);

        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }
}