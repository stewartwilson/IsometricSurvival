using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public bool isPlayerUnit;
    public bool isSelected;
    public bool isDefeated;
    public Unit unit;
    public Facing facing;
    public bool canMove;
    protected Animator animator;
    public GridPosition position;
    public List<GridPosition> currentPath;
    public int maxMovement;
    public Vector2 spriteOffset;

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void checkIfDefeated()
    {
        if(unit.health <=0)
        {
            isDefeated = true;
        }
    }
}
