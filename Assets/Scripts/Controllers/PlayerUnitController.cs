using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController {

    protected void Update()
    {
        animator.SetInteger("Facing", (int)facing);
        checkIfDefeated();
        transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;
    }


}
