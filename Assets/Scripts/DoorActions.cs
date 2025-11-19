using UnityEngine;

public class DoorActions : MonoBehaviour
{
    [SerializeField] GameObject textBubble;
    Animator anim;
    bool isOpen = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (textBubble != null)
            textBubble.SetActive(false);
    }

    public bool IsOpen() => isOpen;

    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
        anim.SetTrigger("isOpen");
        AudioManager.instance.Play("Unlock"); // 🔊 SOM PORTA

        if (textBubble != null)
            textBubble.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && GameManager.Instance.HasKey() && !isOpen)
        {
            GameManager.Instance.UseKey();
            OpenDoor();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && isOpen && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.GoToNextLevel();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && textBubble != null)
            textBubble.SetActive(isOpen);
    }
}
