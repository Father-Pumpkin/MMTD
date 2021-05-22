using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public int rounds;
    public int enemiesKilled;

    public SaveData (PlayerStats player)
    {
        rounds = player.roundsPublic;
        enemiesKilled = player.numEnemiesKilledPublic;
    }
}
