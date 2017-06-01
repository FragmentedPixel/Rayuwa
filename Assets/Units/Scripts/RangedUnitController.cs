using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnitController : UnitController
{
    #region Projectile
    [Header("Projectile")]
    public GameObject projectile;
    public Transform shootingPoint;
    #endregion

    #region Ranged Attacking
    public override void FightTarget()
    {
        FireProjectile();
    }
    public override string GetAmmoText()
    {
        string ammoText = ammo.ToString() + "/" + maxAmmo.ToString();
        return ammoText;
    }
    public void FireProjectile()
    {
        Transform hitPlace = target;
        for (int i = 0; i < target.childCount; i++)
            if (target.GetChild(i).tag == "HitPlace")
                hitPlace = target.GetChild(i).transform;
        GameObject projectileGO = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        projectileGO.GetComponent<UnitProjectile>().FireProjectile(hitPlace, fightDmg, transform);
    }
    #endregion
}
