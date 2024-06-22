using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[Serializable]
   public class StrRandomDrops
    {
        public GameObject obj;
        public int dropRate;
    }


namespace attributeMgr
{


     
    public class AttributeMgr : MonoBehaviour
    {

        public int health;
        public int attack;
        public bool player;
        public bool playerWeapon;
        public bool Invincible;
        public StrRandomDrops[] randomDrops;
        //private int dropRadius = 10;
        public int ammo;
        public int rewardAmmo;
        public bool bullet;
        public Vector3 origin;// = transform.position; // for the random spawns


        public bool ammoCheck()
        {
            if (ammo > 0) { ammo--; return true; }
            else return false;
            
        }

        private void Start()
        {
            //if (bullet) { Debug.LogError("i am a bullet"); }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var atm = collision.gameObject.GetComponent<AttributeMgr>();
            /*if (!bullet) */
            //Debug.LogError($"{gameObject.name} collided with {health} health");
            
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
                    origin = transform.position; 
                    for (int i = 0; i < randomDrops.Length; i++)
                    {
                        Vector3 randomPosition = origin + UnityEngine.Random.insideUnitSphere;
                        if (UnityEngine.Random.Range(1, 100) <= randomDrops[i].dropRate) { Instantiate(randomDrops[i].obj, randomPosition, Quaternion.identity); }
                        Debug.LogWarning($"{randomDrops[i].obj.name} has {randomDrops[i].dropRate} of spawning");
                    }
                    GameObject player = GameObject.Find("XR Origin (XR Rig)");
                    AttributeMgr playerAttr = player.GetComponent<AttributeMgr>();
                    playerAttr.ammo = playerAttr.ammo + rewardAmmo;
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
