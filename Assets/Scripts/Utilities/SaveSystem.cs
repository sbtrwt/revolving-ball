
using GAMEHIGAME.Root;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GAMEHIGAME.Utilities
{
    public static class SaveSystem
    {
        public static void SaveGameData(GameData gameData) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = string.Format("{0}/playerdata_{1}", Application.persistentDataPath, gameData.ID);
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, gameData);

            stream.Close();
        
        }

        public static GameData LoadGameData(int gameID)
        {
            string path = string.Format("{0}/playerdata_{1}", Application.persistentDataPath, gameID);
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();
                return data;

            }
            else
            {
                Debug.Log("File not found "+ path);
                return null;
            }
        }
    }

}
