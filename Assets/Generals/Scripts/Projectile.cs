using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public Transform target;
    [HideInInspector] public Transform attacker;

    public void FireProjectile(Transform _target, float _damage, Transform _attacker)
    {
        attacker = _attacker;
        damage = _damage;
        target = _target;
        StartCoroutine(FollowTargetCR());
    }

    private IEnumerator FollowTargetCR()
    {
        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        DamageTarget();
    }

    public abstract void DamageTarget();

}
