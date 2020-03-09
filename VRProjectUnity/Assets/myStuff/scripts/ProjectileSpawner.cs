using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ProjectileSpawner : MonoBehaviour
{
    public Wand wand;
    public Transform projectile;

    void Update()
    {
        if(wand.canFire && wand.WasCastButtonReleased())
        {
            wand.CastSpell();
            if(projectile != null)
            {
                Debug.Log("ProjectileSpawner: " + transform.forward);

                Instantiate(projectile, transform.position, transform.rotation);
            }
            else
                Debug.Log("Projectile Not Found!");
        }
    }
}
