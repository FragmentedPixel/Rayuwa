using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    public ParticleSystem lightningParticules;
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        {
            SpawnLightning();
            triggered = true;
            Destroy(gameObject, 3f);
        }

        UnitHealth unit = other.GetComponent<UnitHealth>();
        if (unit != null)
            unit.Die();
    }

    public void SpawnLightning()
    {
        Instantiate(lightningParticules, transform.position, transform.rotation, transform);
    }

}
