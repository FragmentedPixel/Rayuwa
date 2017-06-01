using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnitController : UnitController
{
    #region Melee Attacking
    public override void FightTarget()
    {
        SwordHit();
    }
    public override string GetAmmoText()
    {
        string ammoText = ((ammo * 100f) / maxAmmo ).ToString() + "%";
        return ammoText;
    }
    public void SwordHit()
    {
        target.GetComponent<EnemyHealth>().Hit(fightDmg,transform);
    }
    #endregion
}
