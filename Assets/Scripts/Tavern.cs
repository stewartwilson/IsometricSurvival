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
            if (spawn.gameObject.tag.Equals("Spawn Point"))
            {
                spawnPoints.Add(spawn.GetComponent<PlayerSpawnPoint>().position);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(!placingUnits)
        {
            foreach (Transform spawn in transform)
            {
                if (spawn.gameObject.tag.Equals("Spawn Point"))
                {
                    spawn.gameObject.SetActive(false);
                }
            }
        }
	}
}
