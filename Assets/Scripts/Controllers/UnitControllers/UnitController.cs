using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public bool isPlayerUnit;
    public bool isSelected;
    public bool isDefeated;
    public int health;
    public Facing facing;
    public bool canMove;
    public bool behindEnvironmentObject;
    protected Animator animator;
    public GridPosition position;
    public List<GridPosition> currentPath;
    public int maxMovement;
    public Vector2 spriteOffset;
    public Action action1;
    public Action action2;
    public bool isBeingPlacing;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        isBeingPlacing = false;
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
        bool isUnitBehind = false;
        foreach (Transform child in GameObject.Find("Behind Environment").transform)
        {
            if (position.Equals(child.GetComponent<WalkableArea>().position))
            {
                isUnitBehind = true;
            }
        }
        behindEnvironmentObject = isUnitBehind;
        if(behindEnvironmentObject)
        {
            GetComponent<SpriteRenderer>().color = new Color32(0x00, 0x00, 0x00, 0xAA);
        } else
        {
            GetComponent<SpriteRenderer>().color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        }
        
        doUpdateTasks();
    }

    protected virtual void doUpdateTasks()
    {

    }

    public virtual void doAction1()
    {

    }

    public virtual void doAction2()
    {

    }
}
