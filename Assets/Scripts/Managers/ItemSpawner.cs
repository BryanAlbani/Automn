
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs = new();

    [SerializeField] private Transform spawnPoint;

    public void SpawnItem()
    {
        if (itemPrefabs.Count == 0)
        {
            Debug.LogWarning($"No prefab  {name}");
            return;
        }

        int randomIndex = Random.Range(0, itemPrefabs.Count);
        GameObject selectedPrefab = itemPrefabs[randomIndex];

        if (selectedPrefab == null)
            return;

        Transform point = spawnPoint != null ? spawnPoint: transform;

        Instantiate(selectedPrefab, point.position, point.rotation);
    }
}
