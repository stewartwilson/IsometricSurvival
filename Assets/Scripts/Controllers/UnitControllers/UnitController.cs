using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public bool isPlayerUnit;
    public bool isSelected;
    public bool isDefeated;
    public int maxHealth;
    public int health;
    public int speed;
    public Facing facing;
    public bool canMove;
    public bool behindEnvironmentObject;
    protected Animator animator;
    public GridPosition position;
    public List<List<GridPosition>> possiblePaths;
    public List<GridPosition> currentPath;
    public int pathCounter;
    public int maxMovement;
    public int maxJump;
    public Vector2 spriteOffset;
    public ActionSet actionSet;
    public bool isBeingPlacing;
    public bool inGame;
    public float spriteMoveSpeed;

    public bool isMoving;
    protected bool hasMoved;
    protected bool hasActed;
    protected int turnsTaken;


    protected void Awake()
    {
        inGame = false;
    }

    protected void Start()
    {
        animator = GetComponent<Animator>();
        isBeingPlacing = false;
        initiateUnitChacteristics();
        health = maxHealth;
        hasMoved = false;
        hasActed = false;
        turnsTaken = 0;
    }

    protected virtual void initiateUnitChacteristics()
    {
        Move move = new Move();
        move.range = maxMovement;
        Wait wait = new Wait();
        actionSet.actions.Add(new ActionMap(move, false));
        actionSet.actions.Add(new ActionMap(wait, false));
    }

    protected void checkIfDefeated()
    {
        if(health <=0)
        {
            isDefeated = true;
        }
    }

    public void Update()
    {
        doUpdateTasks();
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void takeHealing(int healing)
    {
        health += healing;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    protected virtual void doUpdateTasks()
    {
        GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(position);
    }

    public virtual Action doMoveAction()
    {
        actionSet.actions[0].hasBeenDone = true;

        return actionSet.actions[0].action;
    }

    public virtual Action doWaitAction()
    {
        actionSet.actions[1].hasBeenDone = true;
        return actionSet.actions[1].action;
    }

    public virtual Action doAction1()
    {
        actionSet.actions[2].hasBeenDone = true;
        return actionSet.actions[2].action;
    }

    public virtual Action doAction2()
    {
        actionSet.actions[3].hasBeenDone = true;
        return actionSet.actions[3].action;
    }

    public void moveAlongCurrentPath()
    {
        Debug.Log(currentPath[pathCounter]);
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), IsometricHelper.gridToGamePostion(currentPath[pathCounter]) + spriteOffset, spriteMoveSpeed * Time.deltaTime);
        Vector2 goalPosition = IsometricHelper.gridToGamePostion(currentPath[pathCounter])+ spriteOffset;
        if (Math.Abs(transform.position.x - goalPosition.x) < .1 && Math.Abs(transform.position.y - goalPosition.y) < .1)
        {
            if (pathCounter < currentPath.Count-1)
            {
                pathCounter++;
            } else
            {
                isMoving = false;
                position = currentPath[pathCounter];
                pathCounter = 0;
                currentPath = new List<GridPosition>();
            }
        }
    }

}
