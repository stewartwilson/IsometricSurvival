using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController {

    protected override void doUpdateTasks()
    {
        base.doUpdateTasks();
        animator.SetInteger("Facing", (int)facing);
        checkIfDefeated();
        if (!isBeingPlacing)
        {
            transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;
        }
    }
}
