using UnityEngine;
using DG.Tweening;

public class HalfFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer topHalf, bottomHalf;
    [SerializeField] private Paddle playerPaddle;
    [SerializeField] private float flashDuration = 0.25f;
    [SerializeField] private float lightenAmount = 0.25f;    

    private Color topBase, bottomBase;

    private void Awake()
    {
        topBase = topHalf.color;
        bottomBase = bottomHalf.color;
    }

    private void OnEnable()
    {
        GameManager.GoalScored += HandleGoal;
    }

    private void OnDisable()
    {
        GameManager.GoalScored -= HandleGoal;
        topHalf.DOKill();
        bottomHalf.DOKill();
    }

    private void HandleGoal(Paddle conceding)
    {
        bool bottom = (conceding == playerPaddle);
        SpriteRenderer half = bottom ? bottomHalf : topHalf;
        Color baseColor = bottom ? bottomBase : topBase;

        half.DOKill();
        half.color = Color.Lerp(baseColor, Color.white, lightenAmount);
        half.DOColor(baseColor, flashDuration).SetUpdate(true);

    }

}
