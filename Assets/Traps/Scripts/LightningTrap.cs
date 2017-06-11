using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    public float damage;
    public float cooldown;
    public float warnningTime;
    public float duration;
    public ParticleSystem lightningParticules;
    public ParticleSystem rainParticules;

    private new BoxCollider collider;
    private new Renderer renderer;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<Renderer>();

        InvokeRepeating("Trap", .3f, cooldown);
    }

    public void Trap()
    {
        StartCoroutine(TriggerTrap());
    }

    private IEnumerator TriggerTrap()
    {
        rainParticules.Play();
        yield return new WaitForSeconds(3f);

        renderer.enabled = true;
        yield return new WaitForSeconds(warnningTime);

        collider.enabled = true;
        lightningParticules.Play();

        yield return new WaitForSeconds(duration);
        lightningParticules.Stop();

        yield return new WaitForSeconds(.3f);
        rainParticules.Stop();

        collider.enabled = false;
        renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {        
        UnitHealth unit = other.GetComponent<UnitHealth>();
        if (unit != null)
            unit.Hit(damage, null);
    }

}
