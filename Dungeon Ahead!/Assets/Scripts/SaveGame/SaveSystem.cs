using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /////////// Save Game ////////////
    public static void SaveGame (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.bruh";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        PlayerData data = new PlayerData(player);
        Debug.Log("Game saved");
        formatter.Serialize(stream, data);
        stream.Close();
        //Notification
        GameObject.FindGameObjectWithTag("Notification").GetComponent<Notification>().StartNotification("Game saved.", "system");
    }

    public static PlayerData LoadGame()
    {
        //Notification
        GameObject.FindGameObjectWithTag("Notification").GetComponent<Notification>().StartNotification("Game loaded.", "system");
        Debug.Log("Game loaded");
        string path = Application.persistentDataPath + "/save.bruh";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        } else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
        
    }
    //////////// Level Data ////////////
    //Ah, yes... my old enemy. So we finally meet
    //This code is stupid. I want to kill it, but at this point killing it with fire will destroy the whole game.
    //So this stays, inactive... a memory of past struggle and effort.
    public static void SaveLevelData(GameObject player, Item item)  //To be continued
    {
        string path = Application.persistentDataPath + "/lvldat.bruh";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate); //Open->opnOrCreate

        LevelData levelData = formatter.Deserialize(stream) as LevelData;

        //ADD ITEM HERE                 /////////To be continued (this part... agains)
        for (int i = 0; i <= levelData.sceneList.Count; i++)    //Search for scene
        {
            if (player.scene.name == levelData.sceneList[i].sceneName)  //Não posso simplesmente pega no levelData pq de alguma forma
            {                                                           //dá erro de instantiate. Não consigo criar um novo gameObject para guardar
                levelData.sceneList[i].itemList.Add(item);              //uma cópia de levelData pq esse n tem MonoBehaviour
            }
        }

        //DEBUG TO CHECK ITEMS IN LIST
        Debug.Log(levelData.sceneList[0].sceneName);
        for (int i = 0; i <= levelData.sceneList[0].itemList.Count; i++)
        {
            Debug.Log(levelData.sceneList[0].itemList[i].itemType);
        }

        /*for (int i = 0; i < 100; i++)
        {
            levelData.sceneList.Add(new SceneItemList(player));
            levelData.sceneList[i].sceneName = i.ToString();
        }*/


        /*for (int i = 0; i <= levelData.sceneList.Count; i++)
        {
            if (levelData.sceneList[i].sceneName == player.scene.name) {
                levelData.sceneList[i].itemList.Add(item);
            }
            else
            {
                levelData.sceneList.Add(new SceneItemList(player));
                levelData.sceneList[levelData.sceneList.Count].itemList.Add(item);
            }
        }*/
        
        //formatter.Serialize(stream, levelData);
        stream.Close();
        Debug.Log($"Saved levelData at {path}");
    }
    
}
