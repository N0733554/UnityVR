using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ProjectileSpawner : MonoBehaviour
{
    public Wand wand;
    public GameObject[] projectiles;

    void Update()
    {
        if(wand.canFire && wand.WasCastButtonReleased())
        {
            wand.CastSpell();
            if(projectiles[0] != null)
            {
                Instantiate(projectiles[0], transform.position, transform.rotation);
            }
            else
                Debug.Log("Projectile Not Found!");
        }
    }
}
