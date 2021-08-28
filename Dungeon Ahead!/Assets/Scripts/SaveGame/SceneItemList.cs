using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItemList
{
    public SceneItemList (GameObject player)
    {
        sceneName = player.scene.name;
        
        //Item.isDestroyed gets false on ItemWorld and true on Player

        //Search for all items in the world
    }

    public string sceneName;

    public List<Item> itemList;
}
