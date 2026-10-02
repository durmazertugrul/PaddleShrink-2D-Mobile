using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private BoxCollider2D leftWall, rightWall;
    [SerializeField] private float wallThickness = 1f;
    private Camera mainCam;

    public static float halfWidth { get; private set; }
    public static float halfHeight { get; private set; }

    private void Awake()
    {
        mainCam = Camera.main;
        halfHeight = mainCam.orthographicSize;
        halfWidth = halfHeight * mainCam.aspect;

        leftWall.transform.position =  new Vector2(-halfWidth - wallThickness / 2,0f);
        rightWall.transform.position = new Vector2(halfWidth + wallThickness / 2,0f);

        leftWall.size = new Vector2(wallThickness, halfHeight * 2 + 2);
        rightWall.size = new Vector2(wallThickness, halfHeight * 2 + 2);
    }
}
