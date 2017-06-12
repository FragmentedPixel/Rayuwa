using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeUnitController: UnitController
{
    public float aoeDistance;

    #region Melee Attacking
    public override void FightTarget()
    {
        anim.SetTrigger("AoeAttack");
    }
    public override string GetAmmoText()
    {
        string ammoText = Mathf.RoundToInt( ((ammo * 100f) / maxAmmo ) ).ToString() + "%";
        return ammoText;
    }
    public void AoeHit()
    {
        if (target != null)
        {
            target.GetComponent<EnemyHealth>().Hit(fightDmg, transform);

            foreach(EnemyHealth enemy in nearbyEnemies)
            {
                if (enemy == null)
                    continue;

                Vector3 playerPos = new Vector3(transform.position.x, 0f, transform.position.z);
                Vector3 enemyPos = new Vector3(enemy.transform.position.x, 0f, enemy.transform.position.z);

                if(Vector3.Distance(playerPos, enemyPos) < aoeDistance)
                    enemy.Hit(fightDmg, transform);
            }
        }

        audioS.PlayOneShot(fightSound);
    }
    #endregion
}
