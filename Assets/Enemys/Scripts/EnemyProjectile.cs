using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public override void DamageTarget()
    {
        UnitHealth unitHealth = target.GetComponent <UnitHealth> ();
        unitHealth.Hit(damage, attacker);
    }
}
