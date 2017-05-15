using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class iUnitState
{
    public UnitController controller;

    public iUnitState(UnitController controller)
    {
        this.controller = controller;
    }

    public abstract void Update();
    public abstract void OnTriggerEnter(Collider other);

    public abstract void ToBattleState();
    public abstract void ToAggroState();
    public abstract void ToFighttate();
}
