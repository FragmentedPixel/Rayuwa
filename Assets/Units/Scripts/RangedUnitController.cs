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
        anim.SetTrigger("RangedAttack");
    }
    public override string GetAmmoText()
    {
        string ammoText = ammo.ToString() + "/" + maxAmmo.ToString();
        return ammoText;
    }
    public void FireProjectile()
    {
        audioS.PlayOneShot(fightSound);
        GameObject projectileGO = Instantiate(projectile, shootingPoint.position, shootingPoint.rotation);
        projectileGO.GetComponent<UnitProjectile>().FireProjectile(target, fightDmg, transform);
    }
    #endregion
}
