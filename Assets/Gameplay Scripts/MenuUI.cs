
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Saliendo del juego");
    }
}
