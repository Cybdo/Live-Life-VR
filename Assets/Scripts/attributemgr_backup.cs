/*
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


    public void TakeDamage(int amount, GameObject target)
    {
        var atm = target.GetComponent<AttributeMgr>();
        if (!Invincible)
        {
            if (atm != null)
            {
                if (!player)
                {
                    health -= amount;
                }
                else
                {
                    if (!atm.playerWeapon) { health -= amount; }
                }
            }
        }
    }
    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributeMgr>();
        if (atm != null)
        {
            atm.TakeDamage(attack, gameObject);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!player) { DealDamage(collision.gameObject); }
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (player)
            {
                PlayerGameOver.gameOver();

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
*/
