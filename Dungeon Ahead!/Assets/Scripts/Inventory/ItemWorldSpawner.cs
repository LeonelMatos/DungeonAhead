using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

///Spawns the item in the world, defined in the editor. Kills itself (sad).
public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    ///\bug This reference does not hold the lvlDat in Assets, it=?. Check this script in editor. Same in Player.
    public LevelData storedLevelData;

    /**\todo Before placing the object, check if it was
    grabbed before (info on lvlDat), if yes: don't place it,
    if not: do it! Just do it!*/

    private void Start()
    {
        //1.Check witch scene I'm at.
        for (int i = 0; i < storedLevelData.sceneList.Count; i++) {
            if (storedLevelData.sceneList[i].sceneName != SceneManager.GetActiveScene().name) continue;
                //2.Search for the same item in the same pos.
                foreach(ItemHolder holder in storedLevelData.sceneList[i].itemList) {
                    //3.Check if it exists.
                    if (holder.x == transform.position.x && holder.y == transform.position.y)
                        break;
                    else {
                        ItemWorld.SpawnItemWorld(transform.position, new Item{isPrePlaced = true, itemType = item.itemType, amount = item.amount, itemText = item.itemText});
                        Destroy(gameObject);
                        break;
                    }

                }
        }
    }
}
