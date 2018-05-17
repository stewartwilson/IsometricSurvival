using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ActionPrefabHelper : MonoBehaviour
{
    public GameObject actionObjectParent;
    public GameObject trapPrefab;
    public GameObject wallPrefab;

    public void initTrap(Trap trap)
    {
        trapPrefab.GetComponent<TrapData>().position = trap.destination;
        trapPrefab.GetComponent<TrapData>().damage = trap.damage;
        GameObject trapObject = Instantiate(trapPrefab, IsometricHelper.gridToGamePostion(trap.destination), Quaternion.identity);
        trapObject.transform.SetParent(actionObjectParent.transform);


    }

    public void initWall(Build wall)
    {
        trapPrefab.GetComponent<WallData>().position = wall.destination;
        GameObject trapObject = Instantiate(trapPrefab, IsometricHelper.gridToGamePostion(wall.destination), Quaternion.identity);
        trapObject.transform.SetParent(actionObjectParent.transform);


    }
}


