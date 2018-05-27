using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Holds the cursor object used for controlling the game
    public GameObject cursor;
    //Holds which unit is selected by the cursor
    public GameObject selectedObject;
    //Holds the object that displays possible moves for a unit
    public GameObject movesContainer;
    //Holds the object that displays possible targets for a unit's action
    public GameObject targetsContainer;
    //True if attemtping to move a unit
    public bool displayingMoves;
    //True if attemtping to take an action
    public bool displayingTargets;
    //True if attemtping to take an action
    public bool displayingActionTargets;
    //Action that a unit is currently taking
    public Action activeAction;
    //Holds all walkable postions from map data
    public List<GridPosition> walkableArea;
    //Holds the grid positions for a selected units possible moves
    public List<GridPosition> possibleMoves;
    //Holds the grid positions for a selected actions targets
    public List<GridPosition> possibleTargets;
    //Parent object for units the player controls
    public GameObject playerUnitsContainer;
    //Holds all unit game objects that are the players 
    public List<GameObject> playerUnits;
    //Parent object for units the player does not control
    public GameObject enemyUnitsContainer;
    //Holds all unit game objects that are enemies 
    public List<GameObject> enemyUnits;
    //Parent object for objects created by actions
    public GameObject actionObjectsContainer;
    //Holds all unit game objects that are action objects 
    public List<GameObject> actionObjects;
    //The level controller is in charge of map actions
    public LevelController levelController;
    //this list is generated at the start of each round to store turn order
    public List<GameObject> turnOrder;
    //This object holds the unit whose turn it is
    public GameObject activeUnit;
    //Will switch active unit with true
    public bool switchTurn;
    //Used to determine which unit's turn it is
    public int turnCounter;
    //Tracks the current round to be used when generating level data
    public int currentRound;
    //Keeps track of the players current power ups
    public List<PowerUp> powerUps;
    //determines if the level is day or night
    public bool isNight;
    //object incharge of holding save info
    private GameObject persistence;
    //Enemy spawn info for the current level
    public List<EnemySet> enemySets;
    //Prefabs for all possible enemy types
    public List<GameObject> enemyPrefabs;
    //Hold the tavern object
    public GameObject tavern;
    //unit action 1 button
    public Button actionButton1;
    //unit action 2 button
    public Button actionButton2;

    // Use this for initialization
    void Awake()
    {
        if(GameObject.Find("Load Game Controller") == null)
        {
            SceneManager.LoadScene("Persistence", LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        persistence = GameObject.Find("Load Game Controller");
        if (persistence != null && persistence.GetComponent<LoadGameController>().saveData != null
            && persistence.GetComponent<LoadGameController>().saveData.currentRound != 0)
        {
            loadFromController();
            
        }
        else
        {
            currentRound = 1;
            powerUps = new List<PowerUp>();
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main Game"));
        switchTurn = false;
        turnCounter = 0;
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        populateUnits();
        initTurnOrder();
        displayingMoves = false;
        activeUnit = turnOrder[0];
        foreach(WalkableArea wa in levelController.walkableArea)
        {
            walkableArea.Add(wa.position);
        }
        //Enact all power Ups
        foreach (PowerUp powerUp in powerUps)
        {
            UnitType unitType = persistence.GetComponent<LoadGameController>().getPowerUpDataFromEnum(powerUp).unitType;
            switch (unitType)
            {
                case UnitType.Contractor:
                    PowerUpHelper.activatePowerUp(powerUp, playerUnits.Find(unit => UnitType.Contractor.Equals(unit.GetComponent<PlayerUnitController>().unitType)).GetComponent<UnitController>());
                    break;
                case UnitType.Fisherman:
                    PowerUpHelper.activatePowerUp(powerUp, playerUnits.Find(unit => UnitType.Fisherman.Equals(unit.GetComponent<PlayerUnitController>().unitType)).GetComponent<UnitController>());
                    break;
                case UnitType.Hunter:
                    PowerUpHelper.activatePowerUp(powerUp, playerUnits.Find(unit => UnitType.Hunter.Equals(unit.GetComponent<PlayerUnitController>().unitType)).GetComponent<UnitController>());
                    break;
                case UnitType.Nurse:
                    PowerUpHelper.activatePowerUp(powerUp, playerUnits.Find(unit => UnitType.Nurse.Equals(unit.GetComponent<PlayerUnitController>().unitType)).GetComponent<UnitController>());
                    break;
                default:
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        activeUnit = turnOrder[turnCounter];
        if (activeUnit.tag.Equals("Player Unit"))
        {
            actionButton1.GetComponentInChildren<Text>().text = activeUnit.GetComponent<UnitController>().actionSet.actions[2].action.actionName;
            actionButton2.GetComponentInChildren<Text>().text = activeUnit.GetComponent<UnitController>().actionSet.actions[3].action.actionName;
        }
        if (switchTurn)
        {
            cursor.GetComponent<CursorController>().position = activeUnit.GetComponent<UnitController>().position;
            switchTurn = false;
            if (!activeUnit.activeSelf)
            {
                endCurrentTurn();
                switchTurn = true;
            }
            
        }
        if((activeUnit != null) && "Enemy Unit".Equals(activeUnit.tag))
        {
            EnemyUnitController enemy = activeUnit.GetComponent<EnemyUnitController>();
            if (!enemy.isActing)
            {
                enemy.possiblePaths = getPossibleMovement(enemy.position, enemy.maxMovement, enemy.maxJump);
                Debug.Log(enemy.possiblePaths.Count);
                activeUnit.GetComponent<EnemyUnitController>().takeTurn();
            }

        }
        if (selectedObject != null && !displayingActionTargets)
        {
            GridPosition gp = selectedObject.GetComponent<PlayerUnitController>().position;
            if (activeAction is Move)
            {
                possibleTargets.Clear();
                List<List<GridPosition>> possibleMoves = getPossibleMovement(gp, selectedObject.GetComponent<PlayerUnitController>().maxMovement, selectedObject.GetComponent<PlayerUnitController>().maxJump);
                foreach (List<GridPosition> path in possibleMoves) {
                    possibleTargets.Add(path[path.Count - 1]);
                }
                selectedObject.GetComponent<PlayerUnitController>().possiblePaths = possibleMoves;
                InstantiateTargetsDisplay(possibleTargets);
                displayingActionTargets = true;
            }
            else
            {
                possibleTargets = getPossibleActionTargets(gp, activeAction);

                InstantiateTargetsDisplay(possibleTargets);
                displayingActionTargets = true;
            }
        }
        else if (selectedObject == null)
        {
            if (displayingActionTargets)
            {
                destroyTargetsDisplay();
                displayingActionTargets = false;
            }
        }
        if(checkForPlayerLoss())
        {
            playerLoses();
        }
        if(checkForRoundOver())
        {
            endRound();
        }

        foreach(Transform child in actionObjectsContainer.transform)
        {
            actionObjects.Add(child.gameObject);
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

    private void InstantiateTargetsDisplay(List<GridPosition> targets)
    {
        int count = 0;
        foreach (GridPosition pos in targets)
        {
            count++;
            GameObject go = (GameObject)Instantiate(Resources.Load("Possible Move"));
            go.transform.SetParent(targetsContainer.transform);
            go.transform.position = IsometricHelper.gridToGamePostion(pos);
            go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(pos);
            go.name = "Target " + count;
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


    public List<List<GridPosition>> getPossibleMovement(GridPosition currentPos, int maxMovement, int maxJump)
    {

        PathingHelper pathingHelper = new PathingHelper();
        pathingHelper.walkableAreas = walkableArea;
        pathingHelper.enemyUnitPositions = getEnemyUnitPositions();
        pathingHelper.playerUnitPositions = getPlayerUnitPositions();
        pathingHelper.blockingEffectPositions = getActionItemPositions();
        List<List<GridPosition>> paths = pathingHelper.getPossiblePaths2(currentPos, maxMovement, maxJump, activeUnit.GetComponent<UnitController>().isPlayerUnit);

        /*
        List<GridPosition> possibleMoves = new List<GridPosition>();
        foreach (WalkableArea wa in levelController.walkableArea)
        {
            //TODO account for elevation difference
            if (IsometricHelper.distanceBetweenGridPositions(wa.position, currentPos) <= maxMovement &&
                (getObjectAtPosition(wa.position) == null || wa.position.Equals(currentPos)))
            {
                possibleMoves.Add(wa.position);
            }
        }
        */

        return paths;
    }

    public List<GridPosition> getPlayerUnitPositions()
    {
        List<GridPosition> positions = new List<GridPosition>();
        foreach(GameObject unit in playerUnits)
        {
            positions.Add(unit.GetComponent<UnitController>().position);
        }
        return positions;
    }

    public List<GridPosition> getEnemyUnitPositions()
    {
        List<GridPosition> positions = new List<GridPosition>();
        foreach (GameObject unit in enemyUnits)
        {
            positions.Add(unit.GetComponent<UnitController>().position);
        }
        return positions;
    }

    public List<GridPosition> getActionItemPositions()
    {
        List<GridPosition> positions = new List<GridPosition>();
        foreach (GameObject actionObject in actionObjects)
        {
            positions.Add(actionObject.GetComponent<ActionObject>().position);
        }
        return positions;
    }

    public List<GridPosition> getPossibleActionTargets(GridPosition currentPos, Action action)
    {
        List<GridPosition> possibleTargets = new List<GridPosition>();
        foreach (WalkableArea wa in levelController.walkableArea)
        {
            //TODO account for elevation difference
            if (IsometricHelper.distanceBetweenGridPositions(wa.position, currentPos) <= action.range)
            {
                if (getObjectAtPosition(wa.position) == null)
                {
                    possibleTargets.Add(wa.position);
                } else if(wa.position.Equals(currentPos) && action.canTargetSelf)
                {
                    possibleTargets.Add(wa.position);
                } else if(!wa.position.Equals(currentPos) && action.canTargetUnit)
                {
                    possibleTargets.Add(wa.position);
                }
            }
        }

        return possibleTargets;
    }

    public void destroyTargetsDisplay()
    {
        foreach (Transform child in targetsContainer.transform)
        {
            Destroy(child.gameObject);
        }
        displayingTargets = false;
    }

    void populateUnits()
    {
        foreach(Transform child in playerUnitsContainer.transform)
        {
            playerUnits.Add(child.gameObject);
        }
        if (enemySets != null && enemySets[currentRound-1] != null)
        {
            foreach (EnemyMap enemy in enemySets[currentRound-1].enemies)
            {
                GameObject go = instantiateEnemyType(enemy.enemyType, enemy.position);
                go.transform.SetParent(enemyUnitsContainer.transform);
                enemyUnits.Add(go);
            }
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
        SaveContainer data = new SaveContainer();
        data.currentRound = currentRound;
        data.powerUps = powerUps;
        persistence.GetComponent<LoadGameController>().saveData = data;
        saveProgress(data);
        SceneManager.LoadScene("Tavern", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Main Game");
    }

    public bool checkForRoundOver()
    {
        bool roundOver = true;
        foreach(GameObject enemy in enemyUnits)
        {
            if(enemy.GetComponent<UnitController>().health > 0)
            {
                roundOver = false;
            }
        }

        return roundOver;
    }

    public bool checkForPlayerLoss()
    {
        bool playerLoss = true;
        foreach (GameObject unit in playerUnits)
        {
            if (unit.GetComponent<UnitController>().health > 0)
            {
                playerLoss = false;
            }
        }

        return playerLoss;
    }

    private void saveProgress()
    {
        SaveContainer data = new SaveContainer();
        data.currentRound = currentRound;
        data.powerUps = powerUps;
        SaveDataHelper.saveFile(data);
    }

    private void saveProgress(SaveContainer data)
    {
        SaveDataHelper.saveFile(data);
    }

    private void loadProgress()
    {
        SaveContainer data = SaveDataHelper.loadSaveFile();
        currentRound = data.currentRound;
        powerUps = data.powerUps;
        
    }

    private void loadFromController()
    {
        SaveContainer data = GameObject.Find("Load Game Controller").GetComponent<LoadGameController>().saveData;
        currentRound = data.currentRound;
        powerUps = data.powerUps;

    }

    private void playerLoses()
    {
        Debug.Log("Player has no more living units");
    }

    public GameObject instantiateEnemyType(EnemyType enemyType, GridPosition position)
    {
        GameObject enemy = null;

        switch (enemyType)
        {
            case EnemyType.Zombie:

                enemy = (GameObject)Instantiate(Resources.Load("Zombie"));
                enemy.GetComponent<EnemyUnitController>().position = position;
                break;
            case EnemyType.Behemoth:

                enemy = (GameObject)Instantiate(Resources.Load("Behemoth"));
                enemy.GetComponent<EnemyUnitController>().position = position;
                break;
        }

        return enemy;
    }

    public void removeUnactiveUnitsFromTurnOrder()
    {
        List<GameObject> tempList = new List<GameObject>();
        foreach(GameObject unit in turnOrder)
        {
            if(unit.activeSelf)
            {
                tempList.Add(unit);
            }
        }
        turnOrder = tempList;
    }
}
