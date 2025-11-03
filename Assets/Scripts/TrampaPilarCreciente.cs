using System.Collections;
using UnityEngine;

public class TrampaPilarCreciente : MonoBehaviour
{
    public GameObject pilarAAnimar;

    public float alturaFinal = 8f;
    public float velocidadAnimacion = 5f;

    private Vector3 escalaInicial;
    private bool pilarEstaCrecido = false;
    private bool estaAnimando = false; 

    void Start()
    {
        escalaInicial = pilarAAnimar.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !estaAnimando)
        {
            if (!pilarEstaCrecido)
            {
                StartCoroutine(AnimarPilar(escalaInicial, new Vector3(escalaInicial.x, alturaFinal, escalaInicial.z)));
            }
            else
            {
                StartCoroutine(AnimarPilar(pilarAAnimar.transform.localScale, escalaInicial));
            }

            pilarEstaCrecido = !pilarEstaCrecido;
        }
    }

    private IEnumerator AnimarPilar(Vector3 inicio, Vector3 fin)
    {
        estaAnimando = true; 
        float tiempoPasado = 0f;

        while (tiempoPasado < 1f)
        {
            pilarAAnimar.transform.localScale = Vector3.Lerp(inicio, fin, tiempoPasado);
            tiempoPasado += Time.deltaTime * velocidadAnimacion;
            yield return null;
        }

        pilarAAnimar.transform.localScale = fin;
        estaAnimando = false;
    }
}