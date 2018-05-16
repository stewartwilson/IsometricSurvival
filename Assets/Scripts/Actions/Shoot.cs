using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Shoot : Action
{
    public int damage;

    public Shoot()
    {
        range = 3;
        damage = 1;
        canTargetSelf = false;
        canTargetUnit = true;
        actionName = "Shoot";
    }

    public override void act()
    {
        base.act();
        target.GetComponent<UnitController>().takeDamage(damage);
    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + target.GetComponent<UnitController>().position;
    }
}

