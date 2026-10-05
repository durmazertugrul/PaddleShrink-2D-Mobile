using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event System.Action<GameState> StateChanged;
    public static event System.Action ScoreChanged;
    public static event System.Action<Paddle> GoalScored;

    [SerializeField] private Ball ball;
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private Paddle aiPaddle;
    [SerializeField] private AIPaddle aiPaddleControl;
    public DifficultySettings SelectedDifficulty { get; private set; } = DifficultySettings.Normal;
    public int PlayerScore => aiPaddle.GoalsConceded;
    public int AiScore => playerPaddle.GoalsConceded;

    public GameState State { get; private set; }
    public bool PlayerWon { get; private set; }

   


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SelectedDifficulty = PlayerPrefs.HasKey("SelectedDifficulty") ? (DifficultySettings)PlayerPrefs.GetInt("SelectedDifficulty") : DifficultySettings.Normal;
    }

    private void Start()
    {
        SetState(GameState.Menu);
    }

    public void StartGame() 
    {
        aiPaddleControl.ApplyDifficulty(SelectedDifficulty);
        playerPaddle.ResetPaddle();
        aiPaddle.ResetPaddle();
        SetState(GameState.Playing);
        ball.Serve(towardPlayer: Random.value > 0.5f);
    }
    public void SetDifficulty(DifficultySettings difficulty) 
    {
        SelectedDifficulty = difficulty;
        PlayerPrefs.SetInt("SelectedDifficulty", (int)difficulty);
        PlayerPrefs.Save();

    }

    public void OnGoal(Paddle concedingPaddle) 
    {
        if (State == GameState.GameOver) return;

        ball.Stop();
        bool eliminated = concedingPaddle.Shrink(); //if the paddle eliminated, the paddle will shrink

        ScoreChanged?.Invoke();
        GoalScored?.Invoke(concedingPaddle);

        AudioManager.Instance.PlayGoal();
        if(!eliminated) AudioManager.Instance.PlayShrink();


        if (eliminated) 
        {
            PlayerWon = concedingPaddle == aiPaddle;
            SetState(GameState.GameOver);
        }
        else
        {
            SetState(GameState.Playing);
            ball.Serve(towardPlayer: concedingPaddle == playerPaddle);
        }
    }

    public void Pause() 
    {
        if (State == GameState.Playing) SetState(GameState.Paused);
    }

    public void Resume()
    {
        if (State == GameState.Paused) SetState(GameState.Playing);
    }



    private void SetState(GameState newState)
    {
        State = newState;
        StateChanged?.Invoke(newState);
        Time.timeScale = (newState == GameState.Paused || newState == GameState.GameOver) ? 0f : 1f;
        
        if (newState == GameState.GameOver)
        {
            if (PlayerWon)
                AudioManager.Instance.PlayWin();
            else
                AudioManager.Instance.PlayLose();
        }
    }

    public void GoToMenu() 
    {
        SetState(GameState.Menu);
    }


}
