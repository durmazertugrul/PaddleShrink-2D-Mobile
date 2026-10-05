using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel, hudPanel, pausePanel, resultPanel;
    [SerializeField] private GameObject gameplayRoot;

    private void OnEnable()
    {
        GameManager.StateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        GameManager.StateChanged -= HandleStateChanged;

    }

    public void HandleStateChanged(GameState state)
    {
        menuPanel.SetActive(state == GameState.Menu);
        hudPanel.SetActive(state == GameState.Playing || state == GameState.Paused);
        pausePanel.SetActive(state == GameState.Paused);
        resultPanel.SetActive(state == GameState.GameOver);
        gameplayRoot.SetActive(state != GameState.Menu); 
    }


}
