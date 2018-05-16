using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Heal : Action
{
    public int healing;

    public Heal()
    {
        range = 1;
        healing = 1;
        canTargetSelf = true;
        canTargetUnit = true;
        actionName = "Heal";
    }

    public override void act()
    {
        base.act();
        target.GetComponent<UnitController>().takeHealing(healing);
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + target.GetComponent<UnitController>().position;
    }
}

