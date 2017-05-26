using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnitController : UnitController
{
    [Header("Projectile")]
    public GameObject projectile;
    public Transform shootingPoint;

    public override void FightTarget()
    {
        Debug.Log("Play ranged attack animation");
        FireProjectile();
    }
    public override string GetAmmoText()
    {
        string ammoText = ammo.ToString() + "/" + maxAmmo.ToString();
        return ammoText;
    }
    public void FireProjectile()
    {
        GameObject projectileGO = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        projectileGO.GetComponent<UnitProjectile>().FireProjectile(target, fightDmg, transform);
    }

}
