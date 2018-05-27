using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController {
    public UnitType unitType;
    protected override void doUpdateTasks()
    {
        base.doUpdateTasks();
        animator.SetInteger("Facing", (int)facing);
        checkIfDefeated();
        if (!isBeingPlacing && !isMoving)
        {
            transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;
        } else if(isMoving)
        {
            moveAlongCurrentPath();
        }
    }
}
