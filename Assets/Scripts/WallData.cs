using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallData : ActionObject
{

    public int health;

    void Update()
    {
        transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
