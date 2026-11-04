

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public float currentScore = 0f;
    public float scoreMultiplier = 10f;
    public bool isPlaying = true;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public GameObject gameOverPanel;

    public string prettyScore
    {
        get
        {
            return Mathf.RoundToInt(currentScore).ToString("00000");
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!isPlaying) return;

        currentScore += Time.deltaTime * scoreMultiplier;

        if (scoreText != null)
        {
            scoreText.text = prettyScore;
        }
    }


    public void GameOver()
    {
        if (!isPlaying) return;

        isPlaying = false;

        // Guardar la puntuacion final
        if (finalScoreText != null)
        {
            finalScoreText.text = prettyScore;
        }

        if (gameOverPanel != null)
        {
            // Activar el panel
            gameOverPanel.SetActive(true);

            // Recuperar su posicion y escala
            RectTransform rect = gameOverPanel.GetComponent<RectTransform>();

            if (rect != null)
            {
                rect.localScale = Vector3.one;
                rect.anchoredPosition = Vector2.zero;

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;

                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;
            }

            // Recuperar la visibilidad
            CanvasGroup group = gameOverPanel.GetComponent<CanvasGroup>();

            if (group != null)
            {
                group.alpha = 1f;
                group.interactable = true;
                group.blocksRaycasts = true;
            }
        }
        else
        {
            Debug.LogError("No esta asignado Game Over Panel");
        }

        Time.timeScale = 0f;

        Debug.Log("GAME OVER ACTIVADO");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("POPUPUI");
    }
}
