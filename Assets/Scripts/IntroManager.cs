using System.Collections;
using UnityEngine;
using TMPro; // Necesario para TextMeshPro
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class IntroManager : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI storyTextComponent; // Arrastra aquí tu objeto de texto
    public GameObject pressEnterPrompt; // Arrastra el texto "Presiona Enter..."

    [Header("Story Settings")]
    [TextArea(5, 10)] // Esto hace que el campo de texto sea más grande en el Inspector
    public string fullStoryText; // Aquí escribirás tu historia
    public float typingSpeed = 0.05f; // Velocidad a la que aparecen las letras

    [Header("Scene To Load")]
    public string nextSceneName; // El nombre de la escena del juego (ej: "Level1")

    private bool isTypingFinished = false;

    void Start()
    {
        // Limpiamos el texto y ocultamos el aviso de "Presiona Enter"
        storyTextComponent.text = "";
        pressEnterPrompt.SetActive(false);

        // Iniciamos la Corutina que escribirá el texto
        StartCoroutine(ShowText());
    }

    void Update()
    {
        // Solo escuchamos la tecla Enter si la escritura ha terminado
        if (isTypingFinished)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // Cargamos la siguiente escena
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    // Una Corutina es una función que puede pausar su ejecución
    IEnumerator ShowText()
    {
        // Recorremos cada letra de la historia
        foreach (char letter in fullStoryText.ToCharArray())
        {
            // Añadimos la letra al texto visible
            storyTextComponent.text += letter;
            // Esperamos un poquito antes de mostrar la siguiente letra
            yield return new WaitForSeconds(typingSpeed);
        }

        // Cuando termina, activamos el aviso y marcamos que ha finalizado
        pressEnterPrompt.SetActive(true);
        isTypingFinished = true;
        Debug.Log("¡Escritura terminada! Ahora puedes presionar Enter.");
    }
}