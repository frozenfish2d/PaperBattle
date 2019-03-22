using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = true;
    [SerializeField] int wavesBeforeBoss;

    [SerializeField] WaveConfig bossWave;

    public int waveCount=1;

    IEnumerator Start() {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private void Update()
    {
        
        if (waveCount > wavesBeforeBoss)
        {
            StopAllCoroutines();
            waveConfigs.Clear();
            if (FindObjectsOfType<Enemy>().Length <= 0)
            {
                waveConfigs.Add(bossWave);
                waveCount = 1;
                StartCoroutine(SpawnAllEnemiesInWave(bossWave));
            }
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            waveCount++;
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyCount=0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }



}
