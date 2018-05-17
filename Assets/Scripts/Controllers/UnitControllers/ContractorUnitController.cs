using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractorUnitController : PlayerUnitController
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
        Build build = new Build();
        actionSet.actions.Add(new ActionMap(hit, false));
        actionSet.actions.Add(new ActionMap(build, false));
        foreach (ActionMap actionMap in actionSet.actions)
        {
            actionMap.action.actor = gameObject;
        }
    }

}
