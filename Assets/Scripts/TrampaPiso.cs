using UnityEngine;

public class TrampaDePiso : MonoBehaviour
{
    
    public GameObject pisoADesactivar;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pisoADesactivar.SetActive(false);
        }
    }
}