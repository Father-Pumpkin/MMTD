using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	public int startMoney = 400;
	public static int Lives;
	public int startLives = 20;
    public static int Mana;
    public int startMana = 0;
    public static int highestRounds;
    public static int highestEnemies;

    public static int numEnemiesKilled;
    public static int rounds;
    public int roundsPublic;
    public int numEnemiesKilledPublic;
    public static int numEnemiesAlive;

	void Start()
	{
        numEnemiesKilled = 0;
        rounds = 0;
        numEnemiesAlive = 0;
		Money = startMoney;
		Lives = startLives;
        Mana = startMana;
	}

    private void Update()
    {
        roundsPublic = rounds;
        numEnemiesKilledPublic = numEnemiesKilled;
    }
}
