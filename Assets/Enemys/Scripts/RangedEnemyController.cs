using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController {


    public override void AttackTarget()
    {
        anim.SetTrigger("RangedAttack");
        Debug.Log("Ranged");
    }
    
    public void RangedHit()
    {
        if (Random.Range(0, 100) < 90)
            target.GetComponent<UnitHealth>().Hit(attackDmg, transform);
        else
            Debug.Log("Miss");
    }
}
