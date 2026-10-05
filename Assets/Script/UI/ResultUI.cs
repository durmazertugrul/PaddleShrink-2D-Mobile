using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text resultText, scoreText, playAgainLabel;
    [SerializeField] private Button playAgainButton, mainMenuButton;
    [SerializeField] private Color winColor;
    [SerializeField] private Color loseColor;

    private void Start()
    {
        playAgainButton.onClick.AddListener(() => {
            GameManager.Instance.StartGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            GameManager.Instance.GoToMenu();
        });
    }


    private void OnEnable()
    {
        var gamemover = GameManager.Instance;
        bool won = gamemover.PlayerWon;
        
        Color themeColor = won ? winColor : loseColor;


        backgroundImage.color = themeColor;
        playAgainLabel.color = themeColor;
        resultText.text = won ? "YOU WIN" : "YOU LOSE";
        scoreText.text = gamemover.PlayerScore + " - " + gamemover.AiScore; 
    }
}
