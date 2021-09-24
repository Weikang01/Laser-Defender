using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

	// Use this for initialization
	IEnumerator Start () {
        do
        {
            yield return StartCoroutine(spawnAllWaves());
        }
        while (looping);
        
	}

    private IEnumerator spawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(spawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator spawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        if(waveConfig.GetNumberOfEnemies() > 0)
        {
            for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
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

	// Update is called once per frame
	void Update () {
		
	}
}
