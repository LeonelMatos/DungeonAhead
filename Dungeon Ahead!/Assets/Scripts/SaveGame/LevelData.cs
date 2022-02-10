/** Script that is used to store a list of scenes for which holds the scene's name and
a list of items in order to be positioned when the called scene resets*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**Script LevelData used as ScriptableObject (template) located in Scripts/SaveGame
containing a sceneList for SceneItemList.
\note LevelData will be used at SceneTransition.
\todo Add a method to sort through the sceneList to see if active scene is in the list,
if not, add it.
*/
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public List<SceneItemList> sceneList;

    //LevelData created at MenuManager.
    //Useless tho
}
/**Script SceneItemList is a class type that represents each scene and holds
the itemList that lists all items that will be saved and loaded when this scene resets.
*/
public class SceneItemList
{
    ///String that holds the SceneItemList's scene name.
    public string sceneName;

    ///List of Item class that stores the items in the world scene.
    public List<ItemHolder> itemList;

    public SceneItemList()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
}

///A class that holds the item's info and it's position, used on itemList;
public class ItemHolder
{
    public Item item;

    public float x, y;
}