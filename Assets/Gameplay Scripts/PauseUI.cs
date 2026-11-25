
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pausePanel;

    private bool _isPaused = false;

    public void PauseGame()
    {
        Debug.Log("¡Botón de pausa presionado!");
        // Activamos el panel directamente
        pausePanel.SetActive(true);

        // Establecemos la escala de tiempo a 0 para congelar el juego
        Time.timeScale = 0f;

        // Actualizamos el estado
        _isPaused = true;
    }

    public void ResumeGame()
    {
        _isPaused = false;

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("POPUPUI");
    }
}
