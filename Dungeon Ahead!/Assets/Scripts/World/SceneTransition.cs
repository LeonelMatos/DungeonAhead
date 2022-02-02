using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("The next scene to load when entering the trigger.")]
    public string sceneToLoad;

    public Vector2 playerPosition;

    public VectorValue playerStorage;
    private Image transitionPanel;

    private Player player;
    private PlayerStats playerStats;
    private Effects effects;

    private LevelData savedLevelData;

    private void Start()
    {
        StartCoroutine("SceneLoadingEnd");
    }

    /**Activated when entering the trigger, saves all player's data to the playerStorage (VectorValue).
    \arg Collider2D other (SceneTransition's collider that triggers the function)
    */
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            player = other.GetComponent<Player>();
            playerStats = other.GetComponent<PlayerStats>();
            effects = other.GetComponent<Effects>();

            Debug.Log("Entering " + sceneToLoad);
            playerStorage.initialValue = playerPosition;

            //Inventory
            playerStorage.storedInventory = player.inventory.GetItemList();
            //PlayerStats
            playerStorage.maxHealth = playerStats.maxHealth;
            playerStorage.health = playerStats.health;
            playerStorage.MaxEnergy = playerStats.maxEnergy;
            playerStorage.energy = playerStats.energy;
            playerStorage.storedQuests = playerStats.questList;
            //Effects
            playerStorage.activeEffects = effects.activeEffects;
            playerStorage.timeCounter = effects.timeCounter;
            //LevelData
            updateLevelData();


            //LoadScene
            transitionPanel = GameObject.FindGameObjectWithTag("TransitionPanel").GetComponent<Image>();
            StartCoroutine("SceneLoadingStart");
        }
    }

    private void OnApplicationQuit() //On editor quit?! idk
    {
        //Resets VectorValue.
        Debug.Log("Resetting values.");
        playerStorage.initialValue = Vector2.zero;
        playerStorage.storedInventory.Clear();
        playerStorage.maxHealth = 100;
        playerStorage.health = 100;
        playerStorage.MaxEnergy = 100;
        playerStorage.energy = 100;
        playerStorage.storedQuests.Clear();
        playerStorage.activeEffects.Clear();
        playerStorage.timeCounter.Clear();
    }

    ///Sets and starts the transition while changing scenes.
    public IEnumerator SceneLoadingStart()
    {
        transitionPanel.transform.SetAsLastSibling();
        float alpha = 0;
        for (int i = 0; i < 10; i++)
        {
            transitionPanel.color = new Color(0, 0, 0, alpha += 0.1f);
            yield return new WaitForSeconds(0.02f);
        }

        SceneManager.LoadScene(sceneToLoad);
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        /*while(!asyncOperation.isDone)
        {
            yield return null;
        }*/
    }

    ///Ends the transition after changing scenes.
    public IEnumerator SceneLoadingEnd()
    {
        transitionPanel = GameObject.FindGameObjectWithTag("TransitionPanel").GetComponent<Image>();
        float alpha = 1;
        transitionPanel.color = Color.black;
        for (int i = 0; i < 10; i++)
        {
            transitionPanel.color = new Color(0, 0, 0, alpha -= 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        transitionPanel.transform.SetAsFirstSibling();
    }

    /** 
    \todo Add isPrePlaced param to item.
    */
    public void updateLevelData()
    {
        bool foundActiveScene = false;

        //Checks if current scene in on the sceneList, if not, adds it.
        for (int i = 0; i < savedLevelData.sceneList.Count; i++)
        {
            if (savedLevelData.sceneList[i].sceneName == SceneManager.GetActiveScene().name)
                foundActiveScene = true;
        }

        if (!foundActiveScene)
        {
            savedLevelData.sceneList.Add(new SceneItemList { sceneName = SceneManager.GetActiveScene().name });
            foundActiveScene = false;
        }

        //Add items
    }
}
