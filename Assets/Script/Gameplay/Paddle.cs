using System.Collections;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float[] widthSteps = {1.0f, 0.78f, 0.58f, 0.40f };
    [SerializeField] float shrinkDuration = 0.25f; //animation time
    private Rigidbody2D rb_2d; 
    private BoxCollider2D boxCollider_2d;
    private float limit => Arena.halfWidth - boxCollider_2d.bounds.extents.x; // Limit the paddle's movement to within the arena bounds
    private int goalsConceded;
    private float baseWidth;
    private Coroutine shrinkRoutine;
    public int GoalsConceded { get { return goalsConceded; } }

    private void Awake()
    {
        rb_2d = GetComponent<Rigidbody2D>();
        boxCollider_2d = GetComponent<BoxCollider2D>();
        baseWidth = transform.localScale.x; // Store the initial width of the paddle
    }
    public void MoveTowards(float targetX, float speed)
    {
        targetX = Mathf.Clamp(targetX, -limit, limit);
        float newX = Mathf.MoveTowards(rb_2d.position.x, targetX, speed * Time.fixedDeltaTime);
        rb_2d.MovePosition(new Vector2(newX, rb_2d.position.y));
    }

    public bool Shrink()
    {
        goalsConceded++;
        if (goalsConceded >= widthSteps.Length)
        {
            return true; // Paddle is at minimum width, game over
        }

        float targetWidth = baseWidth * widthSteps[goalsConceded];

        if (shrinkRoutine != null)
        {
            StopCoroutine(shrinkRoutine);
        }

        shrinkRoutine = StartCoroutine(ShrinkRoutine(targetWidth));
        return false;
    }

    private IEnumerator ShrinkRoutine(float targetWidth) 
    {
        float startWidth = transform.localScale.x;
        float elapsed = 0f; //the time elapsed since animation started

        while (elapsed < shrinkDuration) 
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / shrinkDuration;
            float currentWidth = Mathf.Lerp(startWidth, targetWidth, progress);
            transform.localScale = new Vector3(currentWidth, transform.localScale.y, transform.localScale.z);
            yield return null;
        }

        transform.localScale = new Vector3(targetWidth, transform.localScale.y, transform.localScale.z);
    }


    public void ResetPaddle() //need to reset the paddle to new game state
    {
        if (shrinkRoutine !=null) { StopCoroutine(shrinkRoutine); }
        goalsConceded = 0;
        transform.localScale = new Vector3(baseWidth, transform.localScale.y, transform.localScale.z);
    }

}
