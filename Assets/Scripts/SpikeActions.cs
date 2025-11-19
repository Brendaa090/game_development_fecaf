using UnityEngine;

public class SpikeActions : MonoBehaviour
{
    [SerializeField, Tooltip("Partícula que será gerada")]
    public GameObject particlePrefab; // Referência ao prefab da partícula

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // --- Partícula (Apenas se quiser no futuro) ---
            /*
            if (particlePrefab != null)
            {
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Prefab de partícula não atribuído!");
            }
            */


            // --- Mata o player e atualiza o game ---
            Destroy(collision.gameObject);
            GameManager.Instance.DecLife();


        }
    }

    private void CheckLife()
    {
        GameManager.Instance.DecLife();
    }
}
