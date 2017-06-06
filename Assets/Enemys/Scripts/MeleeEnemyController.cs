using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : EnemyController {


    public override void AttackTarget()
    {
        anim.SetTrigger("MeleeAttack");
    }

    public void MeleeHit()
    {
        if (target == null||!target.CompareTag("Unit"))
            return;

        audioS.PlayOneShot(attackSound);
        if (UnityEngine.Random.Range(0, 100) < 90)
            target.GetComponent<UnitHealth>().Hit(attackDmg, transform);
        else
            Debug.Log("Miss");
    }

    public override bool Ammo()
    {
        return true;
    }
}
