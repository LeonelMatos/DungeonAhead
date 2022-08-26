using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
