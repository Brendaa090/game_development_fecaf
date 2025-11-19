using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerSimple : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundMask;

    Rigidbody2D rb;
    bool grounded;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * speed, rb.linearVelocity.y);

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
        if (grounded && Input.GetButtonDown("Jump"))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
