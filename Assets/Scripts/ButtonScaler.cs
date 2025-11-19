using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour
{
    private Vector3 originalScale;
    public float scaleMultiplier = 1.2f;

    void Start()
    {
        originalScale = transform.localScale;
    }
    public void ScaleUp()
    {
        transform.localScale = originalScale * scaleMultiplier;
    }

    public void ScaleDown()
    {
        transform.localScale = originalScale; 
    }
}
