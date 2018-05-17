using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Build : Action
{
    public GridPosition destination;

    public Build()
    {
        range = 1;
        canTargetSelf = false;
        canTargetUnit = false;
        actionName = "Build";
    }

    public override void act()
    {
        base.act();
        GameObject.Find("ActionPrefabHelper").GetComponent<ActionPrefabHelper>().initWall(this);
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + destination;
    }
}