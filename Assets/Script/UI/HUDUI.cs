using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreText, aiScoreText;
    [SerializeField] private Button pauseButton;
    private void OnEnable()
    {
        UpdateScores();
        GameManager.ScoreChanged += UpdateScores;
    }

    private void OnDisable()
    {
        GameManager.ScoreChanged -= UpdateScores;

    }

    private void Start()
    {
        pauseButton.onClick.AddListener(() => {
            GameManager.Instance.Pause();
        });
    }


    private void UpdateScores() 
    {
        playerScoreText.text = GameManager.Instance.PlayerScore.ToString();
        aiScoreText.text = GameManager.Instance.AiScore.ToString();
    }
}
