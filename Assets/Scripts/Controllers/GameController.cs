using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject cursor;
    public GameObject selectedObject;
    public GameObject movesContainer;
    public bool displayingMoves;
    public List<GridPosition> possibleMoves;
    public GameObject playerUnitsContainer;
    public List<GameObject> playerUnits;
    public GameObject enemyUnitsContainer;
    public List<GameObject> enemyUnits;
    public LevelController levelController;
    public List<GameObject> turnOrder;
    public GameObject activeUnit;
    public bool switchTurn;
    public int turnCounter;
    public int currentRound;

    // Use this for initialization
    void Awake()
    {

    }

    private void Start()
    {
        switchTurn = false;
        turnCounter = 0;
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        populateUnits();
        initTurnOrder();
        displayingMoves = false;
        activeUnit = turnOrder[0];

    }

    // Update is called once per frame
    void Update()
    {
        if(switchTurn)
        {
            activeUnit = turnOrder[turnCounter];
            cursor.GetComponent<CursorController>().position = activeUnit.GetComponent<UnitController>().position;
            switchTurn = false;
        }
        if("Enemy Unit".Equals(activeUnit.tag))
        {
            activeUnit.GetComponent<EnemyUnitController>().takeTurn();
        }
        if (selectedObject != null && !displayingMoves)
        {
            GridPosition gp = selectedObject.GetComponent<PlayerUnitController>().position;
            int maxMovement = selectedObject.GetComponent<PlayerUnitController>().maxMovement;
            possibleMoves = getPossibleMovement(gp, maxMovement);

            InstantiateMovesDisplay(possibleMoves);
            displayingMoves = true;
        }
        else if (selectedObject == null)
        {
            if (displayingMoves)
            {
                destroyMovesDisplay();
            }
        }
    }

    public void moveUnit(GridPosition destination)
    {
        selectedObject.GetComponent<PlayerUnitController>().position = destination;
    }

    private void InstantiateMovesDisplay(List<GridPosition> moves)
    {
        int count = 0;
        foreach (GridPosition pos in moves)
        {
            count++;
            GameObject go = null;
            GameObject unit = getObjectAtPosition(pos);
            if (unit == null)
            {
                go = (GameObject)Instantiate(Resources.Load("Possible Move"));
            }
            go.transform.SetParent(movesContainer.transform);
            go.transform.position = IsometricHelper.gridToGamePostion(pos);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(pos);
            go.name = "Move " + count;
        }
    }

    public GameObject getObjectAtPosition(GridPosition position)
    {
        foreach (GameObject unit in playerUnits)
        {
            UnitController unitController = unit.GetComponent<UnitController>();
            if (position.Equals(unitController.position))
            {
                return unit;
            }
        }
        foreach (GameObject unit in enemyUnits)
        {
            UnitController unitController = unit.GetComponent<UnitController>();
            if (position.Equals(unitController.position))
            {
                return unit;
            }
        }
        return null;
    }

    public void destroyMovesDisplay()
    {
        foreach (Transform child in movesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        displayingMoves = false;
    }

    public List<GridPosition> getPossibleMovement(GridPosition currentPos, int maxMovement)
    {
        Debug.Log(currentPos + ", " + maxMovement);
        List<GridPosition> possibleMoves = new List<GridPosition>();
        foreach (WalkableArea wa in levelController.walkableArea)
        {
            //TODO account for elevation difference
            if (IsometricHelper.distanceBetweenGridPositions(wa.position, currentPos) <= maxMovement &&
                getObjectAtPosition(wa.position) == null)
            {
                possibleMoves.Add(wa.position);
            }
        }

        return possibleMoves;
    }

    void populateUnits()
    {
        foreach(Transform child in playerUnitsContainer.transform)
        {
            playerUnits.Add(child.gameObject);
        }

        foreach (Transform child in enemyUnitsContainer.transform)
        {
            enemyUnits.Add(child.gameObject);
        }
    }


    public void initRound()
    {

    }

    public void initTurnOrder()
    {
        List<GameObject> tempList = new List<GameObject>();
        tempList.AddRange(playerUnits);
        tempList.AddRange(enemyUnits);
        turnOrder.AddRange(tempList.OrderBy(p => p.GetComponent<UnitController>().speed));
        turnOrder.Reverse();
    }

    public void endCurrentTurn()
    {
        turnCounter++;
        if(turnCounter >= turnOrder.Count)
        {
            turnCounter = 0;
        }
        switchTurn = true;
    }

    public void endRound()
    {
        currentRound++;
    }

}
