using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Move : Action
{
    public GridPosition destination;

    public Move()
    {
        range = 3;
        actionName = "Move";
    }

    public override void act()
    {
        base.act();
        actor.GetComponent<UnitController>().position = destination;
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + destination;
    }
}


