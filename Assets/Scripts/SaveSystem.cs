using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayerStats(PlayerStats player)
    {
        if(PlayerStats.highestEnemies >= PlayerStats.numEnemiesKilled && PlayerStats.highestRounds >= PlayerStats.rounds)
        {
            return;
        }
        else if (PlayerStats.rounds > PlayerStats.highestRounds)
        {
            PlayerStats.highestRounds = PlayerStats.rounds;
        }
        if (PlayerStats.numEnemiesKilled > PlayerStats.highestEnemies)
        {
            PlayerStats.highestEnemies = PlayerStats.numEnemiesKilled;
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found. Creating now");
            FileStream stream = new FileStream(path, FileMode.Create);
            return null;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            PlayerStats.highestEnemies = data.enemiesKilled;
            PlayerStats.rounds = data.rounds;
            stream.Close();
            return data;
            
        }
    }
   
}
