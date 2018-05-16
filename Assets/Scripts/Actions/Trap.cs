using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Trap : Action
{
    public GridPosition destination;
    public int damage;

    public Trap()
    {
        range = 1;
        damage = 1;
        canTargetSelf = false;
        canTargetUnit = false;
        actionName = "Trap";
    }

    public override void act()
    {
        base.act();
        GameObject.Find("ActionPrefabHelper").GetComponent<ActionPrefabHelper>().initTrap(this);
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + destination;
    }
}

