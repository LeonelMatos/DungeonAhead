using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobweb : MonoBehaviour
{
    PlayerMovement playerMovement;
    public float slowness = 2.0f;   //Default: 2.0f
    public bool inCobweb = false;
    bool checkrun;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerMovement.isRunning == true)
        {
            checkrun = true;
        }
        else
        {
            checkrun = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(inCobweb == false)
        {
        if(checkrun == false)
        {
            playerMovement.runSpeed = playerMovement.runSpeed / slowness;
            
        }
        else
        {
            playerMovement.runSpeed = playerMovement.runSpeed / (slowness * 2);
        }
        }


        inCobweb = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(inCobweb == true)
        {
        if (checkrun == false)
        {
            playerMovement.runSpeed = playerMovement.runSpeed * slowness;

        }
        else
        {
            playerMovement.runSpeed = playerMovement.runSpeed * (slowness * 2);
        }
        }
        inCobweb = false;

    }
}
