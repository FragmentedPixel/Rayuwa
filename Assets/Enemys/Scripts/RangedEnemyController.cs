using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{

    [Header("Projectile")]
    public GameObject projectile;
    public Transform shootingPoint;

    public void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<Agent>();
        currentAmmo = maxAmmo;
    }

    public override void AttackTarget()
    {
        anim.SetTrigger("RangedAttack");
    }
    
    public void RangedHit()
    {
        GameObject projectileGO = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        projectileGO.GetComponent<EnemyProjectile>().FireProjectile(target, attackDmg, transform);
        currentAmmo--;
    }

    public override bool Ammo()
    {
        if(currentAmmo!=0)
        {
            return true;
        }
        return false;
    }
}
