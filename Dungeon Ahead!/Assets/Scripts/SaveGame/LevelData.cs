using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public List<SceneItemList> sceneList;

    //LevelData created at MenuManager.
    //Useless tho
}

public class SceneItemList
{
    public string sceneName;

    public List<Item> itemList;

    public SceneItemList(GameObject player)
    {
        sceneName = player.scene.name;
    }
}