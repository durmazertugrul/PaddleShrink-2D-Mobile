using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScoreText, aiScoreText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private float punchScale = 0.4f;
    [SerializeField] private float punchDuration = 0.3f;
    private int lastPlayerScore, lastAiScore;
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
        int playerScore = GameManager.Instance.PlayerScore;
        int aiScore = GameManager.Instance.AiScore;
        
        playerScoreText.text = GameManager.Instance.PlayerScore.ToString();
        aiScoreText.text = GameManager.Instance.AiScore.ToString();

        if (playerScore != lastPlayerScore) Punch(playerScoreText.transform);
        if (aiScore != lastAiScore) Punch(aiScoreText.transform);

        lastPlayerScore = playerScore;
        lastAiScore = aiScore;

    }

    private void Punch(Transform target)
    {
        target.DOKill(true);
        target.localScale = Vector3.one;
        target.DOPunchScale(Vector3.one * punchScale, punchDuration).SetUpdate(true);
    }
}
