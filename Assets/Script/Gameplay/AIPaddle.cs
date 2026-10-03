using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    private Paddle paddle;
    [SerializeField] private Rigidbody2D ball;

    [SerializeField] private float aiSpeed = 7f;
    [SerializeField] private float reactionDelay = 0.35f;
    [SerializeField] private float errorRange = 0.8f;

    private float targetX; //target
    private float timer; //the time elapsed since the last reaction

    private void Awake()
    {
        paddle = GetComponent<Paddle>();
        targetX = transform.position.x;
    }

    private void FixedUpdate()
    {
        if (ball == null) return;
        timer += Time.fixedDeltaTime;

        if (timer >= reactionDelay) 
        {
            timer =0f;
            if (ball.linearVelocity.y > 0)
            {
                targetX = ball.position.x + Random.Range(-errorRange, errorRange);
            }
            else targetX = 0f;
        }

        paddle.MoveTowards(targetX, aiSpeed);

    }

}
