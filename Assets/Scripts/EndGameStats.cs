using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour
{
    public GameObject enemiesGO;
    public GameObject roundsGO;
    private Text numEnemies;
    private Text numRounds;
    private void Start()
    {
        numEnemies = enemiesGO.GetComponent<Text>();
        numRounds = roundsGO.GetComponent<Text>();
    }
    private void Update()
    {
        if (GameManager.GameOver)
        {
            setStats();
        }
    }
    public void setStats()
    {
        numEnemies.text = PlayerStats.numEnemiesKilled.ToString();
        numRounds.text = PlayerStats.rounds.ToString();
    }
}
