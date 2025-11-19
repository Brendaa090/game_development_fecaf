using UnityEngine;

public class PortalActions : MonoBehaviour
{
    [SerializeField, Tooltip("Destino do Portal")]
    private Transform destination;

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = destination.position;
        }
    }
}