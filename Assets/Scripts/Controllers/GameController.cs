using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject selectedUnit;
    public GameObject movesContainer;
    public bool displayingMoves;
    public List<GridPosition> possibleMoves;
    public GameObject playerUnitsContainer;
    public List<GameObject> playerUnits;
    public GameObject enemyUnitsContainer;
    public List<GameObject> enemyUnits;
    public LevelController levelController;

    // Use this for initialization
    void Awake()
    {

    }

    private void Start()
    {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        populateUnits();
        displayingMoves = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (selectedUnit != null && !displayingMoves)
        {
            GridPosition gp = selectedUnit.GetComponent<PlayerUnitController>().position;
            int maxMovement = selectedUnit.GetComponent<PlayerUnitController>().maxMovement;
            possibleMoves = getPossibleMovement(gp, maxMovement);

            InstantiateMovesDisplay(possibleMoves);
            displayingMoves = true;
        }
        else if (selectedUnit == null)
        {
            if (displayingMoves)
            {
                destroyMovesDisplay();
                displayingMoves = false;
            }
        }
    }

    public void moveUnit(GridPosition destination)
    {
        selectedUnit.GetComponent<PlayerUnitController>().position = destination;
    }

    private void InstantiateMovesDisplay(List<GridPosition> moves)
    {
        int count = 0;
        foreach (GridPosition pos in moves)
        {
            count++;
            GameObject go = null;
            GameObject unit = getUnitAtPosition(pos);
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

    private GameObject getUnitAtPosition(GridPosition position)
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

    private void destroyMovesDisplay()
    {
        foreach (Transform child in movesContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public List<GridPosition> getPossibleMovement(GridPosition currentPos, int maxMovement)
    {
        Debug.Log(currentPos + ", " + maxMovement);
        List<GridPosition> possibleMoves = new List<GridPosition>();
        foreach (WalkableArea wa in levelController.walkableArea)
        {
            //TODO account for elevation difference
            if (IsometricHelper.distanceBetweenGridPositions(wa.position, currentPos) <= maxMovement &&
                getUnitAtPosition(wa.position) == null)
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

}
