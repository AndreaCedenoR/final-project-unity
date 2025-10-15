using UnityEngine;

public class Level4Manager : MonoBehaviour
{
    // Start se llama una vez, justo cuando la escena carga
    void Start()
    {
        // Buscamos el objeto del jugador en la escena
        ControladorJugador jugador = FindFirstObjectByType<ControladorJugador>();

        // Comprobamos si encontramos al jugador para evitar errores
        if (jugador != null)
        {
            // ¡Activamos el interruptor de los controles invertidos!
            jugador.controlesInvertidos = true;
            Debug.Log("¡Los controles del jugador han sido invertidos por el Nivel 4!");
        }
        else
        {
            Debug.LogError("¡No se encontró al jugador en la escena!");
        }
    }
}