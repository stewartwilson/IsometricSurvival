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
        moveTargetTowardsActor();
        target.GetComponent<UnitController>().takeDamage(damage);

    }

    public override string ToString()
    {
        return actionName + ", actor:" + actor.name + ", postion:" + actor.GetComponent<UnitController>().position + ", target:" + target.GetComponent<UnitController>().position;
    }

    private void moveTargetTowardsActor()
    {
        GridPosition targetPositon = target.GetComponent<UnitController>().position;
        GridPosition actorPosition = actor.GetComponent<UnitController>().position;
        GridPosition targetNewPostion = targetPositon;
        if (targetPositon.x > actorPosition.x)
        {
            targetNewPostion = new GridPosition(actorPosition.x+1, actorPosition.y);
        } else if (targetPositon.x < actorPosition.x)
        {
            targetNewPostion = new GridPosition(actorPosition.x - 1, actorPosition.y);
        }
        else if (targetPositon.y < actorPosition.y)
        {
            targetNewPostion = new GridPosition(actorPosition.x, actorPosition.y - 1);
        } else if (targetPositon.y > actorPosition.y)
        {
            targetNewPostion = new GridPosition(actorPosition.x, actorPosition.y + 1);
        }
        target.GetComponent<UnitController>().position = targetNewPostion;
    }
}

