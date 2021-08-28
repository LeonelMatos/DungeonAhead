using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public GameObject chestTexture;
    public Sprite chestOpen;
    public Sprite chestClosed;
    bool inTrigger; //Debug.Log
    
    private void Update()
    {
        //O código é estúpido,
        //felizmente não o vou usar.
        if (inTrigger == true)
        {
        if (Input.GetKey("e"))
        {
            OpenChest();
        }
        if (Input.GetKey("f"))
        {
            CloseChest();
        }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Debug.Log("Closed to " + gameObject.tag); //For debug stuff
            inTrigger = true;
        }
    }

    void OpenChest()
    {
        chestTexture.GetComponent<SpriteRenderer>().sprite = chestOpen;
    }

    void CloseChest()
    {
        chestTexture.GetComponent<SpriteRenderer>().sprite = chestClosed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Away from " + gameObject.tag); //For debug stuff
        inTrigger = false;
    }
}