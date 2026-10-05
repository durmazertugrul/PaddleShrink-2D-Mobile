using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float strength = 0.25f;
    [SerializeField] private int vibrato = 12;



    private void OnEnable()
    {
        GameManager.GoalScored += HandleGoal;
    }

    private void OnDisable()
    {
        GameManager.GoalScored -= HandleGoal;
        transform.DOKill();
    }

    private void HandleGoal(Paddle conceding)
    {
        transform.DOKill(true);
        transform.DOShakePosition(duration, new Vector3(strength, strength, 0), vibrato)
         .SetUpdate(true);
    }

}
