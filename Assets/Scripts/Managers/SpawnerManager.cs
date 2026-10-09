using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private List<ItemSpawner> itemSpawners = new();

    [SerializeField] private bool startAutomatically = true;
    private Coroutine _spawnCoroutine;

    [Header("Paramètres du spawn")]
    [Min(0.01f)]
    [SerializeField] private float timeBetweenSpawns = 2f;

    [Min(0f)]
    [SerializeField] private float maxSpawnDuration = 30f;

    
    private void Start()
    {
        if (startAutomatically)
            StartSpawning();
    }

    public void StartSpawning()
    {
        if (_spawnCoroutine != null)
            return;

        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < maxSpawnDuration)
        {
            float waitTime = Mathf.Min(
                timeBetweenSpawns,
                maxSpawnDuration - elapsedTime
            );

            yield return new WaitForSeconds(waitTime);
            elapsedTime += waitTime;

            if (elapsedTime >= maxSpawnDuration)
                break;

            SpawnRandomItem();
        }

        _spawnCoroutine = null;
    }

    private void SpawnRandomItem()
    {
        if (itemSpawners.Count == 0)
        {
            Debug.LogWarning("Aucun ItemSpawner assigné !");
            return;
        }

        int randomIndex = Random.Range(0, itemSpawners.Count);
        ItemSpawner selectedSpawner = itemSpawners[randomIndex];

        if (selectedSpawner != null)
            selectedSpawner.SpawnItem();
        
    }
}
