using UnityEngine;

public class CutsceneDuckRunning : MonoBehaviour
{
    [Header("Destino")]
    public Transform patinha;

    [Header("Movimento")]
    public float speed = 2f;
    public float stopRadius = 0.08f; // aumenta se precisar

    bool walking = false;
    Animator anim;
    Rigidbody2D rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Se houver um Player na cena, desabilita controles
        var player = GameObject.FindWithTag("Player");
        if (player)
        {
            var pa = player.GetComponent<PlayerActions>();
            if (pa) pa.enabled = false;
            var prb = player.GetComponent<Rigidbody2D>();
            if (prb) prb.linearVelocity = Vector2.zero;
        }

        // Garante que física não empurra o pato da cutscene
        if (rb)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            rb.gravityScale = 0f;
        }
    }

    // Chame pelo Animation Event ou no Start se quiser que ele já comece andando
    public void StartWalk()
    {
        walking = true;
        if (anim) anim.SetBool("Walk", true);
    }

    void Update()
    {
        if (!walking || patinha == null) return;

        // Move
        Vector2 pos = transform.position;
        Vector2 alvo = patinha.position;
        Vector2 newPos = Vector2.MoveTowards(pos, alvo, speed * Time.deltaTime);
        transform.position = newPos;

        // Condição robusta de parada
        if ((alvo - newPos).sqrMagnitude <= (stopRadius * stopRadius))
        {
            // Snap no ponto final e encerra
            transform.position = patinha.position;
            walking = false;

            if (anim) anim.SetBool("Walk", false);
            if (rb) rb.linearVelocity = Vector2.zero;
        }
    }

    // Caso queira parar manualmente
    public void StopCutsceneWalk()
    {
        walking = false;
        if (anim) anim.SetBool("Walk", false);
        if (rb) rb.linearVelocity = Vector2.zero;
    }
}
