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
    public bool unitTakingAction;
    public bool inspectingUnit;
    public bool turningUnit;
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
        unitBeingPlaced = gameController.playerUnits[0];
        unitBeingPlaced.SetActive(true);
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        bool enter = Input.GetButtonDown("Submit");
        bool back = Input.GetButtonDown("Back");
        //Start of game unit placing controls
        if (gameController.tavern.GetComponent<Tavern>().placingUnits)
        {
            Tavern tavern = gameController.tavern.GetComponent<Tavern>();
            if (!(Time.time < nextCursorMoveAllowed))
            {
                GridPosition cursorPosition = cursor.GetComponent<CursorController>().position;
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
            if(tavern.spawnPoints.Contains(cursor.GetComponent<CursorController>().position) && !unitBeingPlaced.GetComponent<UnitController>().inGame)
            {
                unitBeingPlaced.GetComponent<UnitController>().position = cursor.GetComponent<CursorController>().position;
                unitBeingPlaced.SetActive(true);
            } else
            {
                if (!unitBeingPlaced.GetComponent<UnitController>().inGame)
                {
                    unitBeingPlaced.SetActive(false);
                }
            }
            if (enter)
            {
                if (tavern.spawnPoints.Contains(cursor.GetComponent<CursorController>().position))
                {
                    unitBeingPlaced.GetComponent<UnitController>().position = cursor.GetComponent<CursorController>().position;
                    unitBeingPlaced.GetComponent<UnitController>().inGame = true;
                }

            }
            if (Input.GetButtonDown("Next Unit"))
            {
                int index = gameController.playerUnits.IndexOf(unitBeingPlaced);
                if (index < gameController.playerUnits.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                if (!unitBeingPlaced.GetComponent<UnitController>().inGame)
                {
                    unitBeingPlaced.SetActive(false);
                }
                unitBeingPlaced = gameController.playerUnits[index];
                unitBeingPlaced.SetActive(true);
            }
            if (Input.GetButtonDown("Previous Unit"))
            {
                int index = gameController.playerUnits.IndexOf(unitBeingPlaced);
                if (index > 0)
                {
                    index--;
                }
                else
                {
                    index = gameController.playerUnits.Count - 1;
                }
                if (!unitBeingPlaced.GetComponent<UnitController>().inGame)
                {
                    unitBeingPlaced.SetActive(false);
                }
                unitBeingPlaced = gameController.playerUnits[index];
                unitBeingPlaced.SetActive(true);
            }
            if (Input.GetButtonDown("Start"))
            {
                gameController.tavern.GetComponent<Tavern>().placingUnits = false;
                gameController.removeUnactiveUnitsFromTurnOrder();
                gameController.activeUnit = gameController.turnOrder[0];
            }
        }
        //Ingame movement controls
        else
        {
            if(gameController.activeUnit.GetComponent<UnitController>().isPlayerUnit)
            {
                cursor.GetComponent<SpriteRenderer>().enabled = true;
            } else
            {
                cursor.GetComponent<SpriteRenderer>().enabled = false;
            }
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
                else
                {
                    //resonsible for moving cursor when nothing is selected
                    if (!(Time.time < nextCursorMoveAllowed) && !(gameController.activeAction is Wait))
                    {
                        GridPosition cursorPosition = cursor.GetComponent<CursorController>().position;
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
                        }
                        else if (back)
                        {
                            cursor.GetComponent<CursorController>().position = gameController.activeUnit.GetComponent<UnitController>().position;
                        }
                    }
                    else if (unitTakingAction && back)
                    {
                        back = false;
                        unitTakingAction = false;
                        gameController.destroyMovesDisplay();
                        gameController.selectedObject = null;
                        cursor.GetComponent<CursorController>().position = selectedObject.GetComponent<UnitController>().position;
                        playerUnitUIPanel.SetActive(true);
                        panelUIActive = true;
                    }
                    else if (unitTakingAction && enter)
                    {
                        processActiveAction();
                        
                    }
                    else if (gameController.activeAction is Wait)
                    {
                        if (vertical > 0)
                        {
                            selectedObject.GetComponent<UnitController>().facing = Facing.Left;
                        }
                        if (vertical < 0)
                        {
                            selectedObject.GetComponent<UnitController>().facing = Facing.Right;
                        }
                        if (horizontal > 0)
                        {
                            selectedObject.GetComponent<UnitController>().facing = Facing.Back;
                        }
                        if (horizontal < 0)
                        {
                            selectedObject.GetComponent<UnitController>().facing = Facing.Forward;
                        }
                        
                        /*if(enter)
                        {
                            gameController.activeAction.act();
                            selectedObject = null;
                            gameController.selectedObject = null;
                            gameController.endCurrentTurn();
                        }*/
                    }
                }
            }
        }
        
    }

    /**
     * Processes Unit actions based on the game controllers active action type
     * 
     */
    private void processActiveAction()
    {
        if (gameController.possibleTargets.Contains(cursor.GetComponent<CursorController>().position))
        {
            unitTakingAction = false;
            if (gameController.activeAction is Move)
            {
                ((Move)gameController.activeAction).destination = cursor.GetComponent<CursorController>().position;
                foreach(List<GridPosition> path in selectedObject.GetComponent<PlayerUnitController>().possiblePaths)
                {
                    if(cursor.GetComponent<CursorController>().position.Equals(path[path.Count-1]))
                    {
                        selectedObject.GetComponent<PlayerUnitController>().currentPath = path;
                    }
                }
                gameController.activeAction.act();

                selectedObject = null;
                gameController.selectedObject = null;
            }
            if (gameController.activeAction is Shoot)
            {
                GameObject unit = gameController.getObjectAtPosition(cursor.GetComponent<CursorController>().position);
                if (unit != null && unit.GetComponent<UnitController>() != null)
                {
                    ((Shoot)gameController.activeAction).target = unit;
                    gameController.activeAction.act();
                    selectedObject = null;
                    gameController.selectedObject = null;
                }
            }
            if (gameController.activeAction is Trap)
            {
                ((Trap)gameController.activeAction).destination = cursor.GetComponent<CursorController>().position;
                gameController.activeAction.act();
                selectedObject = null;
                gameController.selectedObject = null;
            }
            if (gameController.activeAction is Build)
            {
                ((Build)gameController.activeAction).destination = cursor.GetComponent<CursorController>().position;
                gameController.activeAction.act();
                selectedObject = null;
                gameController.selectedObject = null;
            }
            if (gameController.activeAction is Hook)
            {
                GameObject unit = gameController.getObjectAtPosition(cursor.GetComponent<CursorController>().position);
                if (unit != null && unit.GetComponent<UnitController>() != null)
                {
                    ((Hook)gameController.activeAction).target = unit;
                    gameController.activeAction.act();
                    selectedObject = null;
                    gameController.selectedObject = null;
                }
            }
            if (gameController.activeAction is Hit)
            {
                GameObject unit = gameController.getObjectAtPosition(cursor.GetComponent<CursorController>().position);
                if (unit != null && unit.GetComponent<UnitController>() != null)
                {
                    ((Hit)gameController.activeAction).target = unit;
                    gameController.activeAction.act();
                    selectedObject = null;
                    gameController.selectedObject = null;
                }
            }
            if (gameController.activeAction is Heal)
            {
                GameObject unit = gameController.getObjectAtPosition(cursor.GetComponent<CursorController>().position);
                if (unit != null && unit.GetComponent<UnitController>() != null)
                {
                    ((Heal)gameController.activeAction).target = unit;
                    gameController.activeAction.act();
                    selectedObject = null;
                    gameController.selectedObject = null;
                }
            }
            if (gameController.activeAction is Wait)
            {
                ((Wait)gameController.activeAction).facing = selectedObject.GetComponent<UnitController>().facing;
                gameController.activeAction.act();
                selectedObject = null;
                gameController.selectedObject = null;
                gameController.endCurrentTurn();
            }


            //TODO add in the other Action Types
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
            unitTakingAction = true;
            gameController.activeAction = selectedObject.GetComponent<UnitController>().doMoveAction();
            gameController.selectedObject = selectedObject;
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
        }
        
    }

    /**
     * Do acton 1 is called by UI button click
     * This will do the 1st action of the selected unit
     */
    public void doAction1()
    {
        if ("Player Unit".Equals(selectedObject.tag))
        {
            unitTakingAction = true;
            gameController.activeAction = selectedObject.GetComponent<UnitController>().doAction1();
            gameController.selectedObject = selectedObject;
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
        }

    }

    /**
     * Do acton 2 is called by UI button click
     * This will do the 2st action of the selected unit
     */
    public void doAction2()
    {
        if ("Player Unit".Equals(selectedObject.tag))
        {
            unitTakingAction = true;
            gameController.activeAction = selectedObject.GetComponent<UnitController>().doAction2();
            gameController.selectedObject = selectedObject;
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
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
            unitTakingAction = true;
            gameController.selectedObject = selectedObject;
            playerUnitUIPanel.SetActive(false);
            panelUIActive = false;
            gameController.activeAction = selectedObject.GetComponent<UnitController>().doWaitAction();
           
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
        if (selectedObject.Equals(gameController.activeUnit))
        {
            playerUnitUIPanel.SetActive(true);
            panelUIActive = true;
            unitsOriginalFacing = selectedObject.GetComponent<UnitController>().facing;
            unitsOriginalPosition = selectedObject.GetComponent<UnitController>().position;
        } else
        {
            enemyUnitUIPanel.SetActive(true);
            panelUIActive = true;
        }
    }

    


}
