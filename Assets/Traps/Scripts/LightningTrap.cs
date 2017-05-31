using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    public ParticleSystem lightningParticules;
    
    public void SpawnLightning()
    {
        Instantiate(lightningParticules, transform.position, transform.rotation);
    }

}
