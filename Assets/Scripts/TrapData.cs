using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapData : ActionObject
{
    public int damage;

    void Update()
    {
        transform.position = IsometricHelper.gridToGamePostion(position) + spriteOffset;

    }
}
