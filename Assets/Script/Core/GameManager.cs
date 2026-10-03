using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ball ball;
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private Paddle aiPaddle;

    public GameState State { get; private set; }
    public bool PlayerWon { get; private set; }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame() 
    {
        playerPaddle.ResetPaddle();
        aiPaddle.ResetPaddle();
        SetState(GameState.Serving);
        ball.Serve(towardPlayer: Random.value > 0.5f);
    }

    public void OnGoal(Paddle concedingPaddle) 
    {
        if (State == GameState.GameOver) return;

        ball.Stop();
        bool eliminated = concedingPaddle.Shrink(); //if the paddle eliminated, the paddle will shrink

        if (eliminated) 
        {
            PlayerWon = concedingPaddle == aiPaddle;
            SetState(GameState.GameOver);
        }
        else
        {
            SetState(GameState.Serving);
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
        Time.timeScale = (newState == GameState.Paused || newState == GameState.GameOver) ? 0f : 1f;
    }
}
