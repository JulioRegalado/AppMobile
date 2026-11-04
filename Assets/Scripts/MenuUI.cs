using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : UIWindow
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

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("POPUPUI");
    }
}

