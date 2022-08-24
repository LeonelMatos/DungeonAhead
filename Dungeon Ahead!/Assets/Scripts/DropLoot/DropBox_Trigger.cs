using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
\brief Trigger component waiting for input
Only enabled when the player enters the trigger collider
to save on the task to wait for input only when necessary.
(in order to avoid the problem of having too many Dropbox components
waiting for input at the same level, at the same time).
*/
public class DropBox_Trigger : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (Input.GetKeyDown(player.interact))
        {
            GetComponent<DropBox>().OpenLoot();
        }
    }
}
