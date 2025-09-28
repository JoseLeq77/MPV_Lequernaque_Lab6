using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Events

    public static event Action OnPlayerWin;
    public static event Action OnPlayerLose;

    #endregion

    #region Variables

    [Header("Timer Settings")]
    [SerializeField] private float winTime = 30f;
    private float timer = 0f;
    private bool gameEnded = false;

    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText; 

    [Header("Win Panel UI")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winTimeText;
    [SerializeField] private TMP_Text winScoreText;

    [Header("Lose Panel UI")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TMP_Text loseTimeText;
    [SerializeField] private TMP_Text loseScoreText;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        OnPlayerLose += ShowLosePanel;
        OnPlayerWin += ShowWinPanel;
    }

    private void OnDisable()
    {
        OnPlayerLose -= ShowLosePanel;
        OnPlayerWin -= ShowWinPanel;
    }

    private void Start()
    {
        timer = 0f;
        gameEnded = false;
        Time.timeScale = 1f;

        if (timerText != null)
        {
            timerText.text = "Time: 0.0";
        }
        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (gameEnded)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = $"Time: {timer:F1}";
        }
        if (scoreText != null)
        {
            scoreText.text = $"Score: {(int)(timer * 20)}";
        }

        if (timer >= winTime)
        {
            gameEnded = true;
            OnPlayerWin?.Invoke();
        }
    }

    #endregion

    #region Custom Methods

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        if (winTimeText != null)
        {
            winTimeText.text = $"Time: {timer:F1}";
        }
        if (winScoreText != null)
        {
            winScoreText.text = $"Score: {(int)(timer * 20)}";
        }
        Time.timeScale = 0f;
    }

    private void ShowLosePanel()
    {
        gameEnded = true;
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
        if (loseTimeText != null)
        {
            loseTimeText.text = $"Time: {timer:F1}";
        }
        if (loseScoreText != null)
        {
            loseScoreText.text = $"Score: {(int)(timer * 20)}";
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void TriggerOnPlayerWin()
    {
        OnPlayerWin?.Invoke();
    }

    public static void TriggerOnPlayerLose()
    {
        OnPlayerLose?.Invoke();
    }
    #endregion
}
