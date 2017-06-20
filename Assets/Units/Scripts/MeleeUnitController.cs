using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnitController : UnitController
{
    #region Melee Attacking
    public override void FightTarget()
    {
        anim.SetTrigger("MeleeAttack");
        targetToAttack = target;
    }
    public override string GetAmmoText()
    {
        string ammoText = Mathf.RoundToInt( ((ammo * 100f) / maxAmmo ) ).ToString() + "%";
        return ammoText;
    }
    public void SwordHit()
    {
        if (targetToAttack != null && transform != null)
        {
            audioS.PlayOneShot(fightSound);
            targetToAttack.GetComponent<EnemyHealth>().Hit(fightDmg, transform);
        }
    }
    #endregion
}
