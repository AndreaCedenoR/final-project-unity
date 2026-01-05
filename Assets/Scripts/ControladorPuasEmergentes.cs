using System.Collections;
using UnityEngine;

public class ControladorPuasEmergentes : MonoBehaviour
{
    [Header("Referencias")]
    public Transform puas; // El objeto padre que contiene todas las púas

    [Header("Configuración de Movimiento")]
    public Vector3 posicionOculta;   // La posición Y donde empiezan
    public Vector3 posicionActiva;   // La posición Y a la que subirán
    public float velocidadSubida = 8f;
    public float velocidadBajada = 4f;
    public float tiempoDeEspera = 2f;

    private bool yaFueActivado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !yaFueActivado)
        {
            yaFueActivado = true;
            StartCoroutine(SecuenciaDeTrampa());
        }
    }

    IEnumerator SecuenciaDeTrampa()
    {
        Debug.Log("Trampa de púas emergentes activada.");

        // --- FASE 1: SUBIR LAS PÚAS ---
        while (Vector3.Distance(puas.position, posicionActiva) > 0.01f)
        {
            puas.position = Vector3.MoveTowards(puas.position, posicionActiva, velocidadSubida * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }
        puas.position = posicionActiva; // Aseguramos la posición final

        // --- FASE 2: ESPERAR ---
        yield return new WaitForSeconds(tiempoDeEspera);

        // --- FASE 3: BAJAR LAS PÚAS ---
        while (Vector3.Distance(puas.position, posicionOculta) > 0.01f)
        {
            puas.position = Vector3.MoveTowards(puas.position, posicionOculta, velocidadBajada * Time.deltaTime);
            yield return null; // Espera al siguiente frame
        }
        puas.position = posicionOculta; // Aseguramos la posición final

        Debug.Log("Púas emergentes retiradas.");
    }
}