using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField, Tooltip("Velocidade máxima do jogador")]
    float moveSpeed = 7f;

    [SerializeField, Tooltip("Força do pulo do jogador")]
    float jumpForce = 12f;

    [SerializeField, Tooltip("Quantidade de pulos do jogador")]
    int maxJumps = 2;
    private int jumpCount = 0;

    [SerializeField, Tooltip("Ponto 'Propriedade' do jogador utilizada para verificar se está no chão")]
    Transform groundCheck;

    [SerializeField, Tooltip("Camada 'layer' que o chão está definido")]
    LayerMask groundLayer;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isGrounded;
    private bool jumpPressed;

    public InputAction MoveAction;
    public InputAction JumpAction;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
        JumpAction.Enable();
    }

    void Update()
    {
        // Gravidade extra no pulo
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.18f, groundLayer);

        // Ativar animação de andar
        anim.SetBool("isWalking", moveInput.x != 0f);

        Move();
        Jump();
    }

    private void Move()
    {
        moveInput = MoveAction.ReadValue<Vector2>();
        Flip(moveInput.x);

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        jumpPressed = JumpAction.WasPressedThisFrame();

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (jumpPressed && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;

            // ✅ Som do pulo aqui
            AudioManager.instance.Play("Quack");
        }
    }

    void Flip(float moveInput)
    {
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
