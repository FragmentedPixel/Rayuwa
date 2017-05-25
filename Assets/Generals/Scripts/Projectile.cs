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
        target = _target;
        damage = _damage;
        attacker = _attacker;
        StartCoroutine(FollowTargetCR());
    }

    private IEnumerator FollowTargetCR()
    {
        while(target !=  null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("Unit"))
            return;

        StopAllCoroutines();
        DamageTarget();
        Destroy(gameObject);
    }

    public abstract void DamageTarget();

}
