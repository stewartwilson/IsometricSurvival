using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapData : MonoBehaviour
{
    public int damage;
    public GridPosition position;
    public Vector2 spriteOffset;

    void Update()
    {
        transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;

    }
}
