using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sfxSource, hitSource;
    [SerializeField] private AudioClip paddleHit, wallHit, goal, shrink, win, lose, uiClick, uiToggle, serve;
    [SerializeField] float maxBallSpeed = 13f;
    private bool muted;
    public bool Muted => muted;
    public void PlayGoal() => Play(goal);
    public void PlayShrink() => Play(shrink);
    public void PlayWin() => Play(win);
    public void PlayLose() => Play(lose);
    public void PlayClick() => Play(uiClick);
    public void PlayToggle() => Play(uiToggle);
    private void OnEnable()
    {
        Ball.PaddleHit += HandlePaddleHit;
        Ball.WallHit += HandleWallHit;
        Ball.Served += HandleServed;
    }

    private void OnDisable()
    {
        Ball.PaddleHit -= HandlePaddleHit;
        Ball.WallHit -= HandleWallHit;
        Ball.Served -= HandleServed;
    }


    private void Awake()
    {
        Instance = this;
        muted = PlayerPrefs.GetInt("muted", 0) == 1;
        ApplyMute();
    }
    public void Play(AudioClip clip) 
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayPaddleHit(float pitch) 
    {
        hitSource.pitch = pitch;
        hitSource.PlayOneShot(paddleHit);

    }

    public void ToggleMute() 
    {
        muted = !muted;
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyMute();
    }

    private void ApplyMute() 
    {
        AudioListener.volume = muted ? 0 : 1; 
    }

    private void HandlePaddleHit(Transform paddle, Vector2 point, float speed) 
    {
        float speedRatio = speed / maxBallSpeed;
        float pitch = Mathf.Lerp(1.0f, 1.3f, speedRatio);
        PlayPaddleHit(pitch);
    }

    private void HandleWallHit(Vector2 point)
    {
        Play(wallHit);
    }

    private void HandleServed()
    {
        Play(serve);
    }

    


}
