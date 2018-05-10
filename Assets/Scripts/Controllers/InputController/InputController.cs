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
    private GridPosition unitsOriginalPosition;
    private Facing unitsOriginalFacing;
    private GameController gameController;
    private List<GridPosition> possibleMoves;

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
        if (inspectingUnit)
        {
            if (back)
            {
                inspectingUnit = false;
                playerUnitUIPanel.SetActive(false);
                enemyUnitUIPanel.SetActive(false);
                panelUIActive = false;
                selectedObject = null;
            }
        }
        else
        {
            if (panelUIActive)
            {
                if (back)
                {
                    playerUnitUIPanel.SetActive(false);
                    enemyUnitUIPanel.SetActive(false);
                    panelUIActive = false;
                    selectedObject = null;
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
                if (back)
                {
                    unitBeingPlaced.GetComponent<UnitController>().isBeingPlacing = false;
                    unitBeingPlaced.transform.position = IsometricHelper.gridToGamePostion(unitsOriginalPosition) +
                            unitBeingPlaced.GetComponent<UnitController>().spriteOffset;
                    unitBeingPlaced.GetComponent<UnitController>().facing = unitsOriginalFacing;
                    unitBeingPlaced = null;
                    placingUnit = false;
                    movingUnit = true;
                    back = false;

                }
                else if (enter)
                {
                    placingUnit = false;
                    movingUnit = false;
                    gameController.moveUnit(cursor.GetComponent<CursorController>().position);
                    unitBeingPlaced.GetComponent<UnitController>().isBeingPlacing = false;
                    selectedObject = null;
                    unitBeingPlaced = null;
                    gameController.selectedObject = null;
                }
            }
            else
            {
                //resonsible for moving cursor when nothing is selected
                if (!(Time.time < nextCursorMoveAllowed))
                {
                    GridPosition cursorPosition = cursor.GetComponent<CursorController>().position;
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
                    cursor.GetComponent<CursorController>().position = cursorPosition;

                    nextCursorMoveAllowed = Time.time + cursorDelay;
                    
                }
                
                if (selectedObject == null)
                {
                    if (enter)
                    {
                        Debug.Log("enter");
                        GameObject obj = gameController.getObjectAtPosition(cursor.GetComponent<CursorController>().position);
                        if (obj != null)
                        {
                            selectObject(obj);

                        }
                    } else if (back)
                    {
                        cursor.GetComponent<CursorController>().position = unitsOriginalPosition;
                    }
                }
                else if (movingUnit && back)
                {
                    back = false;
                    movingUnit = false;
                    gameController.destroyMovesDisplay();
                    gameController.selectedObject = null;
                    cursor.GetComponent<CursorController>().position = selectedObject.GetComponent<UnitController>().position;
                    playerUnitUIPanel.SetActive(true);
                    panelUIActive = true;
                }
                else if (movingUnit && enter)
                {
                    if (gameController.possibleMoves.Contains(cursor.GetComponent<CursorController>().position))
                    {
                        movingUnit = false;
                        placingUnit = true;
                        unitBeingPlaced = selectedObject;
                        unitBeingPlaced.transform.position = IsometricHelper.gridToGamePostion(cursor.GetComponent<CursorController>().position) +
                            unitBeingPlaced.GetComponent<UnitController>().spriteOffset;
                        unitBeingPlaced.GetComponent<UnitController>().isBeingPlacing = true;
                    }
                }
            }
        }
        
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
            gameController.selectedObject = selectedObject;
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
    public void waitUnit()
    {
        if ("Player Unit".Equals(selectedObject.tag))
        {
            Debug.Log("End Turn");
            gameController.endCurrentTurn();
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
            selectedObject = null;
            unitBeingPlaced = null;
        }

    }

    /**
     * 
     * 
     */
    private void selectObject(GameObject selected)
    {
        selectedObject = selected;
        if (selectedObject.Equals(gameController.activeUnit))
        {
            playerUnitUIPanel.SetActive(true);
            panelUIActive = true;
            unitsOriginalFacing = selectedObject.GetComponent<UnitController>().facing;
            unitsOriginalPosition = selectedObject.GetComponent<UnitController>().position;
            Debug.Log("selected Active Unit");
        } else
        {
            enemyUnitUIPanel.SetActive(true);
            panelUIActive = true;
            Debug.Log("selected non active Unit");
        }
        /*switch (selectedObject.tag)
        {
            case "Player Unit":
                playerUnitUIPanel.SetActive(true);
                panelUIActive = true;
                unitsOriginalFacing = selectedObject.GetComponent<UnitController>().facing;
                unitsOriginalPosition =  selectedObject.GetComponent<UnitController>().position;
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
        } */
    }

    


}
