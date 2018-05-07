using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableArea : MonoBehaviour {

    public GridPosition position;

    public WalkableArea(GridPosition position)
    {
        this.position = position;
    }

}
