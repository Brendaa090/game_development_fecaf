using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public InputAction InteractionAction;
    bool interactionInput;
    AudioSource audioSource;

    DoorActions currentDoor;

    void Awake()
    {
        InteractionAction?.Enable();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        interactionInput = InteractionAction != null && InteractionAction.IsPressed();

        if (currentDoor != null && GameManager.Instance != null)
        {
            // Se encostou na porta com chave, abre
            if (!currentDoor.IsOpen() && GameManager.Instance.HasKey())
            {
                GameManager.Instance.UseKey();
                AudioManager.instance.Play("DoorOpen"); // ✅ som da porta
                currentDoor.OpenDoor();
            }

            // Porta já aberta e pressionou E -> passa de fase
            if (currentDoor.IsOpen() && interactionInput)
            {
                GameManager.Instance.GoToNextLevel();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Key"))
        {
            AudioManager.instance.Play("Key"); // ✅ som da chave
            if (col.CompareTag("Key"))
            {
                GameManager.Instance.AddKey();
                AudioManager.instance.Play("Key"); // 🔊 SOM DA CHAVE
                Destroy(col.gameObject);
                return;
            }

        }

        if (col.CompareTag("Heart"))
        {
            AudioManager.instance.Play("Heart"); // ✅ som do coração (se tiver)
            GameManager.Instance.IncLife();
            Destroy(col.gameObject);
            return;
        }

        if (col.CompareTag("Door"))
        {
            currentDoor = col.GetComponent<DoorActions>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Door"))
            currentDoor = null;
    }
}
