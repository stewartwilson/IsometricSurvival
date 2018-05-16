using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Trap : Action
{
    public GridPosition destination;

    public Trap()
    {
        range = 1;
        canTargetSelf = false;
        canTargetUnit = false;
        actionName = "Trap";
    }

    public override void act()
    {
        base.act();
        //create trap prefab at destination;
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + destination;
    }
}

