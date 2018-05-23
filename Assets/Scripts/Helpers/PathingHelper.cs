using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PathingHelper
{
    public List<GridPosition> walkableAreas;
    public List<GridPosition> playerUnitPositions;
    public List<GridPosition> enemyUnitPositions;
    public List<GridPosition> blockingEffectPositions;

    public List<GridPosition> getBestPath(GridPosition start, GridPosition goal, int maxMovement, int maxJump)
    {
        List<GridPosition> path = new List<GridPosition>();
        GridPosition currentNode = start;
        int closestDistace = 999;
        for (int i = 0; i < maxMovement; i++)
        {
            GridPosition up = getGridPositionFromCoord(0, 1);
            GridPosition left = getGridPositionFromCoord(-1, 0);
            GridPosition right = getGridPositionFromCoord(1, 0);
            GridPosition down = getGridPositionFromCoord(0, -1);

            if(!up.Equals(new GridPosition(0,0)))
            {
                int tempDistance = IsometricHelper.distanceBetweenGridPositions(up, goal);
                if(tempDistance < closestDistace)
                {
                    closestDistace = tempDistance;
                    path[i] = up;
                }
            }
            if (!left.Equals(new GridPosition(0, 0)))
            {
                int tempDistance = IsometricHelper.distanceBetweenGridPositions(left, goal);
                if (tempDistance < closestDistace)
                {
                    closestDistace = tempDistance;
                    path[i] = left;
                }
            }
            if (!right.Equals(new GridPosition(0, 0)))
            {
                int tempDistance = IsometricHelper.distanceBetweenGridPositions(right, goal);
                if (tempDistance < closestDistace)
                {
                    closestDistace = tempDistance;
                    path[i] = right;
                }
            }
            if (!down.Equals(new GridPosition(0, 0)))
            {
                int tempDistance = IsometricHelper.distanceBetweenGridPositions(down, goal);
                if (tempDistance < closestDistace)
                {
                    closestDistace = tempDistance;
                    path[i] = down;
                }
            }

            

        }

        return path;
    }

    public GridPosition getGridPositionFromCoord(int x, int y)
    {
        GridPosition gp = walkableAreas.Find(pos => pos.x == x && pos.y == y);
        return gp;

    }

    public List<List<GridPosition>> getPossiblePaths(GridPosition start, int maxMovement, int maxJump, bool isPlayer)
    {
        List<List<GridPosition>> paths = new List<List<GridPosition>>();
        List<GridPosition> starting = new List<GridPosition>();
        starting.Add(start);
        paths.Add(starting);
        for (int i = 0; i < maxMovement; i++)
        {
            List<List<GridPosition>> tempList = new List<List<GridPosition>>();
            foreach (List<GridPosition> path in paths) {
                tempList = getPossibleMovesFromPositon(path, maxJump, isPlayer);
            }
            paths.AddRange(tempList);

        }
        List<List<GridPosition>> toRemove = new List<List<GridPosition>>();
        foreach (List<GridPosition> path1 in paths)
        {
            foreach (List<GridPosition> path2 in paths)
            {
                if(path1[path1.Count-1].Equals(path2[path2.Count - 1]))
                {
                    if(path1.Count > path2.Count)
                    {
                        toRemove.Add(path1);
                    } else
                    {
                        toRemove.Add(path2);
                    }
                }
            }
        }
        foreach(List<GridPosition> remove in toRemove)
        {
            paths.Remove(remove);
        }
        return paths;
    }

    public List<List<GridPosition>> getPossibleMovesFromPositon(List<GridPosition> path, int maxJump, bool isPlayer)
    {
        GridPosition up = getGridPositionFromCoord(0, 1);
        GridPosition left = getGridPositionFromCoord(-1, 0);
        GridPosition right = getGridPositionFromCoord(1, 0);
        GridPosition down = getGridPositionFromCoord(0, -1);

        List<List<GridPosition>> paths = new List<List<GridPosition>>();
        GridPosition position = path[path.Count-1];
        if (!up.Equals(new GridPosition(0, 0)))
        {
            if (Math.Abs(up.elevation - position.elevation) <= maxJump  && !blockingEffectPositions.Contains(up))
            { 
                List<GridPosition> possibleMoves = path;
                if (isPlayer &&!enemyUnitPositions.Contains(up)) {
                    possibleMoves.Add(up);
                    paths.Add(possibleMoves);
                } else if(!playerUnitPositions.Contains(up))
                {
                    possibleMoves.Add(up);
                    paths.Add(possibleMoves);
                }
            }


        }
        if (!left.Equals(new GridPosition(0, 0)))
        {
            if (Math.Abs(left.elevation - position.elevation) <= maxJump && !blockingEffectPositions.Contains(left))
            {
                List<GridPosition> possibleMoves = path;
                if (isPlayer && !enemyUnitPositions.Contains(left))
                {
                    possibleMoves.Add(left);
                    paths.Add(possibleMoves);
                }
                else if (!playerUnitPositions.Contains(left))
                {
                    possibleMoves.Add(left);
                    paths.Add(possibleMoves);
                }
            }
        }
        if (!right.Equals(new GridPosition(0, 0)))
        {
            if (Math.Abs(right.elevation - position.elevation) <= maxJump && !blockingEffectPositions.Contains(right))
            {
                List<GridPosition> possibleMoves = path;
                if (isPlayer && !enemyUnitPositions.Contains(right))
                {
                    possibleMoves.Add(right);
                    paths.Add(possibleMoves);
                }
                else if (!playerUnitPositions.Contains(right))
                {
                    possibleMoves.Add(right);
                    paths.Add(possibleMoves);
                }
            }
        }
        if (!down.Equals(new GridPosition(0, 0)))
        {
            if (Math.Abs(down.elevation - position.elevation) <= maxJump && !blockingEffectPositions.Contains(down))
            {
                List<GridPosition> possibleMoves = path;
                if (isPlayer && !enemyUnitPositions.Contains(down))
                {
                    possibleMoves.Add(down);
                    paths.Add(possibleMoves);
                }
                else if (!playerUnitPositions.Contains(down))
                {
                    possibleMoves.Add(down);
                    paths.Add(possibleMoves);
                }
            }
        }

        return paths;
    }

}