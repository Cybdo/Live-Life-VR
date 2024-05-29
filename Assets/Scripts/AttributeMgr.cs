using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttributeMgr : MonoBehaviour
{
    public int health;
    public int attack;
    public bool player;
    public bool playerWeapon;
    public bool Invincible;


    

    private void OnCollisionEnter(Collision collision)
    {
        var atm = collision.gameObject.GetComponent<AttributeMgr>();
        if (atm != null)
        {
            if (!playerWeapon || !atm.player)
            {
                if (!player)
                {
                    if (!atm.Invincible) { atm.health -= attack; }
                }
            }
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (player) { PlayerGameOver.gameOver(); }
            else        { Destroy(gameObject);       }
        }
    }

}

// mother made this
// time to go do debugging
// i do not trust her
// :)
