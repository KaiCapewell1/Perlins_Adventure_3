using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb; 

    private Vector2 movement;

    void Update()
    {
        // 1. Get Input
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        float x = 0;
        float y = 0;

        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) y = 1;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) y = -1;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) x = -1;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) x = 1;

        movement = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
 
        rb.linearVelocity = movement * moveSpeed;
    }
}