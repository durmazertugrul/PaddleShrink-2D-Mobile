using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    [SerializeField] float followSpeed = 20f;
    [SerializeField] float keySpeed = 12f;
    private Paddle paddle;
    private Camera cam;
    private float targetX;

    private void Awake()
    {
        paddle = GetComponent<Paddle>();
        cam = Camera.main;
        targetX = transform.position.x;
    }

    private void FixedUpdate()
    {
        //Move the paddle with finger or mouse input
        var pointer = Pointer.current;
        if (pointer != null && pointer.press.isPressed)
        {
            Vector2 screenPos = pointer.position.ReadValue(); // Get the pointer position in screen coordinates
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos); // Convert the screen position to world coordinates
            targetX = worldPos.x; // Set the target X position to the pointer's world X position
        }

        // Move the paddle with keyboard input
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            float axis = 0f;
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) axis = -1f; // A or Left Arrow key moves left
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) axis = 1f; // D or Right Arrow key moves right
            if (axis != 0f) targetX += axis * keySpeed * Time.fixedDeltaTime;
        }

        paddle.MoveTowards(targetX, followSpeed);//to tell the paddle to move towards the target position with a certain speed
    }


}
