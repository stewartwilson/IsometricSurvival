using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseUnitController : PlayerUnitController
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
        Heal heal = new Heal();
        Hit hit = new Hit();
        Wait wait = new Wait();
        actionSet.actions.Add(new ActionMap(move, false));
        actionSet.actions.Add(new ActionMap(wait, false));
        actionSet.actions.Add(new ActionMap(heal, false));
        actionSet.actions.Add(new ActionMap(heal, false));
        foreach (ActionMap actionMap in actionSet.actions)
        {
            actionMap.action.actor = gameObject;
        }
    }
}
