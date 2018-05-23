using UnityEngine;

[System.Serializable]
public struct GridPosition
{
    public int x;
    public int y;
    public int elevation;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.elevation = 0;
        
    }

    public GridPosition(int x, int y, int elevation)
    {
        this.x = x;
        this.y = y;
        this.elevation = elevation;
    }

    public override string ToString()
    {
        return "x="  + x +","+ "y=" + y + "," + "elevation=" + elevation ;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is GridPosition))
            return false;
        GridPosition pos = (GridPosition)obj;
        return pos.x == x && pos.y == y && pos.elevation == elevation;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static GridPosition operator +(GridPosition gp1, GridPosition gp2)
    {
        return new GridPosition(gp1.x + gp2.x, gp1.y + gp2.y);
    }
}