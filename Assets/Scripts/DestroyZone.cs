using UnityEngine;

public class DestroyZone : MonoBehaviour
{  
    [SerializeField] private string targetTag = "FallingObject";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
        }
    }
}
