using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReiniciarNivel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ReiniciarNivel();
        }
    }

    private void ReiniciarNivel()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(escenaActual);
    }
}