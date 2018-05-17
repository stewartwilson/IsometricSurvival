using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterUnitController : PlayerUnitController
{

    public override Action doAction1()
    {
        return base.doAction1();
    }

    public override Action doAction2()
    {
        return base.doAction2();
    }

    protected override void initiateUnitChacteristics()
    {
        base.initiateUnitChacteristics();
        Shoot shoot = new Shoot();
        Trap trap = new Trap();
        actionSet.actions.Add(new ActionMap(shoot, false));
        actionSet.actions.Add(new ActionMap(trap, false));
        foreach (ActionMap actionMap in actionSet.actions)
        {
            actionMap.action.actor = gameObject;
        }
    }

}
