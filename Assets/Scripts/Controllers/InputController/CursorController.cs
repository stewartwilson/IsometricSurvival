using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    public GridPosition position;

    private void Update()
    {
        transform.position = IsometricHelper.gridToGamePostion(position);
    }
}
