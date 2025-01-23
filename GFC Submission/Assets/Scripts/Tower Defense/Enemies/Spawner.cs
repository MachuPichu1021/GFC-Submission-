using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnOperation
{
    [Tooltip("Type of enemy to spawn. See enemyPrefabs for indexes")]
    public int type;
    [Tooltip("Number of enemies to spawn in this operation")]
    public int count;
    [Tooltip("Time between each enemy spawning in seconds")]
    public float delay;
}

[System.Serializable]
public struct Wave
{
    public SpawnOperation[] operations;
    [Tooltip("The amount of money to award at the end of this wave")]
    public int waveReward;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    private int waveIndex = -1;
    private Wave currentWave;

    private float enemySpawnTimer;
    private bool waveOngoing;
    private int currentOperationIndex;

    [SerializeField] private GameObject[] enemyPrefabs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !waveOngoing)
            StartNextWave();
    }

    private void FixedUpdate()
    {
        if (waveOngoing)
        {
            enemySpawnTimer -= Time.fixedDeltaTime;
            if (currentOperationIndex < currentWave.operations.Length && enemySpawnTimer <= 0)
            {
                SpawnEnemy();
                if (currentWave.operations[currentOperationIndex].count == 0)
                    currentOperationIndex++;
            }
            else if (currentOperationIndex == currentWave.operations.Length && FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0)
                OnEndOfWave();
        }
    }

    private void StartNextWave()
    {
        waveIndex++;
        currentWave = waves[waveIndex];
        waveOngoing = true;
    }

    private void SpawnEnemy()
    {
        currentWave.operations[currentOperationIndex].count--;
        enemySpawnTimer = currentWave.operations[currentOperationIndex].delay;
        Instantiate(enemyPrefabs[currentWave.operations[currentOperationIndex].type]);
    }

    private void OnEndOfWave()
    {
        MoneyManager.instance.ChangeMoney(currentWave.waveReward);
        waveOngoing = false;
    }
}
