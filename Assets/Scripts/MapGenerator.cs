using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public bool generateMap;
    public string filename;
    public List<TileData> map;


	// Use this for initialization
	void Start () {
        if (generateMap)
        {
            MapData mapData = SaveDataHelper.loadMapData(filename);
            generateTilesFromMapData(mapData);
        }
    }

    public void generateTilesFromMapData(MapData data)
    {
        int maxX = 0;
        int maxY = 0;
        foreach (TileSave ts in data.tiles)
        {
            if (maxX < ts.position.x)
            {
                maxX = ts.position.x;
            }
            if (maxY < ts.position.y)
            {
                maxY = ts.position.y;
            }
            createTileFromTileSave(ts);
        }
        Debug.Log("Max values:" + maxX + "," + maxY);
    }

    public void createTileFromTileSave(TileSave ts)
    {
        Debug.Log(ts.spriteName);
        GridPosition pos = new GridPosition(ts.position.x, ts.position.y, ts.position.elevation);
        Sprite sp = Resources.Load<Sprite>(ts.spriteName);
        GameObject go = (GameObject)Instantiate(Resources.Load("Tile"));
        go.GetComponent<TileData>().position = pos;
        go.GetComponent<TileData>().sprite = sp;
        go.GetComponent<TileData>().groundType = ts.groundType;
        go.name = "x" + pos.x + "y" + pos.y;
        go.transform.position = IsometricHelper.gridToGamePostion(pos);
        go.GetComponent<SpriteRenderer>().sortingOrder = IsometricHelper.getTileSortingOrder(pos);
        go.transform.SetParent(GameObject.Find("Tiles").transform);
        map.Add(go.GetComponent<TileData>());
    }
}
