using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour {

    public List<GridPosition> spawnPoints;
    public bool placingUnits;
    void Awake()
    {
        placingUnits = true;
    }
	// Use this for initialization
	void Start () {
		foreach(Transform spawn in transform)
        {
            spawnPoints.Add(spawn.GetComponent<PlayerSpawnPoint>().position);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(!placingUnits)
        {
            foreach (Transform spawn in transform)
            {
                spawn.gameObject.SetActive(false);
            }
        }
	}
}
