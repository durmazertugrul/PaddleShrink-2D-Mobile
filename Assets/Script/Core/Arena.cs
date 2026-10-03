using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private BoxCollider2D leftWall, rightWall,topGoal,bottomGoal;
    [SerializeField] private float wallThickness = 1f;
    private Camera mainCam;

    public static float halfWidth { get; private set; }
    public static float halfHeight { get; private set; }

    private void Awake()
    {
        mainCam = Camera.main;
        halfHeight = mainCam.orthographicSize;
        halfWidth = halfHeight * mainCam.aspect;

        leftWall.transform.position =  new Vector2(-halfWidth - wallThickness / 2,0f);//position of the left wall
        rightWall.transform.position = new Vector2(halfWidth + wallThickness / 2,0f);//position of the right wall

        leftWall.size = new Vector2(wallThickness, halfHeight * 2 + 2);//collider size both of them
        rightWall.size = new Vector2(wallThickness, halfHeight * 2 + 2);

        topGoal.transform.position = new Vector2(0, halfHeight + wallThickness / 2);//position of the top goal
        bottomGoal.transform.position = new Vector2(0, -halfHeight - wallThickness / 2);//position of the bottom goal
        topGoal.size = new Vector2(halfWidth * 2 + 2, wallThickness);//collider size both of them
        bottomGoal.size = new Vector2(halfWidth * 2 + 2, wallThickness);

    }
}
