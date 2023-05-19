using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public int count;
    public float spawnInterval;
}

public class WaveSystem : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<Wave> waves;

    public UnityEvent OnLastWaveCompleted;

    private int _currentWaveIndex = 0;
    private bool _isSpawning = false;
    private bool _isWaveActive = false;
    private List<Transform> _availableSpawnPoints;
    private int _enemyCount;

    private void Start()
    {
        _availableSpawnPoints = new List<Transform>(spawnPoints);
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        _isWaveActive = true;
        _enemyCount = wave.count;

        // Reset available spawn points for this wave
        _availableSpawnPoints.Clear();
        _availableSpawnPoints.AddRange(spawnPoints);

        for (int i = 0; i < wave.count; i++)
        {
            if (_availableSpawnPoints.Count == 0)
            {
                Debug.LogError("No available spawn points!");
                yield break;
            }

            int randomIndex = Random.Range(0, _availableSpawnPoints.Count);
            Transform spawnPoint = _availableSpawnPoints[randomIndex];
            _availableSpawnPoints.RemoveAt(randomIndex);

            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            var enemyHealth = enemy.GetComponent<EnemyHealth>();

            // Subscribe to the enemy's OnDeath event using a delegate
            enemyHealth.OnDeath.AddListener(() => HandleEnemyDeath(enemyHealth));

            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    private void HandleEnemyDeath(EnemyHealth enemyHealth)
    {
        enemyHealth.OnDeath.RemoveListener(() => HandleEnemyDeath(enemyHealth)); // Unsubscribe from the event

        _enemyCount--;

        if (_enemyCount <= 0 && _isWaveActive)
        {
            _isWaveActive = false;
            _currentWaveIndex++;
            _isSpawning = false;

            if (_currentWaveIndex >= waves.Count)
            {
                OnLastWaveCompleted.Invoke();
            }
        }
    }

    private void Update()
    {
        if (_isSpawning || _currentWaveIndex >= waves.Count || _isWaveActive)
        {
            return;
        }
    }

    public void StartFirstWave()
    {
        if (_currentWaveIndex == 0)
        {
            StartCoroutine(SpawnWave(waves[_currentWaveIndex]));
            _isSpawning = true;
        }
    }
}
