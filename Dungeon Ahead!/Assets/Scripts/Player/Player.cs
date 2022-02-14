using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public InventoryUI inventoryUI;
    private PlayerStats playerStats;
    public Effects effects;
    public PlayerControls playerControls;
    private PlayerMovement playerMovement;
    [HideInInspector]
    public KeyCode interact;
    [HideInInspector]
    public KeyCode runKey;
    [HideInInspector]
    public KeyCode invLog;
    [HideInInspector]
    public KeyCode healthDebug;
    [HideInInspector]
    public KeyCode openInv0;
    [HideInInspector]
    public KeyCode openInv1;

    public Inventory inventory; //Update: private -> public (for QuestGoal reference)
    public GameObject npc;
    public Tooltip windowTooltip;
    public GameObject textWindow;

    public VectorValue startingPosition;
    public LevelData storedLevelData;

    private void Awake()
    {
        inventoryUI.gameObject.SetActive(true);
        inventoryUI.SetPlayer(this);//Update: Start ->> Awake
        playerStats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();

        interact = playerControls.Interact;
        runKey = playerControls.RunKey;
        openInv0 = playerControls.OpenInv0;
        openInv1 = playerControls.OpenInv1;
        invLog = playerControls.InvLog;
        healthDebug = playerControls.HealthDebug;
    }

    public Vector2 GetPosition()
    {
        Vector2 playerPos;
        return playerPos = Player.FindObjectOfType<Player>().gameObject.GetComponent<Transform>().position;
    }

    public void GetReference(Tooltip tooltip)
    {
        windowTooltip = tooltip;
    }

    private void Start()
    {
        inventory = new Inventory(UseItem);
        inventoryUI.SetInventory(inventory);
        inventory.GetPlayer(gameObject);
        effects = GetComponent<Effects>();
        effects.SetReferences();

        textWindow = GameObject.FindGameObjectWithTag("TextWindow");

        StartNewScene();
    }

    private void StartNewScene()
    {
        transform.position = startingPosition.initialValue;

        playerStats.maxHealth = startingPosition.maxHealth;
        playerStats.health = startingPosition.health;
        playerStats.maxEnergy = startingPosition.MaxEnergy;
        playerStats.energy = startingPosition.energy;
        playerStats.questList = startingPosition.storedQuests;

        effects.activeEffects = startingPosition.activeEffects;
        effects.timeCounter = startingPosition.timeCounter;

        if (effects.activeEffects.Count != 0)
        {
            int count = effects.activeEffects.Count;
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Effect: " + effects.activeEffects[0] + " time: " + effects.timeCounter[0]);
                int effect = effects.activeEffects[0];
                effects.activeEffects.RemoveAt(0);
                effects.time = effects.maxTime - effects.timeCounter[0] - 1;
                effects.timeCounter.RemoveAt(0);
                effects.SetEffect(effect);
            }
        }
    }

    ///Grab dropped items
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld == null) return;

        //Touching item
        inventory.AddItem(itemWorld.GetItem());
        //If the player grabs a prePlaced item, it will add it to the lvlDat that will later check if such item
        //was prePlaced so it knows not to place it again because the player already grabbed it.
        if (itemWorld.GetItem().isPrePlaced)
        {
            for (int i = 0; i < storedLevelData.sceneList.Count; i++)
            {
                if (storedLevelData.sceneList[i].sceneName == SceneManager.GetActiveScene().name)
                {
                    storedLevelData.sceneList[i].itemList.Add(new ItemHolder { item = itemWorld.GetItem(), x = collider.transform.position.x, y = collider.transform.position.y });
                    Debug.Log($"Item {itemWorld.GetItem().itemType} saved to LevelData.");
                    break;
                }
            }
        }
        //If the player grabs a not prePlaced item, it will remove it from the lvlDat because it's not necessary anymore
        //to save a item that was grabbed by the player.
        else
        {
            for (int i = 0; i < storedLevelData.sceneList.Count; i++)
            {   
                if (storedLevelData.sceneList[i].sceneName == SceneManager.GetActiveScene().name)
                {
                    foreach (ItemHolder holder in storedLevelData.sceneList[i].itemList)
                        if (holder.item == itemWorld.GetItem())
                        {
                            storedLevelData.sceneList[i].itemList.Remove(holder);
                            Debug.Log($"Item {itemWorld.GetItem().itemType} removed from the LevelData.");
                            break;
                        }
                }
            }
        }


        //To LevelData: sets item to isDestroyed = true
        ///\bug Check if this is useful or not.
        ///\todo When user grabs item lvlDat.
        //itemWorld.GetItem().isDestroyed = true;
        //LevelData save goes here
        itemWorld.DestroySelf();
    }

    void Update()
    {
        ///\bug To update : switch (perhaps?! idk bruh, but thats alotof ifs)
        if (Input.GetKeyDown(invLog))  //Check items in inventory @ Inventory script
        {
            inventory.InventoryItemsLog();
        }
        if (Input.GetKeyDown(openInv0) || Input.GetKeyDown(openInv1))
        {
            inventoryUI.OpenInventory();
            windowTooltip.HideTooltip_Public();
        }
        if (Input.GetKeyDown(healthDebug))
        {
            playerStats.TakeDamage(10);
            playerStats.TakeEnergy(10);
        }
        if (Input.GetKeyDown(playerControls.Pause))
        {
            GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().PauseGame();
        }

        //To update!
        /*if (Input.GetKeyDown("p"))
        {
            npc.SetActive(true);
        }*/
    }

    private void RemoveUsedItem(Item.ItemType ItemType)
    {
        inventory.RemoveItem(new Item { itemType = ItemType, amount = 1 });
        windowTooltip.HideTooltip_Public();
        //Debug.Log("Removed " + ItemType);
    }

    private void SetTimeEffect(int time)    //May be useless, idk (keep it to be safe=)
    {
        StartCoroutine(UsageTimeEffect(time));
    }

    ///To add visual output
    IEnumerator UsageTimeEffect(int time)
    {
        Debug.Log("Started effect");
        yield return new WaitForSeconds(time);  //May be useless, idk (keep it to be safe=)
        Debug.Log("Ended effect");
    }

    ///Use Items utilities here!
    ///\todo create a new script to hold all the uses of the items.
    private void UseItem(Item item)
    {
        windowTooltip.HideTooltip_Public();
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                //Debug.Log("Used HealthPotion");
                if (playerStats.health != playerStats.maxHealth)
                {
                    RemoveUsedItem(Item.ItemType.HealthPotion);
                    playerStats.TakeDamage(-20);
                }
                else
                    StartCoroutine(maxValueAnimation(GameObject.FindGameObjectWithTag("HealthBar/Icon")));  //Full bar anim.
                break;
            case Item.ItemType.EnergyPotion:
                //Debug.Log("Used EnergyPotion");
                if (playerStats.energy != playerStats.maxEnergy)
                {
                    RemoveUsedItem(Item.ItemType.EnergyPotion);
                    playerStats.TakeEnergy(-20);
                }
                else
                    StartCoroutine(maxValueAnimation(GameObject.FindGameObjectWithTag("EnergyBar/Icon")));
                break;
            case Item.ItemType.Medkit:
                //Debug.Log("Used Medkit");
                if (playerStats.health < playerStats.maxHealth)
                {
                    RemoveUsedItem(Item.ItemType.Medkit);
                    RemoveUsedItem2(item);
                    playerStats.TakeDamage(-50);
                }
                else
                {
                    StartCoroutine(maxValueAnimation(GameObject.FindGameObjectWithTag("HealthBar/Icon")));
                }
                break;
            case Item.ItemType.NightVisionPotion:
                //Debug.Log("Used NightVisionPotion");
                RemoveUsedItem(Item.ItemType.NightVisionPotion);
                effects.SetEffect(1);   //Default: 180 (3 min.)
                break;
            case Item.ItemType.Milk:
                //Debug.Log("Used Milk");
                RemoveUsedItem(Item.ItemType.Milk);
                effects.UseMilk();
                break;
            case Item.ItemType.SpeedPotion:
                //Debug.Log("Used SpeedPotion");
                RemoveUsedItem(Item.ItemType.SpeedPotion);
                effects.SetEffect(2);    //Default: 180 (3 min.)
                break;
            case Item.ItemType.Book:   //BOOK
                textWindow.GetComponent<TextWindow>().OpenTextWindow(item, inventoryUI);
                break;
            case Item.ItemType.RandomPotion:
                effects.UseRandomPotion();
                RemoveUsedItem(Item.ItemType.RandomPotion);
                break;
        }
    }

    private void RemoveUsedItem2(Item item)
    {
        windowTooltip.HideTooltip_Public();
        inventory.RemoveItem(item);
    }

    //Save/Load test debug
    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().StartCoroutine(GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().LoadTransitionStart());

        PlayerData data = SaveSystem.LoadGame();

        //TODO (I think it works, idk never tested... It just works~)
        if (gameObject.scene.name != data.loadedLevel)
        {
            SceneManager.LoadScene(data.loadedLevel);
        }

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        inventory.itemList = data.storedInventory;
        playerStats.maxHealth = data.maxHealth;
        playerStats.health = data.health;
        playerStats.maxEnergy = data.maxEnergy;
        playerStats.energy = data.energy;

        playerStats.questList = data.storedQuests;

        effects.activeEffects = data.activeEffects;
        effects.timeCounter = data.timeCounter;

        GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().StartCoroutine(GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>().LoadTransitionEnd());
    }

    //Health on max Animation
    //Used on UseItem() at Player for visual feedback
    public IEnumerator maxValueAnimation(GameObject image)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                image.transform.localScale += new Vector3(0.03f, 0.03f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
            for (int j = 0; j < 10; j++)
            {
                image.transform.localScale += new Vector3(-0.03f, -0.03f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
    }
}