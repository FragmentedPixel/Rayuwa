using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnitController : UnitController
{
    public override void FightTarget()
    {
        Debug.Log("Melee attack animation");
        SwordHit();
    }
    public override string GetAmmoText()
    {
        string ammoText = ((ammo * 100f) / maxAmmo ).ToString() + "%";
        return ammoText;
    }
    public void SwordHit()
    {
        target.GetComponent<EnemyHealth>().Hit(fightDmg);
    }
}
