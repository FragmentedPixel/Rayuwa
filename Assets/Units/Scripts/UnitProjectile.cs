using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : Projectile
{
    public override void DamageTarget()
    {
        Destroy(gameObject);

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
        {
            Destroy(gameObject);
            return;
        }

        enemyHealth.Hit(damage,attacker);
    }
}
