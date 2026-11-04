
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseGame()
    {
        if (GameManager.instance == null) return;

        if (!GameManager.instance.isPlaying) return;

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("POPUPUI");
    }
}
