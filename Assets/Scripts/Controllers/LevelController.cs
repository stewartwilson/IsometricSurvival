using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public List<WalkableArea> walkableArea;
    public GameObject walkablePositions;
    public int maxX;
    public int maxY;

    // Use this for initialization
    void Awake() {
        walkableArea = new List<WalkableArea>();
        populateWalkableArea();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        
    }

    public void populateWalkableArea()
    {
        foreach(Transform child in walkablePositions.transform)
        {
            walkableArea.Add(child.gameObject.GetComponent<WalkableArea>());
        }
    }

    public void populateMap()
    {
        for(int i = 0; i < maxX; i++)
        {
            for (int j = 0; j < maxY; j++)
            {
                GridPosition pos = new GridPosition(i, j, 0);
                GameObject go = (GameObject)Instantiate(Resources.Load("Walkable Position"));
                go.name = "x" + i + "y" + j;
                go.GetComponent<SpriteRenderer>().enabled = false;
                go.transform.position = IsometricHelper.gridToGamePostion(pos);
                go.GetComponent<WalkableArea>().position = pos;
                walkableArea.Add(go.GetComponent<WalkableArea>());
                go.transform.SetParent(walkablePositions.transform);
            }
        }
    }

}
