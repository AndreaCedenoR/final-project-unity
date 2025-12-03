using System.Collections;
using UnityEngine;

public class TrampaPilarEmpujador : MonoBehaviour
{
    public GameObject pilar;
    public float distanciaEmpuje = 10f; 
    public float velocidadMovimiento = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Solo empezamos la corrutina. NO desactivamos nada aquí.
            StartCoroutine(MoverYCaerPilar());
        }
    }

    private IEnumerator MoverYCaerPilar()
    {
        // Desactivamos el collider del trigger para que no se active de nuevo
        this.GetComponent<Collider2D>().enabled = false;

        // Activamos el pilar
        pilar.SetActive(true);
        yield return new WaitForEndOfFrame(); // Esperamos un frame

        Rigidbody2D pilarRb = pilar.GetComponent<Rigidbody2D>();
        Vector3 posicionInicial = pilar.transform.position;
        Vector3 posicionObjetivo = posicionInicial - new Vector3(distanciaEmpuje, 0, 0);

        float tiempoTranscurrido = 0f;
        float duracionDelViaje = distanciaEmpuje / velocidadMovimiento;

        while (tiempoTranscurrido < duracionDelViaje)
        {
            pilar.transform.position = Vector3.Lerp(posicionInicial, posicionObjetivo, tiempoTranscurrido / duracionDelViaje);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        pilar.transform.position = posicionObjetivo;

        pilarRb.bodyType = RigidbodyType2D.Dynamic;
    }
}