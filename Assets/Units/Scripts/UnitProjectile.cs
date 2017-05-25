using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProjectile : Projectile
{
    public override void DamageTarget()
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        enemyHealth.Hit(damage);
    }
}
