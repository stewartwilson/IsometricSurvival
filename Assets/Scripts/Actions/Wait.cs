using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Wait : Action
{
    public Facing facing;

    public Wait()
    {
        range = 0;
        canTargetSelf = true;
        canTargetUnit = false;
        actionName = "Wait";
    }

    public override void act()
    {
        base.act();
        actor.GetComponent<UnitController>().facing = facing;
    }
}

