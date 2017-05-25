using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{

    [Header("Projectile")]
    public GameObject projectile;
    public Transform shootingPoint;

    public override void AttackTarget()
    {
        anim.SetTrigger("RangedAttack");
        Debug.Log("Ranged");
    }
    
    public void RangedHit()
    {
        GameObject projectileGO = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        projectileGO.GetComponent<EnemyProjectile>().FireProjectile(target, attackDmg, transform);
    }
}
