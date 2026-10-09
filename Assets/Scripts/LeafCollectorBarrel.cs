using UnityEngine;

public class LeafCollectorBarrel : MonoBehaviour
{
    [SerializeField] private string targetTag = "FallingObject";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            //Play sound burn
            Destroy(other.gameObject);

            //Logique increment money
        }
    }
}
