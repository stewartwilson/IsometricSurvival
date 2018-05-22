using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallData : MonoBehaviour
{

    public int health;
    public GridPosition position;
    public Vector2 spriteOffset;

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
