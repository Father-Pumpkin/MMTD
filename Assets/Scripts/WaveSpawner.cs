using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour {

	public Transform enemyWeakPrefab;
    public Transform enemyStrongPrefab;
	public Transform spawnPoint;
	public float timeBetweenWaves = 20f;
	private float countdown = 2f;
    public Button waveSpawnButton;

	public Text waveCountdownText;

	private int waveNumber = 0;
	void Update()
	{
		/*if (countdown <= 0f) {
			StartCoroutine (SpawnWave());
			countdown = timeBetweenWaves;
		}
		// Will decrease by seconds fluently
		countdown -= Time.deltaTime;

		countdown = Mathf.Clamp (countdown, 0f, Mathf.Infinity);
		if (waveCountdownText != null)
		{
			waveCountdownText.text = string.Format("{0:00.0}", countdown);
		}
        */
        if(PlayerStats.numEnemiesAlive <= 0)
        {
            waveSpawnButton.interactable = true;
        }
	}


	IEnumerator SpawnWave()
	{
        PlayerStats.rounds++;
		for (int i = 0; i < waveNumber; i++) {
			SpawnEnemy ();
			yield return new WaitForSeconds (0.5f);
		}
       
	}

    public void SpawnWaveOnClick()
    {
        waveNumber++;
        Debug.Log(waveNumber);
        PlayerStats.numEnemiesAlive = waveNumber;
        Debug.Log("Num enemies to spawn " + PlayerStats.numEnemiesAlive);
        waveSpawnButton.interactable = false;
        StartCoroutine(SpawnWave());
    }

	void SpawnEnemy()
	{
        float randomizer = Random.Range(1, waveNumber);

        if (randomizer >= 5)
        {
            Instantiate(enemyStrongPrefab, spawnPoint.position, enemyStrongPrefab.rotation);
        }
        else
        {
            Instantiate(enemyWeakPrefab, spawnPoint.position, enemyWeakPrefab.rotation);
        }
	}
}
