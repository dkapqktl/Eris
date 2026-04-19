using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody2D rb;
    Vector2 move;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        InputManager.OnMove -= OnMove;
        InputManager.OnMove += OnMove;
    }

    void OnDisable()
    {
        InputManager.OnMove -= OnMove;
    }

    void OnMove(Vector2 value)
    {
        move = value.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }
}