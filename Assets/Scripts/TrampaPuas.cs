using UnityEngine;

public class TrampaDePuas : MonoBehaviour
{
    public GameObject puasAActivar;
    public GameObject puasBActivar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            puasAActivar.SetActive(true);
            puasBActivar.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}