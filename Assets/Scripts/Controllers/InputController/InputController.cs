using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public GameObject playerUnitUIPanel;
    public GameObject enemyUnitUIPanel;
    public bool panelUIActive;
    public float cursorDelay;
    public bool canCursorMove;
    public float nextCursorMoveAllowed;
    //Holds cursor object
    public GameObject cursor;
    public GridPosition cursorPosition;
    public int maxCursorXPos;
    public int maxCursorYPos;
    public bool touching;
    //Hold an object that the cursor has selected
    public GameObject selectedObject;
    public bool movingUnit;
    public bool inspectingUnit;
    public bool placingUnit;
    //holds unit being move to determine location and directino facing
    public GameObject unitBeingPlaced;
    //holds units original location before an attempted move
    private Vector2 unitsOriginalLocation;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        maxCursorXPos = gameController.levelController.maxX;
        maxCursorYPos = gameController.levelController.maxY;
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool enter = Input.GetButtonDown("Submit");
        bool back = Input.GetButtonDown("Back");
        if(panelUIActive)
        {
            if(back)
            {
                playerUnitUIPanel.SetActive(false);
                enemyUnitUIPanel.SetActive(false);
                panelUIActive = false;
            }
        }
        else if (placingUnit)
        {
            if (vertical > 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Left;
            }
            if (vertical < 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Right;
            }
            if (horizontal > 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Back;
            }
            if (horizontal < 0)
            {
                unitBeingPlaced.GetComponent<UnitController>().facing = Facing.Forward;
            }
            if(back)
            {
                unitBeingPlaced.transform.position = unitsOriginalLocation;
                placingUnit = false;
                movingUnit = false;
            }
            else if(enter)
            {
                placingUnit = false;
                movingUnit = false;
            }
        }
        else
        {
            //resonsible for moving cursor when nothing is selected
            if (!(Time.time < nextCursorMoveAllowed))
            {
                GridPosition startPostion = new GridPosition(cursorPosition.x, cursorPosition.y, cursorPosition.elevation);
                if (vertical > 0)
                {
                    cursorPosition.x++;
                }
                if (vertical < 0)
                {
                    cursorPosition.x--;
                }
                if (horizontal > 0)
                {
                    cursorPosition.y--;
                }
                if (horizontal < 0)
                {
                    cursorPosition.y++;
                }

                if (cursorPosition.y < 0)
                {
                    cursorPosition.y = 0;
                }
                if (cursorPosition.x < 0)
                {
                    cursorPosition.x = 0;
                }
                if (cursorPosition.y > maxCursorYPos)
                {
                    cursorPosition.y = maxCursorYPos;
                }
                if (cursorPosition.x > maxCursorXPos)
                {
                    cursorPosition.x = maxCursorXPos;
                }
                if (movingUnit) {
                    
                    if(gameController.possibleMoves.Contains(cursorPosition)) {
                        updateCursor();
                    }
                    else
                    {
                        cursorPosition = startPostion;
                    }
                } else {
                    updateCursor();
                }
                if(movingUnit && back)
                {
                    movingUnit = false;
                    selectedObject = null;
                    gameController.selectedUnit = null;
                }

                nextCursorMoveAllowed = Time.time + cursorDelay;
            }
            GameObject tempSelect = cursor.GetComponent<CursorController>().objectInTile;
            if (tempSelect != null && !movingUnit)
            {
                if (enter)
                {
                    selectObject(cursor.GetComponent<CursorController>().objectInTile);
                    unitsOriginalLocation = selectedObject.transform.position;

                }
            }
            else
            {
                if (movingUnit && enter)
                {
                    gameController.moveUnit(cursorPosition);
                    movingUnit = false;
                    placingUnit = true;
                    unitBeingPlaced = selectedObject;
                    selectedObject = null;
                    gameController.selectedUnit = null;
                }
                if (enter && !movingUnit)
                {
                    selectedObject = null;
                    gameController.selectedUnit = null;
                    
                }
            }
        }
        
    }

    /**
     * 
     * 
     */
    public void updateCursor()
    {
        cursor.transform.position = IsometricHelper.gridToGamePostion(cursorPosition);
    }

    /**
     * Move unit is called by UI button click
     * 
     */
    public void moveUnit()
    {
        if ("Player Unit".Equals(selectedObject.tag))
        {
            movingUnit = true;
            gameController.selectedUnit = selectedObject;
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
        }
        
    }

    /**
     * 
     * 
     */
    public void inspectUnit()
    {
        if ("Player Unit".Equals(selectedObject.tag) || "Enemy Unit".Equals(selectedObject.tag))
        {
            inspectingUnit = true;
            Debug.Log("Inspecting Unit");
        }

    }

    /**
     * 
     * 
     */
    private void selectObject(GameObject selected)
    {
        selectedObject = selected;
        switch (selectedObject.tag)
        {
            case "Player Unit":
                playerUnitUIPanel.SetActive(true);
                panelUIActive = true;
                
                Debug.Log("selected Player Unit");
                break;
            case "Enemy Unit":
                enemyUnitUIPanel.SetActive(true);
                panelUIActive = true;
                Debug.Log("selected Enemy Unit");
                break;
            default:
                Debug.Log("Nothing to select");
                break;
        }
    }

    


}
