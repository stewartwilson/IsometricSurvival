using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishermanUnitController : PlayerUnitController
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
        Hit hit = new Hit();
        Hook hook = new Hook();
        actionSet.actions.Add(new ActionMap(hook, false));
        actionSet.actions.Add(new ActionMap(hit, false));
        foreach (ActionMap actionMap in actionSet.actions)
        {
            actionMap.action.actor = gameObject;
        }
    }
}
