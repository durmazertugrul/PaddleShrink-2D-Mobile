using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button easyButton, normalButton, hardButton, playButton, quitButton;
    [SerializeField] private Sprite fillSprite, outlineSprite;
    [SerializeField] private Color selectedTextColor;
    [SerializeField] private Color unSelectedTextColor ;


    private void Start()
    {
        easyButton.onClick.AddListener(() => { Select(DifficultySettings.Easy); });
        normalButton.onClick.AddListener(() => { Select(DifficultySettings.Normal); });
        hardButton.onClick.AddListener(() => { Select(DifficultySettings.Hard); });
        playButton.onClick.AddListener(GameManager.Instance.StartGame);
        quitButton.onClick.AddListener(() => { Application.Quit(); });

        if (Application.platform == RuntimePlatform.WebGLPlayer)
            quitButton.gameObject.SetActive(false);


        RefreshButtons();
    }

    private void Select(DifficultySettings difficulty) 
    {
        GameManager.Instance.SetDifficulty(difficulty);
        AudioManager.Instance.PlayToggle();
        RefreshButtons();
    }

    private void RefreshButtons() 
    {
        var current = GameManager.Instance.SelectedDifficulty;
        ApplyStyle(easyButton, current == DifficultySettings.Easy);
        ApplyStyle(normalButton, current == DifficultySettings.Normal);
        ApplyStyle(hardButton, current == DifficultySettings.Hard);
    }


    private void ApplyStyle(Button btn, bool selected)
    {
        Image image = btn.GetComponent<Image>();
        image.sprite = selected ? fillSprite : outlineSprite;

        TextMeshProUGUI label = btn.GetComponentInChildren<TextMeshProUGUI>();
        label.color = selected ? selectedTextColor : unSelectedTextColor;
    }
}
