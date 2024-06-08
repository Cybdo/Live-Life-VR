using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace attributeMgr
{
    public class AttributeMgr : MonoBehaviour
    {
        public int health;
        public int attack;
        public bool player;
        public bool playerWeapon;
        public bool Invincible;
        public GameObject[] certainDrops;
        private int dropRadius = 10;
        public int ammo;
        //public Vector3 origin = Vector3.zero; // for the random spawns


        public bool ammoCheck() 
        {
            if (ammo > 0) { ammo--; return true; }
            else return false;
        }


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
                else
                {
                    for (int i = 0; i < certainDrops.Length; i++)
                    {
                        //Vector3 randomPosition = origin + UnityEngine.Random.insideUnitSphere * dropRadius;
                        Instantiate(certainDrops[i], transform.position, transform.rotation);
                        Debug.LogWarning("well it tried to spawn smt");
                    }
                    Destroy(gameObject);
                }
            }
        }

    }
}


// mother made this
// time to go do debugging
// i do not trust her
// :)
