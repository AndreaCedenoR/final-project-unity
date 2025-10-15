using UnityEngine;

public class TrampaDeRoca : MonoBehaviour
{
    public Rigidbody2D rocaRigidbody;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rocaRigidbody.bodyType = RigidbodyType2D.Dynamic;

            gameObject.SetActive(false);
        }
    }
}