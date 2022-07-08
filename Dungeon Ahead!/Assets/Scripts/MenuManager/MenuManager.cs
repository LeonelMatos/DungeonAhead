using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuManager : MonoBehaviour
{

    public SceneTransition sceneTransition;
    public void LoadStartingGameScene()
    {
        sceneTransition.StartCoroutine("SceneLoadingStart");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Create for the first time the LevelData

    private void Start()
    {
        /*string path = Application.persistentDataPath + "/lvldat.bruh";

        if (File.Exists(path))
            Debug.Log("lvldat.bruh already exists.");
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            
            LevelData levelData = new LevelData(gameObject, new Item {itemType = Item.ItemType.Coin, amount = 1, isDestroyed = true});

            //levelData.test = "Hello World!";
            
            formatter.Serialize(stream, levelData);
            stream.Close();

            Debug.Log($"New levelData created at {path}");
        }
        */
    }

    public void NewGame()
    {
        sceneTransition.StartCoroutine("SceneLoadingStart");
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame();
    }



    [ContextMenu("Clear Saved LevelData")]
    public void ClearLvlDat()
    {
        string path = Application.persistentDataPath + "/lvldat.bruh";

        File.Delete(path);
    }

    [ContextMenu("Clear Save File")]
    public void ClearSavDat()
    {
        string path = Application.persistentDataPath + "/save.bruh";

        File.Delete(path);
    }

    [ContextMenu("Open SaveData MAIN")]
    public void OpenSaveData()
    {
        string path = Application.persistentDataPath;
    
        System.Diagnostics.Process.Start("explorer.exe", @"C:\Users\Utilizador\AppData\LocalLow\Leonel Matos\Dungeon Ahead!");
    }

    [ContextMenu("Open SaveData LAPTOP")]
    public void OpenSaveDataLaptop()
    {
        System.Diagnostics.Process.Start("explorer.exe", @"C:\Users\leone\AppData\LocalLow\Leonel Matos\Dungeon Ahead!");
    }
}
