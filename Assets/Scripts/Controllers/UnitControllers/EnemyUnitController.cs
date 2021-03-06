﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : UnitController {

    public List<GridPosition> possibleMoves;
    public EnemyType EnemyType;
    public bool isActing;

    protected override void doUpdateTasks()
    {
        base.doUpdateTasks();
        //animator.SetInteger("Facing", (int)facing);
        checkIfDefeated();
        if (!isBeingPlacing && !isMoving)
        {
            transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;
        } else if(isMoving)
        {
            moveAlongCurrentPath();
        }
    }

    public virtual void takeTurn()
    {
        isActing = true;
        turnsTaken++;
        Debug.Log(gameObject.name + " taking turn number " + turnsTaken);

    }

}
