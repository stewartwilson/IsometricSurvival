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
                {   if (canMove)
                    {
                        Move move = (Move)doMoveAction();
                        move.destination = currentPath[currentPath.Count - 1];
                        move.act();
                    }
                    hasMoved = true;
                }
                else if (!hasActed)
                {
                    if (canAct)
                    {
                        Hit hit = (Hit)doAction1();
                        hit.target = getActionTarget(hit);
                        if (hit.target != null)
                        {
                            hit.act();
                        }
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
            currentPath = getMovePath();
            pathCounter = 0;
            if(currentPath.Count >1)
            {
                pathCounter = 1;
            }
            Debug.Log(currentPath[pathCounter]);
            destination = currentPath[pathCounter];
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

    private List<GridPosition> getMovePath()
    {
        List<GridPosition> movePath = new List<GridPosition>();
        if (closestTarget != null)
        {
            GridPosition unitPosition = closestTarget.GetComponent<UnitController>().position;
            int closestDistance = 99;
            foreach (List<GridPosition> path in possiblePaths)
            {
                
                int tempDistance = IsometricHelper.distanceBetweenGridPositions(unitPosition, path[path.Count-1]);
                Debug.Log(tempDistance);
                if (tempDistance < closestDistance)
                {
                    movePath = path;
                    closestDistance = tempDistance;
                }
            }
        }
        
        return movePath;

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
            int closestDistance = 99;
            foreach (Transform unit in playerUnits.transform)
            {
                //TODO account for environment targets
                if (unit.gameObject.tag.Equals("Player Unit"))
                {
                    if (unit.gameObject.activeSelf)
                    {
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
