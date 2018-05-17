using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Hook : Action
{
    public int damage;

    public Hook()
    {
        range = 3;
        damage = 1;
        canTargetSelf = false;
        canTargetUnit = true;
        actionName = "Hook";
    }

    public override void act()
    {
        base.act();
        //TODO Implement moving target towards actor
        target.GetComponent<UnitController>().takeDamage(damage);
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + target.GetComponent<UnitController>().position;
    }
}

