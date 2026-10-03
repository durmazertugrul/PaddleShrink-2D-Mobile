using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb_2d;
    [SerializeField] private float startSpeed = 7f; //serve speed
    [SerializeField] private float speedStep = 0.3f; //speed increase after each hit
    [SerializeField] private float maxSpeed = 13f; 
    [SerializeField] private float maxBounceX = 1.2f; //max horizontal bounce ratio
    [SerializeField] private float minVerticalRatio = 0.35f; //min vertical bounce ratio 
    [SerializeField] private float serveDelay = 0.6f;

    public static event System.Action<Transform, Vector2, float> PaddleHit;
    public static event System.Action<Vector2> WallHit;
    private bool isMoving;
    private float currentSpeed;
    private Coroutine serveRoutine;

    private void Awake()
    {
        rb_2d = GetComponent<Rigidbody2D>();
    }

    public void Serve(bool towardPlayer) 
    {
        Stop();
        serveRoutine = StartCoroutine(ServeRoutine(towardPlayer));

    }
    private IEnumerator ServeRoutine(bool towardPlayer)
    {
        rb_2d.position = new Vector2(0, 0);
        yield return new WaitForSeconds(serveDelay);

        float y = towardPlayer ? -1 : 1;
        Vector2 dir = new Vector2(Random.Range(-0.3f, 0.3f), y).normalized;
        currentSpeed = startSpeed;
        rb_2d.linearVelocity = dir * currentSpeed;
        isMoving = true;
    }
    public void Stop()
    {
        if (serveRoutine != null) 
        {
            StopCoroutine(serveRoutine);
        }
        rb_2d.linearVelocity = Vector2.zero;
        isMoving = false;
    }


    private void FixedUpdate()
    {
        if (!isMoving) { return; }

        Vector2 dir = rb_2d.linearVelocity.normalized;

        if (Mathf.Abs(dir.y) < minVerticalRatio)
        {
            dir.y = Mathf.Sign(dir.y) * minVerticalRatio;
            dir = dir.normalized;
        }
        rb_2d.linearVelocity = dir * currentSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            Collider2D paddle = other.collider;

            float halfW = paddle.bounds.extents.x;
            float offset = Mathf.Clamp((rb_2d.position.x - paddle.transform.position.x) / halfW, -1, 1);
            float yDir = rb_2d.position.y > paddle.transform.position.y ? +1 : -1;

            Vector2 dir = new Vector2(offset * maxBounceX, yDir).normalized;

            currentSpeed = Mathf.Min(currentSpeed + speedStep, maxSpeed);
            rb_2d.linearVelocity = dir * currentSpeed;

            Vector2 hitpoint = other.GetContact(0).point;
            PaddleHit?.Invoke(paddle.transform, hitpoint, currentSpeed);
        }

        else if (other.gameObject.CompareTag("Wall"))
        {
            Vector2 hitpoint = other.GetContact(0).point;
            WallHit?.Invoke(hitpoint);
        }
    }


}