using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSwpaner : MonoBehaviour
{ 
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;
    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    [System.Serializable]
    public class Wave
    {
        public GameObject preFap;
        public float spawnTimer;
        public float spawnInterval;
        public int objectPerWave;
        public int spawnObjectCount;

    }

    void Update()
    {
        waves[waveNumber].spawnTimer += (Time.deltaTime * PlayerController.Instance.boost);

        // Time to spawn next object
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0f;
            SpawnObject();
        }

        // Wave complete, reset and go to next wave
        if (waves[waveNumber].spawnObjectCount >= waves[waveNumber].objectPerWave)
        {
            waves[waveNumber].spawnObjectCount = 0;
            waveNumber++;

            // If last wave, go back to first wave
            if (waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }

    private void SpawnObject()
    {
        Instantiate(waves[waveNumber].preFap, RandomSpawnPoint(), transform.rotation);
        waves[waveNumber].spawnObjectCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;
        spawnPoint.x = minPos.position.x;
        spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPoint;
    }
}
