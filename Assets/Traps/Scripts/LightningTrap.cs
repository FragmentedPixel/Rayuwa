using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    public float cooldown;
    public float warnningTime;
    public float duration;
    public ParticleSystem lightningParticules;

    private Collider collider;
    private MeshRenderer renderer;

    private void Start()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();

        InvokeRepeating("Trap", .3f, cooldown);
    }

    private void Trap()
    {
        StartCoroutine(TriggerTrap());
    }

    private IEnumerator TriggerTrap()
    {
        renderer.enabled = true;
        yield return new WaitForSeconds(warnningTime);

        collider.enabled = true;
        lightningParticules.Play();
        yield return new WaitForSeconds(duration);
        lightningParticules.Stop();

        collider.enabled = false;
        renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {        
        UnitHealth unit = other.GetComponent<UnitHealth>();
        if (unit != null)
            unit.Die();
    }

}
