using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] private Paddle ownerPaddle; //the paddle that covers this goal zone the one who concedes the goal

    private void OnTriggerEnter2D(Collider2D other)
    {
        Ball ball = other.GetComponent<Ball>();

        if (ball == null) return; // if there is no ball script attached return
     
        GameManager.Instance.OnGoal(ownerPaddle); // Notify the GameManager that a goal has been scored
    }

}
