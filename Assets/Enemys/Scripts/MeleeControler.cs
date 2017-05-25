using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeControler : EnemyController {


    public override void AttackTarget()
    {
        Debug.Log("Melee");
    }

    public void MeleeHit()
    {
        if (Random.Range(0, 100) < 90)
            target.GetComponent<UnitHealth>().Hit(attackDmg, transform);
        else
            Debug.Log("Miss");
    }
}
