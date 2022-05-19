using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(Stats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stats.data";
        FileStream steram = new FileStream(path, FileMode.Create);

        StatsData data = new StatsData(stats);

        formatter.Serialize(steram, data);
        steram.Close();
    }

    public static StatsData Load()
    {
        string path = Application.persistentDataPath + "/stats.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream steram = new FileStream(path, FileMode.Open);

            StatsData data = formatter.Deserialize(steram) as StatsData;
            steram.Close();
            return data;
        }
        else return null;
    }

}
