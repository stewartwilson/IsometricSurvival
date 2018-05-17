using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieUnitController : EnemyUnitController
{

    public GameObject closestTarget;
    public GridPosition destination;
    public float actionDelay;
    private float nextActionAllowed;


    protected override void doUpdateTasks()
    {
        base.doUpdateTasks();
        if (closestTarget != null)
        {
            if (!(Time.time < nextActionAllowed))
            {

                if (!hasMoved)
                {
                    Move move = (Move)doMoveAction();
                    move.destination = destination;
                    move.act();
                    hasMoved = true;
                }
                else if (!hasActed)
                {
                    Hit hit = (Hit)doAction1();
                    hit.target = getActionTarget(hit);
                    if (hit.target != null)
                    {
                        hit.act();
                    }
                    hasActed = true;
                }
                else if (hasActed && hasMoved)
                {
                    Wait wait = (Wait)doWaitAction();
                    //TODO face towards target
                    wait.facing = Facing.Left;
                    wait.act();
                    isActing = false;
                    closestTarget = null;
                    destination = new GridPosition();
                    GameController gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
                    hasActed = false;
                    hasMoved = false;
                    gameController.selectedObject = null;
                    gameController.endCurrentTurn();
                }
                nextActionAllowed = Time.time + actionDelay;
            }
        }

    }

    public override void takeTurn()
    {
        base.takeTurn();
        closestTarget = getClosestTarget();
        if (closestTarget != null)
        {
            destination = getMoveTarget();
        }
    }

    protected override void initiateUnitChacteristics()
    {
        base.initiateUnitChacteristics();
        Hit hit = new Hit();
        actionSet.actions.Add(new ActionMap(hit, false));
        foreach (ActionMap actionMap in actionSet.actions)
        {
            actionMap.action.actor = gameObject;
        }
    }

    private GridPosition getMoveTarget()
    {
        GridPosition targetPos = new GridPosition();
        if (closestTarget != null)
        {
            GridPosition unitPosition = closestTarget.GetComponent<UnitController>().position;
            int closestDistance = 99;
            foreach (GridPosition pos in possibleMoves)
            {

                int tempDistance = IsometricHelper.distanceBetweenGridPositions(unitPosition, pos);
                if (tempDistance < closestDistance)
                {
                    targetPos = pos;
                    closestDistance = tempDistance;
                }
            }
        }
        return targetPos;

    }

    private GameObject getActionTarget(Action action)
    {
        GameObject target = null;
        if (closestTarget != null)
        {
            GridPosition unitPosition = closestTarget.GetComponent<UnitController>().position;
            int distance = IsometricHelper.distanceBetweenGridPositions(unitPosition, position);
            if (distance <= action.range)
            {
                target = closestTarget;
            }
        }
        return target;

    }

    private GameObject getClosestTarget()
    {
        GameObject playerUnits = GameObject.Find("Player Units");
        GameObject closest = null;
        if (playerUnits != null)
        {
            Debug.Log("Found playerUnits");
            int closestDistance = 99;
            foreach (Transform unit in playerUnits.transform)
            {
                Debug.Log("Looping through children");
                //TODO account for environment targets
                if (unit.gameObject.tag.Equals("Player Unit"))
                {
                    if (unit.gameObject.activeSelf)
                    {
                        Debug.Log(unit.gameObject.name);
                        if (closestTarget == null)
                        {
                            closest = unit.gameObject;
                            closestDistance = IsometricHelper.distanceBetweenGridPositions(position, unit.gameObject.GetComponent<UnitController>().position);
                        }
                        else
                        {
                            int tempDistance = IsometricHelper.distanceBetweenGridPositions(position, unit.gameObject.GetComponent<UnitController>().position);
                            if (tempDistance < closestDistance)
                            {
                                closest = unit.gameObject;
                            }
                        }
                    }
                }
            }
        }
        return closest;
    }
}
