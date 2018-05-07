using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    public GridPosition position;
    public Sprite sprite;
    public GroundType groundType;
    public TileSave tileSave;

    private void Awake()
    {
        if(sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

}
