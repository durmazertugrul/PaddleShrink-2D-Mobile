using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton, mainMenuButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Image soundIcon;
    [SerializeField] private Sprite soundOnSprite, soundOffSprite;

    private void OnEnable()
    {
        RefreshSoundIcon();
    }

    private void Start()
    {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.Resume();
        });

        mainMenuButton.onClick.AddListener(() => {
            GameManager.Instance.GoToMenu();
        });

        soundButton.onClick.AddListener(OnSoundClicked);
    }

    private void OnSoundClicked()
    {
        AudioManager.Instance.ToggleMute();
        RefreshSoundIcon();
    }

    private void RefreshSoundIcon()
    {
        soundIcon.sprite = AudioManager.Instance.Muted ? soundOffSprite : soundOnSprite;
    }

}
