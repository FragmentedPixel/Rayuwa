using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : Trap
{
    public float damage;
    public float cooldown;
    public float warnningTime;
    public float duration;
    public AudioClip lightningSound;
    public ParticleSystem lightningParticules;
    public AudioClip rainSound;
    public ParticleSystem rainParticules;

    private new BoxCollider collider;
    private new Renderer renderer;
    private AudioSource audioS;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<Renderer>();
        audioS = GetComponent<AudioSource>();
        audioS.volume = PlayerPrefsManager.GetMasterVolume();

        rainParticules.Stop();
        lightningParticules.Stop();

        InvokeRepeating("Trap", .3f, cooldown);
    }

    public void Trap()
    {
        StartCoroutine(TriggerTrap());
    }

    private IEnumerator TriggerTrap()
    {
        audioS.clip = rainSound;
        rainParticules.Play();

        audioS.PlayDelayed(1f);
        yield return new WaitForSeconds(3f);

        renderer.enabled = true;
        yield return new WaitForSeconds(warnningTime);

        collider.enabled = true;
        audioS.PlayOneShot(lightningSound);
        lightningParticules.Play();

        yield return new WaitForSeconds(duration);
        lightningParticules.Stop();

        collider.enabled = false;
        renderer.enabled = false;

        yield return new WaitForSeconds(.3f);
        rainParticules.Stop();
        yield return new WaitForSeconds(1f);
        audioS.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {        
        UnitHealth unit = other.GetComponent<UnitHealth>();
        if (unit != null)
            unit.Hit(damage, null);
    }

}
