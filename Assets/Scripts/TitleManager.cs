using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // El nombre de la siguiente escena a cargar (la de la historia)
    public string nextSceneName;

    void Update()
    {
        // Si se presiona la tecla Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Carga la siguiente escena
            SceneManager.LoadScene(nextSceneName);
        }
    }
}