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
        Move move = new Move();
        Shoot shoot = new Shoot();
        Trap trap = new Trap();
        Wait wait = new Wait();
        actionSet.actions.Add(new ActionMap(move, false));
        actionSet.actions.Add(new ActionMap(wait, false));
        actionSet.actions.Add(new ActionMap(shoot, false));
        actionSet.actions.Add(new ActionMap(trap, false));
        foreach (ActionMap actionMap in actionSet.actions)
        {
            actionMap.action.actor = gameObject;
        }
    }

}
